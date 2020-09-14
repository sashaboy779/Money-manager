using System.Net.Http;
using System.Threading.Tasks;

namespace MoneyManagerUi.Infrastructure
{
    public interface IExpanseManagerClient
    {
        Task<HttpResponseMessage> SendRequestAsync();
        ExpanseManagerClient CreateRequestMessage(HttpMethod httpMethod, string uri);
        ExpanseManagerClient AddAuthorization(string token);
        ExpanseManagerClient AddJsonContent<TContent>(TContent content);
    }
}
