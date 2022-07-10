using FoodSearch.Services;
using FoodSearch.View;

namespace FoodSearch.ViewModel;

[QueryProperty("SearchTerm", "SearchTerm")]
public partial class ProductsViewModel : BaseViewModel
{
    ProductService productService;

    public ObservableCollection<Product> Products { get; } = new();

    public bool FirstRun { get; set; } = true;

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    string searchTerm;

    public ProductsViewModel(ProductService productService)
    {
        Title = "Produits";
        this.productService = productService;
    }

    [RelayCommand]
    async Task GetRandomProductsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            var products = await productService.GetRandomProductsAsync();

            if (Products.Count != 0)
                Products.Clear();

            foreach (var product in products)
                Products.Add(product);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", "Unable to get products", "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    async Task SearchProductsAsync()
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            Title = SearchTerm;

            Products.Clear();

            var products = await productService.SearchProductsAsync(SearchTerm);                

            foreach (var product in products)
                Products.Add(product);

            SearchTerm = null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", "Unable to search products", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task GoToDetailsAsync(Product product)
    {
        if (product is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true, new Dictionary<string, object>
        {
            { "Product", product }
        });
    }
}
