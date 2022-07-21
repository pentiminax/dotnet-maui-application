using FoodSearch.Services;
using FoodSearch.View;

namespace FoodSearch.ViewModel;

[QueryProperty("SearchTerm", "SearchTerm")]
public partial class ProductsViewModel : BaseViewModel
{
    ProductService productService;

    public ObservableCollection<Product> Products { get; } = new();

    public ObservableCollection<Product> SearchedProducts { get; } = new();

    public bool FirstRun { get; set; } = true;

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    string searchTerm;

    [ObservableProperty]
    string searchedTitle;

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

            SearchedTitle = SearchTerm;
            Title = SearchedTitle;

            SearchedProducts.Clear();

            var products = await productService.SearchProductsAsync(SearchTerm);                

            foreach (var product in products)
                SearchedProducts.Add(product);

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
