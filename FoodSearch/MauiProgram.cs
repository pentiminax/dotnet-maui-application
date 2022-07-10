using FoodSearch.Services;
using FoodSearch.View;
using FoodSearch.ViewModel;

namespace FoodSearch;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

        builder.Services.AddSingleton<ProductService>();

        builder.Services.AddSingleton<ProductsViewModel>();
        builder.Services.AddSingleton<ProductDetailsViewModel>();
        builder.Services.AddSingleton<ProductsSearchViewModel>();

        builder.Services.AddTransient<MainPage>();
        builder.Services.AddSingleton<SearchPage>();
        builder.Services.AddTransient<DetailsPage>();

        return builder.Build();
	}
}
