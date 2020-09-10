using MoneyManagerUi.Data;
using MoneyManagerUi.Infrastructure.Constants;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MoneyManagerUi.Infrastructure
{
    public class ExpanseManagerClient : IExpanseManagerClient
    {
        private readonly HttpClient httpClient;
        private HttpRequestMessage requestMessage;


        public ExpanseManagerClient(IOptions<AppSettings> appSettings)
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(appSettings.Value.MoneyManagerApiUri)
            };
            httpClient.BaseAddress = new Uri(appSettings.Value.MoneyManagerApiUri);
            httpClient.DefaultRequestHeaders.Add(Configuration.UserAgentKey, Configuration.UserAgentValue);
            
            var currentCulture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            httpClient.DefaultRequestHeaders.Add(Configuration.AcceptLanguage, currentCulture);
        }

        public async Task<HttpResponseMessage> SendRequestAsync()
        {
            return await httpClient.SendAsync(requestMessage);
        }

        public ExpanseManagerClient CreateRequestMessage(HttpMethod httpMethod, string uri)
        {
            requestMessage = new HttpRequestMessage(httpMethod, uri);
            return this;
        }
        public ExpanseManagerClient AddAuthorization(string token)
        {
            requestMessage.Headers.Authorization =
               new AuthenticationHeaderValue(Configuration.BearerAuthenticationType, token);
            return this;
        }

        public ExpanseManagerClient AddJsonContent<TContent>(TContent content)
        {
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(content));
            requestMessage.Content.Headers.ContentType =
                new MediaTypeHeaderValue(Configuration.JsonContentType);
            return this;
        }
    }
}
