using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAPI.Common;

namespace WebAPI.Helper
{
    public class ApiClientHelper :IApiClientHelper
    {
        public  async Task<T> GetAsync<T>(string apiEndPoint) where T : class
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaType.Json));
                var response = await client.GetAsync(apiEndPoint);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response?.Content?.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(json);
                }

                return null;
            }
        }
    }
}
