using System.Net;
using WeatherForecast.Exception;

namespace WeatherForecast.Services.Implementations.Base
{
    public abstract class ExternalApiService
    {
        protected HttpClient _httpClient;

        protected ExternalApiService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        protected static void ProceedResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                switch (response.StatusCode)
                {
                    case HttpStatusCode.BadRequest:
                        throw new ArgumentException("Bad request.");
                    case HttpStatusCode.InternalServerError:
                        throw new HttpRequestException("Internal server error.");
                    case HttpStatusCode.NotFound:
                        throw new NotFoundException();
                    default:
                        throw new HttpRequestException($"Unexpected HTTP status code: {response.StatusCode}");
                }
            }
        }
    }
}
