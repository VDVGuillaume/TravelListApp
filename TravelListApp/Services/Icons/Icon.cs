using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace TravelListApp.Services.Icons
{
    public static class Icon
    {
        public static string GetIcon(string name)
        {
            return (string)Windows.UI.Xaml.Application.Current.Resources[name];
        }

        public static Geometry GetIconGeometry(string name)
        {
            return (Geometry)XamlBindingHelper.ConvertValue(typeof(Geometry), Windows.UI.Xaml.Application.Current.Resources[name]);
        }
    }
}
