using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LvivRegionStatisticsVisualization.Services
{
    public class StatisticsDataService : IStatisticsDataService
    {
        private const string Message =
            "sel=&pxkonv=prntt&Valdavarden1=2&Valdavarden2=21&Valdavarden3=1&Valdavarden4=6&values1=1&values1=2&values2=1&values2=2&values2=3&values2=4&values2=5&values2=6&values2=7&values2=8&values2=9&values2=10&values2=11&values2=12&values2=13&values2=14&values2=15&values2=16&values2=17&values2=18&values2=19&values2=20&values2=21&values3=1&values4=1&matrix=24A0102_01&root=..%2FDatabase%2F24PRYRODA%2F01%2F02%2F&classdir=..%2FDatabase%2F24PRYRODA%2F01%2F02%2F&noofvar=4";

        private readonly IHttpClientFactory _httpClientFactory;

        public StatisticsDataService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task GetActualStatisticsData()
        {
            using (var httpClient = _httpClientFactory.CreateClient())
            {
                HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Post, $"http://database.ukrcensus.gov.ua/statbank_lviv/Dialog/SaveShow.asp");
                requestMessage.Content = new StringContent(Message, Encoding.UTF8, "application/x-www-form-urlencoded");
                var response = await httpClient.SendAsync(requestMessage);
                string responseContent = await response.Content.ReadAsStringAsync();

                File.WriteAllText("statistics.csv", responseContent, Encoding.UTF7);
            }
        }
    }
}
