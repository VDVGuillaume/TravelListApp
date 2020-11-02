using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListApp.Services.Icons;
using TravelListApp.Views;

namespace TravelListApp.Mvvm
{
    internal class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            // Build the menus
            Menu.Add(new MenuItem() { Glyph = Icon.GetIcon("Explore"), Text = "TravelLists", NavigationDestination = typeof(TravelListPage) });
        }
    }
}
