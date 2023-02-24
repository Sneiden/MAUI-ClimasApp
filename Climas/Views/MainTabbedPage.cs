using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace Climas.Views
{
    public class MainTabbedPage : Microsoft.Maui.Controls.TabbedPage
    {
        public MainTabbedPage()
        {
            Children.Add(new ClimasPage());
            Children.Add(new Configuracion());
            Children.Add(new MapCities());

            On<Microsoft.Maui.Controls.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);


        }
    }
}
