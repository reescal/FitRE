using System.Text.Json;
using FitRE.Pages;

namespace FitRE.Infrastructure;

public class WeatherForecastService : IWeatherForecastService
{
    private readonly HttpClient _httpClient;

    public WeatherForecastService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("WeatherForecast");
    }

    public async Task<IEnumerable<FetchData.WeatherForecast>> Get()
    {
        var apiResponse = await _httpClient.GetStreamAsync("api/WeatherForecast");
        var forecast = await JsonSerializer.DeserializeAsync<IEnumerable<FetchData.WeatherForecast>>
            (apiResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        return forecast;
    }
}

public interface IWeatherForecastService
{
    Task<IEnumerable<FetchData.WeatherForecast>> Get();
}