using Climas.Context;
using Microsoft.Maui.Maps;

namespace Climas.Views;

public class MapCities : ContentPage
{
    Microsoft.Maui.Controls.Maps.Map map;

    public MapCities()
	{
        Title = "MapCities";
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var location = await Geolocation.Default.GetLocationAsync();
        map = new(MapSpan.FromCenterAndRadius(location,
            Distance.FromKilometers(5)));
        map.IsShowingUser = true;

        var cities = await new DataContext().GetCities();

        foreach (var city in cities)
        {
            if (city.Latitude is null || city.Longitude is null)
                continue;

            var weather = await new RestService().GetWeather(city.Latitude.Value, city.Longitude.Value);
            map.Pins.Add(
                new()
                {
                    Label = $"{city.CityName}: {weather.current_weather.temperature}°C; {weather.current_weather.time}",
                    Location = new Location(city.Latitude.Value, city.Longitude.Value)
                });
        }

        Content = map;
    }
}