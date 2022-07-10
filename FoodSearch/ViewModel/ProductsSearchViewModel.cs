using FoodSearch.View;

namespace FoodSearch.ViewModel;

public partial class ProductsSearchViewModel : BaseViewModel
{
    public ObservableCollection<string> SearchTermsHistory { get; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsSearchTermsHistoryNotEmpty))]
    bool isSearchTermsHistoryEmpty = true;

    public bool IsSearchTermsHistoryNotEmpty => !isSearchTermsHistoryEmpty;

    public ProductsSearchViewModel()
    {
        Title = "Rechercher";
    }

    [RelayCommand]
    async Task SearchProductsAsync(string searchTerm)
    {
        SearchTermsHistory.Add(searchTerm);

        IsSearchTermsHistoryEmpty = false;

        await Shell.Current.GoToAsync($"{nameof(MainPage)}", true, new Dictionary<string, object>
        {
            { "SearchTerm", searchTerm }
        });
    }

    [RelayCommand]
    void ClearSearchTermsHistory()
    {
        SearchTermsHistory.Clear();

        IsSearchTermsHistoryEmpty = true;
    }
}
