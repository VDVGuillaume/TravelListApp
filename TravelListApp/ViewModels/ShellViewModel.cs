using System;
using TravelListApp.Services.Icons;
using TravelListApp.Views;

namespace TravelListApp.ViewModels
{
    internal class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            // Build the menus
            Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("Home"), Text = "Home", NavigationDestination = typeof(HomePage) });
            Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("Explore"), Text = "TravelLists", NavigationDestination = typeof(TravelListPage) });
            Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("Label"), Text = "Labels", NavigationDestination = typeof(HomePage) });

            SecondMenu.Add(new MenuItem() { Glyph = Icon.GetIcon("User"), Text = "User", NavigationDestination = typeof(HomePage) });
            SecondMenu.Add(new MenuItem() { Glyph = Icon.GetIcon("Settings"), Text = "Settings", NavigationDestination = typeof(HomePage) });
        }
    }
}
