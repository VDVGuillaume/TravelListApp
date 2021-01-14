using TravelListApp.Services.Icons;
using TravelListApp.Services.ThemeResources;
using TravelListApp.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace TravelListApp.Services.Theming
{
    public class ThemeManager
    {

        /// <summary>
        /// Defines the supported themes for the sample app
        /// </summary>
        public enum Themes
        {
            Light,
            Dark
        }

        /// <summary>
        /// Gives current/last selected theme from the local storage.
        /// </summary>
        /// <returns></returns>
        public static Themes CurrentTheme()
        {
            return (Themes)Application.Current.RequestedTheme;
        }

        public static SolidColorBrush GetResource(string name)
        {
            SolidColorBrush brush = (SolidColorBrush)Windows.UI.Xaml.Application.Current.Resources[name];
            return brush;
        }
    }
}
