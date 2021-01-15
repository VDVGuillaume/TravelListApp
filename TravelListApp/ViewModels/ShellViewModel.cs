using System;
using TravelListApp.Services.Icons;
using TravelListApp.Views;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace TravelListApp.ViewModels
{
    internal class ShellViewModel : ShellViewModelBase
    {
        
        public ShellViewModel()
        {


            //Build the menus

                Menu.Clear();
                Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("Home"), Text = "Home", NavigationDestination = typeof(HomePage) });
                Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("Explore"), Text = "TravelLists", NavigationDestination = typeof(TravelListPage) });

                SecondMenu.Clear();
                SecondMenu.Add(new MenuItem() { Glyph = Icon.GetIcon("User"), Text = "User", NavigationDestination = typeof(LoginPage) });
                SecondMenu.Add(new MenuItem() { Glyph = Icon.GetIcon("Settings"), Text = "Settings", NavigationDestination = typeof(ThemeSelectionPage) });
          
            
        }
    }
}
