using Climas.Models;
using System.Text.Json;

namespace Climas.Context
{

    class RestService
    {
        HttpClient _httpClient;
        Uri _urlBase;

        public RestService()
        {
            _urlBase = new Uri("https://api.open-meteo.com/v1/");
            _httpClient = new();
            _httpClient.BaseAddress = _urlBase;
        }

        public async Task<WeatherApi> GetWeather(double lat, double lon)
        {
            var response = await _httpClient.GetAsync($"forecast?latitude={lat}&longitude={lon}&current_weather=true");
            if (response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<WeatherApi>(content,
                    new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            throw new Exception("Error al tratar de obtener la informacion");
        }
    }
}
