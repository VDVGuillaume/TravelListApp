
using System.Diagnostics;
using System.Linq;
using TravelListApp.Services.Navigation;
using TravelListApp.Services.Theming;
using TravelListApp.ViewModels;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelListApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell : Page
    {
        public ThemeViewModel ThemeViewModel { get; } = App.ThemeViewModel;
        
        public Shell()
        {
            InitializeComponent();

            // Initialize Navigation Service.            
            Navigation.Frame = SplitViewFrame;

            // Navigate to home page.
            // Navigation.Navigate(typeof(HomePage));
            App.ViewModel.PropertyChanged += (obj, ev) =>
            {
                if (ev.PropertyName.Equals("IsLoading"))
                {
                    MyProgressRing.IsActive = App.ViewModel.IsLoading;
                    if (App.ViewModel.IsLoading)
                    {
                        MyProgressGrid.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        MyProgressGrid.Visibility = Visibility.Collapsed;
                    }
                }

            };
        }

        // Navigate to another page.
        private async void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
            }               
                

            
                if (e.AddedItems.Count > 0 && e.AddedItems.First() is MenuItem menuItem && menuItem.IsNavigation)
                {
                    if(menuItem.Text == "User")
                    {
                    Navigation.Frame = Frame;
                    Frame.Navigate(typeof(LoginPage));
                    }
                    if (menuItem.NavigationDestination == typeof(HomePage))
                    {
                        await App.ViewModel.GetFirstUpcomingAsync();
                    }

                    Navigation.Navigate(menuItem.NavigationDestination);
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

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Navigate to home page.
            await App.ViewModel.GetFirstUpcomingAsync();
            Navigation.Navigate(typeof(HomePage));

            Navigation.EnableBackButton();
            base.OnNavigatedTo(e);
        }

        private void OnLeavingBackground(object sender, LeavingBackgroundEventArgs e)
        {
            // Your code here.
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