using Newtonsoft.Json;
using static LotoNumbers.Premiacoes;

namespace LotoNumbers
{
    public class LoteriaAPI
    {
        private readonly HttpClient _httpClient;

        public LoteriaAPI()
        {
            _httpClient = new HttpClient();
        }

        public LoteriaInfo GetLatestLoteriaInfo(string loteria)
        {
            var httpClient = new HttpClient();
            var response = httpClient.GetAsync($"https://loteriascaixa-api.herokuapp.com/api/{loteria}/latest").Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<LoteriaInfo>(responseContent);
        }
    }
}
