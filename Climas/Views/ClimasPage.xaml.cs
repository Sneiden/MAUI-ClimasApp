using Climas.Context;
using Climas.Models;
using System.Security.Claims;

namespace Climas.Views;

public partial class ClimasPage : ContentPage
{
    private List<City> citys;
    public ClimasPage()
    {
        InitializeComponent();

        
        citys = new()
        {
            new City
            {
                CityName="Mexicali",
                Hora=DateTime.Now,
                PhotoUrl="https://www.periodismonegro.mx/wp-content/uploads/2021/05/contaminaci%C3%B3n.jpg",
            },
            new City
            {
                CityName="Culiacan",
                Hora=DateTime.Now,
                PhotoUrl="https://www.rtv.org.mx/masnoticias/wp-content/uploads/sites/13/2019/10/91018024.jpg",
            }
        };
    }

    //public void AddCity( City city )
    //{
    //    citys.Add( city );
    //}

    private async Task LoadList()
    {
        citys =  await new DataContext().GetCities();

        foreach (City city in citys)
        {
            var weather = await new RestService().GetWeather();
            if (weather != null)
            {
                city.Temp = weather.current_weather.temperature;
                city.Hora = DateTime.Parse(weather.current_weather.time);
            }
        }

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

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new AddCity());
    }
}