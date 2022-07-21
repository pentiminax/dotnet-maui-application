using FoodSearch.Model;
using System.Net.Http.Json;

namespace FoodSearch.Services;

public class ProductService
{
    const string BASE_SEARCH_URL = "https://fr.openfoodfacts.org/cgi/search.pl?action=process&json=true";

    HttpClient httpClient;

    public ProductService()
    {
        httpClient = new HttpClient();
    }

    public async Task<List<Product>> GetRandomProductsAsync()
    {
        var page = new Random().Next(1, 1000);

        var url = $"{BASE_SEARCH_URL}&page={page}&page_size=10&{ProductService.GetNutriScoreFilter(0)}";

        var response = await httpClient.GetAsync(url);

        var products = await GetProductsAsync(response);

        return products;
    }

    public async Task<List<Product>> SearchProductsAsync(string searchTerm)
    {

        var url = $"{BASE_SEARCH_URL}&tag_contains_0=contains&tagtype_0=categories" +
            $"&tagtype_1=label&tag_0={searchTerm}&{ProductService.GetNutriScoreFilter(2)}&page_size=10";

        var response = await httpClient.GetAsync(url);

        var products = await GetProductsAsync(response);

        return products;
    }

    private static async Task<List<Product>> GetProductsAsync(HttpResponseMessage response)
    {
        List<Product> products = new();

        if (response.IsSuccessStatusCode)
        {
            products = (await response.Content.ReadFromJsonAsync<ProductsResult>()).Products;
        }

        return products;
    }

    private static string GetNutriScoreFilter(int tagId)
    {
        var nutriScore = Preferences.Get("NutriScore", "ALL");

        return ("ALL" == nutriScore) ? string.Empty : $"tagtype_{tagId}=nutrition_grades&tag_contains_{tagId}=contains&tag_{tagId}={nutriScore}";
    }
}
