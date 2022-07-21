namespace FoodSearch.View;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

	protected override void OnAppearing()
	{
		var nutriScore = Preferences.Get("NutriScore", "ALL");

		var radioButtons = nutriScoreStackLayout.Children.Where(c => c is RadioButton);

		foreach (RadioButton radioButton in radioButtons)
		{
			if (radioButton.Value.ToString() == nutriScore)
			{
				radioButton.IsChecked = true;
				break;
			}
		}

        base.OnAppearing();
	}

	private void OnNutriScoreCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		var nutriScore = ((RadioButton)sender).Value.ToString();

		Settings.NutriScore = nutriScore;
	}
}