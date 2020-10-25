using System;
using System.Linq;
using TravelListApp.Controls;
using TravelListApp.Mvvm;
using TravelListApp.Services.Icons;
using TravelListApp.Services.Navigation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelListApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TravelListItemMenu : UserControl
    {
        private WrapGrid _itemsPanel;

        public TravelListItemMenu()
        {
            this.InitializeComponent();

            // Populate Menu.
            Menu.Items.Add(new MenuItem() { Glyph = Icon.GetIcon("SevenDotsIcon"), Text = "View", NavigationDestination = typeof(TravelListItemPage) });
            Menu.Items.Add(new MenuItem() { Glyph = Icon.GetIcon("SevenDotsIcon"), Text = "Details", NavigationDestination = typeof(TravelListItemDetailsPage) });
            Menu.Items.Add(new MenuItem() { Glyph = Icon.GetIcon("SevenDotsIcon"), Text = "Checklist", NavigationDestination = typeof(TravelListItemChecklistPage) });
            Menu.Items.Add(new MenuItem() { Glyph = Icon.GetIcon("SevenDotsIcon"), Text = "Map", NavigationDestination = typeof(TravelListItemMapsPage) });

            // Animate Menu.
            GridView.RegisterImplicitAnimations();
        }

        /// <summary>
        /// Highlights the (first) menu item that corresponds to the page.
        /// </summary>
        /// <param name="pageType">Type of the page.</param>
        public void SetTab(Type pageType)
        {
            // Lookup destination type in menu(s)
            var item = (from i in Menu.Items
                        where (i as MenuItem).NavigationDestination == pageType
                        select i).FirstOrDefault();
            if (item != null)
            {
                Menu.SelectedItem = item;
            }
            else
            {
                Menu.SelectedIndex = -1;
            }
        }

        private void Menu_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.First() is MenuItem menuItem && menuItem.IsNavigation)
            {
                Navigation.Navigate(menuItem.NavigationDestination);
            }
        }

        private void GridView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_itemsPanel == null)
            {
                return;
            }

            // Only react to change in Width.
            if (e.NewSize.Width != e.PreviousSize.Width)
            {
                AdjustItemsPanel();
            }
        }

        private void ItemsPanel_Loaded(object sender, RoutedEventArgs e)
        {
            // Avoid walking the Visual Tree on each Size change.
            _itemsPanel = sender as WrapGrid;

            // Initialize itemsPanel.
            AdjustItemsPanel();
        }

        private void AdjustItemsPanel()
        {
            if (ActualWidth > 800)
            {
                // Two rows.
                _itemsPanel.ItemWidth = ActualWidth / 2;
                _itemsPanel.MinWidth = ActualWidth;
            }
            else
            {
                // One row.
                _itemsPanel.ItemWidth = ActualWidth;
                _itemsPanel.Width = ActualWidth;
            }
        }
    }
}
