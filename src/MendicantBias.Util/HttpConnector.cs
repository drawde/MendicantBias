using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MendicantBias.Util
{
    public class HttpConnector : IHttpConnector
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpConnector(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<T> PostAsync<T>(string action, object parameter)
        {
            var client = _httpClientFactory.CreateClient();

            var company = JsonConvert.SerializeObject(parameter);

            var requestContent = new StringContent(company, Encoding.UTF8, "application/json");

            var response = await (await client.PostAsync(TenantAdminWebConfig.Instance.MendicantBiasAPI.URL + action, requestContent)).Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(response);
        }

        public async Task<T> PutAsync<T>(string action, object parameter)
        {
            var client = _httpClientFactory.CreateClient();

            var company = JsonConvert.SerializeObject(parameter);

            var requestContent = new StringContent(company, Encoding.UTF8, "application/json");

            var response = await (await client.PutAsync(TenantAdminWebConfig.Instance.MendicantBiasAPI.URL + action, requestContent)).Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(response);
        }
    }
}
