namespace Projekt_Siłownia;

public partial class AdminPage : ContentPage
{
	public AdminPage()
	{
		InitializeComponent();
	}

    private async void LogOutClicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new NavigationPage(new LoginPage());
    }
    private async Task ShowOnlyAsync(VisualElement layoutToShow)
    {
        // Znajdź wszystkie layouty na stronie
        var layouts = new[] { layout1, layout2, layout3 };

        // Ukryj te, które są inne
        foreach (var layout in layouts)
        {
            if (layout == layoutToShow)
                continue;

            if (layout.IsVisible)
            {
                await layout.FadeTo(0, 200); // 200ms
                layout.IsVisible = false;
            }
        }

        // Pokaż wybrany layout
        if (!layoutToShow.IsVisible)
        {
            layoutToShow.Opacity = 0;
            layoutToShow.IsVisible = true;
            await layoutToShow.FadeTo(1, 200);
        }
    }

    private async void OnLayout1Clicked(object sender, EventArgs e)
        => await ShowOnlyAsync(layout1);

    private async void OnLayout2Clicked(object sender, EventArgs e)
        => await ShowOnlyAsync(layout2);

    private async void OnLayout3Clicked(object sender, EventArgs e)
        => await ShowOnlyAsync(layout3);
}