using System.Text.Json;
using FitRE.Shared;

namespace FitRE.Infrastructure;

public class BudgetService : IBudgetService
{
    private readonly HttpClient _httpClient;

    public async Task<Budget> Get(string id)
    {
        //var budget = await _httpClient.GetFromJsonAsync<Budget>($"budget/{id}");
        try
        {
            var apiResponse = await _httpClient.GetStreamAsync($"budget/{id}");
            var budget = await JsonSerializer.DeserializeAsync<Budget>
                (apiResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return budget;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public BudgetService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient(nameof(Budget));
    }
}

public interface IBudgetService
{
    Task<Budget> Get(string id);
}