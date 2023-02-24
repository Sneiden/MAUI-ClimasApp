using Climas.Context;
using Climas.Models;

namespace Climas.Views;

public partial class Configuracion : ContentPage
{
    private List<City> citys;

    public Configuracion()
	{
		InitializeComponent();
	}
    private async Task LoadList()
    {
        citys = await new DataContext().GetCities();

        ClimasList.ItemsSource = citys;

    }

    protected override async void OnAppearing()
    {
        await LoadList();
        base.OnAppearing();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        ClimasList.ItemsSource = null;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var city = (Models.City)((Button)sender).BindingContext;
        var cityResult = await new DataContext()
            .RemoveFromCities(city.Id);

        if (cityResult)
            await LoadList();
    }
}