using FoodSearch.ViewModel;

namespace FoodSearch.View;

public partial class SearchPage : ContentPage
{
    ProductsSearchViewModel viewModel;

    public SearchPage(ProductsSearchViewModel viewModel)
    {
        InitializeComponent();
        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
    {
        await viewModel.SearchProductsCommand.ExecuteAsync(searchBar.Text);

        searchBar.Text = string.Empty;
    }
}