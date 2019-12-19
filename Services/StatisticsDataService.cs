using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LvivRegionStatisticsVisualization.Models;

namespace LvivRegionStatisticsVisualization.Services
{
    public class StatisticsDataService : IStatisticsDataService
    {
        private const string Message =
            "sel=&pxkonv=prntt&Valdavarden1=2&Valdavarden2=21&Valdavarden3=1&Valdavarden4=6&values1=1&values1=2&values2=1&values2=2&values2=3&values2=4&values2=5&values2=6" +
            "&values2=7&values2=8&values2=9&values2=10&values2=11&values2=12&values2=13&values2=14&values2=15&values2=16&values2=17&values2=18&values2=19&values2=20&values2=21" +
            "&values3=1&values4=1&values4=2&values4=3&values4=4&values4=5&values4=6&matrix=24A0102_01&root=..%2FDatabase%2F24PRYRODA%2F01%2F02%2F&classdir=..%2FDatabase%2F24PRYRODA%2F01%2F02%2F&noofvar=4";

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ICsvDataParser _csvDataParser;

        public StatisticsDataService(IHttpClientFactory httpClientFactory, ICsvDataParser csvDataParser)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _httpClientFactory = httpClientFactory;
            _csvDataParser = csvDataParser;
        }

        public async Task<EnergyUsage> GetActualStatisticsData()
        {
            using HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post,
                "http://database.ukrcensus.gov.ua/statbank_lviv/Dialog/SaveShow.asp")
            {
                Content = new StringContent(Message, Encoding.UTF8, "application/x-www-form-urlencoded")
            };

            HttpResponseMessage response = await httpClient.SendAsync(requestMessage);
            var responseContentBytes = await response.Content.ReadAsByteArrayAsync();

            Encoding win1251 = Encoding.GetEncoding("windows-1251");
            var convertedString = win1251.GetString(responseContentBytes);

            return _csvDataParser.ParseCsvData(convertedString);
        }
    }
}