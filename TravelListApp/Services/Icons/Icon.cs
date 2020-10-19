namespace TravelListApp.Services.Icons
{
    public static class Icon
    {
        public static string GetIcon(string name)
        {
            return (string)Windows.UI.Xaml.Application.Current.Resources[name];
        }
    }
}
