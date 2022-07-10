namespace FoodSearch.ViewModel;

[QueryProperty("Product", "Product")]
public partial class ProductDetailsViewModel : BaseViewModel
{
    [ObservableProperty]
    Product product;

    [RelayCommand]
    async Task OpenProductAsync(Product product)
    {
        if (product is null)
            return;

        try
        {
            Uri uri = new(product.Url);
            await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error", "Unable to open browser", "OK");
        }
    }
}
