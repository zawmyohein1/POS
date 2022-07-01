using System.Net.Http;
using System.Threading.Tasks;

namespace POS.Infrastructure.Common.WebApi
{
    public interface IWebApiClient
    {      

        Task<HttpResponseMessage> GetAsync(string relativeURI,string token);     

        HttpResponseMessage Get(string relativeURI, string token);

        Task<HttpResponseMessage> PostAsync(string relativeURI, string jsonData, string token);

        HttpResponseMessage Post(string relativeURI, string jsonData, string token);

        Task<HttpResponseMessage> PutAsync(string relativeURI, string jsonData, string token);

        HttpResponseMessage Put(string relativeURI, string jsonData, string token);

        Task<HttpResponseMessage> DeleteAsync(string relativeURI, string token);

        Task<HttpResponseMessage> RecoverAsync(string relativeURI, string token);

        HttpResponseMessage Delete(string relativeURI, string token);
    }
}
