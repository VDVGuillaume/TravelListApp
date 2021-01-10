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
        /// Changes the theme of the app.
        /// Add additional switch cases for more themes you add to the app.
        /// This also updates the local key storage value for the selected theme.
        /// </summary>
        /// <param name="theme"></param>
        public static void ChangeTheme(Themes theme)
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(new IconsTheme());
                switch (theme)
                {
                    //case Themes.Light:
                    //    {
                    //        mergedDictionaries.Add(new LightTheme());
                    //        break;
                    //    }
                    //case Themes.Dark:
                    //    {
                    //        mergedDictionaries.Add(new DarkTheme());
                    //        break;
                    //    }
                    default:
                        mergedDictionaries.Add(new Theme());
                        break;
                }
            }

                    
        }

        public static void SetupTheme()
        {
            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();
                mergedDictionaries.Add(new IconsTheme());
                mergedDictionaries.Add(new Theme());
            }
        }

        /// <summary>
        /// Reads current theme id from the local storage and loads it.
        /// </summary>
        public static void LoadTheme()
        {
            Themes currentTheme = CurrentTheme();
            SetupTheme();
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
