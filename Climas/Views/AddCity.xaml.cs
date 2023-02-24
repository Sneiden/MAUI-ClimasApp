using Climas.Context;
using Climas.Models;

namespace Climas.Views;

public partial class AddCity : ContentPage
{
	public AddCity()
	{
		InitializeComponent();
	}

    private async void btnAceptar_Clicked(object sender, EventArgs e)
    {
        City city = new()
        {
            CityName = entryCity.Text,
            Latitude = double.Parse(entryLatitud.Text),
            Longitude = double.Parse(entryLongitud.Text),
            PhotoUrl = entryPhotoUrl.Text,
        };
        var cityResult = await new DataContext()
            .SetCity(city);

        await DisplayAlert("Auto Favorito", cityResult ? "Ciudad Agregada Correctamente" : "La Ciudad ya se encuentra en agregada", "OK");
        await Navigation.PopAsync();
    }

    private async void btnAceptarHere_Clicked(object sender, EventArgs e)
    {
        var location = await Geolocation.Default.GetLocationAsync();

        entryLatitud.Text = location.Latitude.ToString();
        entryLongitud.Text = location.Longitude.ToString();

        City city = new()
        {
            CityName = entryCity.Text,
            Latitude = location.Latitude,
            Longitude = location.Longitude,
            PhotoUrl = entryPhotoUrl.Text,
        };
        var cityResult = await new DataContext()
            .SetCity(city);

        await DisplayAlert("Auto Favorito", cityResult ? "Ciudad Agregada Correctamente" : "La Ciudad ya se encuentra en agregada", "OK");
        await Navigation.PopAsync();
    }
}