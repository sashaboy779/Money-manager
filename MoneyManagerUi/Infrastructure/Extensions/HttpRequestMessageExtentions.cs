using MoneyManagerUi.Data;
using MoneyManagerUi.Infrastructure.Exceptions;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace MoneyManagerUi.Infrastructure.Extensions
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<TDestination> TryFetchContentAsync<TDestination>(this HttpResponseMessage response)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            await TryThrowModelErrorAsync(response);

            return JsonConvert.DeserializeObject<TDestination>(responseBody);
        }

        public static async Task TryThrowModelErrorAsync(this HttpResponseMessage response)
        {
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                TryThrowModelErrorException(responseBody);
            }
        }

        private static void TryThrowModelErrorException(string responseBody)
        {
            var error = JsonConvert.DeserializeObject<Errors>(responseBody);
            throw new ModelErrorException(error);
        }
    }
}
