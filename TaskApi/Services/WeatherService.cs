using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace TaskApi.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public WeatherService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
        }

        public async Task<object?> GetThreeDayForecastAsync()
        {
            string apiKey = _config["WeatherApi:ApiKey"];
            string url = $"/forecast.json?key={apiKey}&q=Lincoln,NE&days=3";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var forecast = JsonSerializer.Deserialize<object>(content,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return forecast;
        }
    }
}
