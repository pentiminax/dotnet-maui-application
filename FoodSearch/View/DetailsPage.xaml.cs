using FoodSearch.ViewModel;

namespace FoodSearch.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(ProductDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}