using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infrastructure.Common.WebApi
{
    public class WebApiClient : IWebApiClient
    {
        private Uri _baseUri;
        private readonly IConfiguration _configuration;
        public WebApiClient(IConfiguration configuration)
        {
            _configuration = configuration;
            _baseUri = new Uri(_configuration["URL:ApiUrl"]);
        }

        private HttpClient CreateHttpClient(string token)
        {
            var client = new HttpClient
            {
                BaseAddress = _baseUri
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeFormats.Json));
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return client;
        }

        public async Task<HttpResponseMessage> GetAsync(string relativeURI, string token)
        {
            using (var httpClient = CreateHttpClient(token))
            {
                return await httpClient.GetAsync(relativeURI);
            }
        }

        public HttpResponseMessage Get(string relativeURI, string token)
        {
            using (var httpClient = CreateHttpClient(token))
            {
                return httpClient.GetAsync(relativeURI).Result;
            }
        }

        public async Task<HttpResponseMessage> PostAsync(string relativeURI, string jsonData, string token)
        {
            using (var httpClient = CreateHttpClient(token))
            {
                var contentPost = new StringContent(jsonData, Encoding.UTF8, MediaTypeFormats.Json);

                return await httpClient.PostAsync(relativeURI, contentPost);
            }
        }

        public HttpResponseMessage Post(string relativeURI, string jsonData, string token)
        {
            using (var httpClient = CreateHttpClient(token))
            {
                var contentPost = new StringContent(jsonData, Encoding.UTF8, MediaTypeFormats.Json);

                return httpClient.PostAsync(relativeURI, contentPost).Result;
            }
        }

        public async Task<HttpResponseMessage> PutAsync(string relativeURI, string jsonData, string token)
        {
            using (var httpClient = CreateHttpClient(token))
            {
                var contentPost = new StringContent(jsonData, Encoding.UTF8, MediaTypeFormats.Json);

                return await httpClient.PutAsync(relativeURI, contentPost);
            }
        }

        public HttpResponseMessage Put(string relativeURI, string jsonData, string token)
        {
            using (var httpClient = CreateHttpClient(token))
            {
                var contentPost = new StringContent(jsonData, Encoding.UTF8, MediaTypeFormats.Json);

                return httpClient.PutAsync(relativeURI, contentPost).Result;
            }
        }

        public async Task<HttpResponseMessage> DeleteAsync(string relativeURI, string token)
        {
            using (var httpClient = CreateHttpClient(token))
            {
                return await httpClient.DeleteAsync(relativeURI);
            }
        }

        public async Task<HttpResponseMessage> RecoverAsync(string relativeURI, string token)
        {
            using (var httpClient = CreateHttpClient(token))
            {
                return await httpClient.GetAsync(relativeURI);
            }
        }

        public HttpResponseMessage Delete(string relativeURI, string token)
        {
            using (var httpClient = CreateHttpClient(token))
            {
                return httpClient.DeleteAsync(relativeURI).Result;
            }
        }
    }

    public class MediaTypeFormats
    {
        public const string Json = "application/json";
        public const string XML = "application/xml";
    }
}

