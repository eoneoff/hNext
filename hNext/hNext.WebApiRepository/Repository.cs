using System.Collections.Generic;
using hNext.Model;
using hNext.IRepository;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Web.Http;
using System.Text;

namespace hNext.WebApiRepository
{
    public class Repository<T> : IRepository<T>
    {
        protected static Dictionary<string, string> ApiKeys = new Dictionary<string, string>
    {
        {nameof(Person), "people"}
    };

        protected HttpClient _httpClient;

        public Repository(HttpClient client) => _httpClient = client;

        public async Task<T> Delete(params object[] key)
        {
            StringBuilder requestUrl = new StringBuilder(ApiKeys[nameof(T)]);
            foreach (var k in key)
            {
                requestUrl.Append($"/{k}");
            }
            return await ReadResponse<T>(await _httpClient.DeleteAsync(requestUrl.ToString()));

        }

        public Task<bool> Exists(params object[] key)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await ReadResponse<IEnumerable<T>>(await _httpClient.GetAsync(ApiKeys[nameof(T)]));
        }

        public async Task<T> Get(params object[] key)
        {
            StringBuilder requestUrl = new StringBuilder(ApiKeys[nameof(T)]);
            foreach (var k in key)
            {
                requestUrl.Append($"/{k}");
            }
            return await ReadResponse<T>(await _httpClient.GetAsync(requestUrl.ToString()));
        }

        public async Task<T> Post(T item)
        {
            return await ReadResponse<T>(await _httpClient.PostAsJsonAsync(ApiKeys[nameof(T)], item));
        }

        public async Task<T> Put(T item, params object[] key)
        {
            StringBuilder requestUrl = new StringBuilder(ApiKeys[nameof(T)]);
            foreach (var k in key)
            {
                requestUrl.Append($"/{k}");
            }
            var response = await _httpClient.PutAsJsonAsync(requestUrl.ToString(), item);
            return await ReadResponse<T>(await _httpClient.PutAsJsonAsync<T>(requestUrl.ToString(), item));
        }

        protected async Task<U> ReadResponse<U>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<U>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            throw new HttpResponseException(response.StatusCode);
        }
    }
}