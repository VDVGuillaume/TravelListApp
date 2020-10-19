using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelListApp.Mvvm;
using TravelListApp.Services.Navigation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelListApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell
    {
        public Shell()
        {
            InitializeComponent();

            // Initialize Navigation Service.
            Navigation.Frame = SplitViewFrame;

            // Navigate to home page.
            Navigation.Navigate(typeof(HomePage));
        }

        // Navigate to another page.
        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                // Unselect the other menu.
                if ((sender as ListView) == Menu)
                {
                    SecondMenu.SelectedItem = null;
                }
                else
                {
                    Menu.SelectedItem = null;
                }

                if (e.AddedItems.First() is MenuItem menuItem && menuItem.IsNavigation)
                {
                    Navigation.Navigate(menuItem.NavigationDestination);
                }
            }
        }

        // Execute command.
        private void Menu_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is MenuItem menuItem && !menuItem.IsNavigation)
            {
                Debugger.Break();
                menuItem.Command.Execute(null);
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Navigation.EnableBackButton();
            base.OnNavigatedTo(e);
        }

        // Swipe to open the splitview panel.
        private void SplitViewOpener_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.X > 50)
            {
                MySplitView.IsPaneOpen = true;
            }
        }

        // Swipe to close the splitview panel.
        private void SplitViewPane_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {
            if (e.Cumulative.Translation.X < -50)
            {
                MySplitView.IsPaneOpen = false;
            }
        }

        // Open or close the splitview panel through Hamburger button.
        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void SplitViewFrame_OnNavigated(object sender, NavigationEventArgs e)
        {
            // Lookup destination type in menu(s)
            var item = (from i in Menu.Items
                        where (i as MenuItem).NavigationDestination == e.SourcePageType
                        select i).FirstOrDefault();
            if (item != null)
            {
                Menu.SelectedItem = item;
                return;
            }

            Menu.SelectedIndex = -1;

            item = (from i in SecondMenu.Items
                    where (i as MenuItem).NavigationDestination == e.SourcePageType
                    select i).FirstOrDefault();
            if (item != null)
            {
                SecondMenu.SelectedItem = item;
                return;
            }

            SecondMenu.SelectedIndex = -1;
        }
    }
}
