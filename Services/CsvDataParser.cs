using System;
using System.Collections.Generic;
using System.Linq;
using LvivRegionStatisticsVisualization.Extensions;
using LvivRegionStatisticsVisualization.Models;

namespace LvivRegionStatisticsVisualization.Services
{
    public class CsvDataParser : ICsvDataParser
    {
        private readonly string[] _territoryNameIdentifiers = 
        {
            "область",
            "район"
        };

        private readonly string[] _energyTypes =
        {
            "Теплоенергія",
            "Електроенергія"
        };

        public EnergyUsage ParseCsvData(string inputString)
        {
            List<List<string>> parsedRowValues = ParseRows(inputString);
            if (!TryParseYears(parsedRowValues, out var years, out var indexOfYearsRow) || 
                !TryParseTerritoryName(parsedRowValues, out var territoryName) ||
                !TryParseEnergyTypes(parsedRowValues.Skip(indexOfYearsRow + 1).ToList(), years, out var energyTypes))
            {
                return null;
            }

            EnergyUsage result = new EnergyUsage
            {
                TerritoryName = territoryName,
                EnergyTypes = energyTypes,
                Years = years
            };

            return result;
        }

        private List<List<string>> ParseRows(string inputString)
        {
            List<List<string>> parsedRowValues = new List<List<string>>();

            var stringLines = inputString.Split(Environment.NewLine);
            foreach (var str in stringLines)
            {
                List<string> stringRow = new List<string>();
                var rowValues = str.Split('\"').ToList();
                rowValues.ForEach(v =>
                {
                    if (rowValues.IndexOf(v) == rowValues.Count - 1)
                    {
                        // Last field contains double values, separated by space or tab
                        foreach (var s in v.Split(new[] { ' ', '\t' }))
                        {
                            if (s.IsValidRowValue())
                            {
                                stringRow.Add(s);
                            }
                        }
                    }
                    else if (!string.IsNullOrEmpty(v) && v.IsValidRowValue())
                    {
                        stringRow.Add(v);
                    }
                });

                if (stringRow.Any())
                {
                    parsedRowValues.Add(stringRow);
                }
            }

            return parsedRowValues;
        }

        private int FindIndexOfYearsRow(List<List<string>> rows)
        {
            int index = 0;
            foreach (List<string> rowValues in rows)
            {
                if (rowValues.Count == 0)
                {
                    continue;
                }

                bool isValid = true;
                foreach (var rowValue in rowValues)
                {
                    if (!(int.TryParse(rowValue, out var value) && value <= 2020 && value >= 2000))
                    {
                        isValid = false;
                        break;
                    }
                }
                if (isValid)
                {
                    return index;
                }

                index++;
            }

            return -1;
        }

        private bool TryParseYears(List<List<string>> rows, out List<int> years, out int indexOfYearsRow)
        {
            int yearsRowIndex = FindIndexOfYearsRow(rows);
            if (yearsRowIndex == -1)
            {
                years = null;
                indexOfYearsRow = -1;
                return false;
            }

            var result = new List<int>();
            rows[yearsRowIndex].ForEach(s =>
            {
                result.Add(int.Parse(s));
            });

            years = result;
            indexOfYearsRow = yearsRowIndex;
            return true;
        }

        private bool TryParseTerritoryName(List<List<string>> rows, out string territoryName)
        {
            foreach (var rowValues in rows)
            {
                foreach(var s in rowValues)
                {
                    if (_territoryNameIdentifiers.Any(t => s.ToLower().Contains(t)))
                    {
                        territoryName = s;
                        return true;
                    }
                }
            }

            territoryName = null;
            return false;
        }

        private bool TryParseEnergyTypes(List<List<string>> rows, List<int> years, out List<EnergyType> energyTypes)
        {
            var result = new List<EnergyType>();
            EnergyType currentEnergyType = default;
            for (int rowIndex = 0; rowIndex < rows.Count; rowIndex++)
            {
                var rowValues = rows[rowIndex];
                if (rowValues.Count == 1 && _energyTypes.Any(t => rowValues.First().Contains(t)))
                {
                    currentEnergyType = new EnergyType
                    {
                        Name = rowValues[0]
                    };
                    result.Add(currentEnergyType);
                } else if (rowValues.First()[0].IsCapital() && TryParseActivityType(rowValues, years, out var activityType))
                {
                    // If first character is capital, then this is activity name
                    if (currentEnergyType != null)
                    {
                        if (currentEnergyType.Activities == null)
                        {
                            currentEnergyType.Activities = new List<ActivityType>();
                        }
                        currentEnergyType.Activities.Add(activityType);
                    }
                } else
                {
                    // If first character is capital, then this is name of subactivity
                    var localRowValues = rowValues;
                    while (!localRowValues.First()[0].IsCapital())
                    {
                        if (TryParseActivityType(rowValues, years, out var subActivityType))
                        {
                            if (currentEnergyType != null)
                            {
                                if (currentEnergyType.Activities.Last().Subtypes == null)
                                {
                                    currentEnergyType.Activities.Last().Subtypes = new List<ActivityType>();
                                }
                                currentEnergyType.Activities.Last().Subtypes.Add(subActivityType);
                            }
                        }

                        rowIndex++;
                        if (rowIndex == rows.Count)
                        {
                            break;
                        }
                        localRowValues = rows[rowIndex];
                    }
                }
            }

            energyTypes = result;
            return true;
        }

        private bool TryParseActivityType(List<string> rowValues, List<int> years, out ActivityType activityType)
        {
            var result = new ActivityType
            {
                Name = rowValues.First()
            };

            int index = 0;
            foreach (var rowValue in rowValues.Skip(1))
            {
                if (double.TryParse(rowValue, out var usage))
                {
                    if (result.EnergyUsage == null)
                    {
                        result.EnergyUsage = new List<EnergyUsageByYear>();
                    }

                    result.EnergyUsage.Add(new EnergyUsageByYear(years[index], usage));
                }

                index++;
            }

            if (result.EnergyUsage == null)
            {
                activityType = null;
                return false;
            }

            activityType = result;
            return true;
        }
    }
}
