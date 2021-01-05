using System;
using System.Linq;
using TravelListApp.Controls;
using TravelListApp.Services.Icons;
using TravelListApp.Services.Navigation;
using TravelListApp.ViewModels;
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

        private TravelListItemViewModel _model;

        public TravelListItemMenu()
        {
            this.InitializeComponent();

            // Populate Menu.
            TravelListMenu.Items.Add(new MenuItem() { Glyph = Icon.GetIcon("View"), Text = "View", NavigationDestination = typeof(TravelListItemPage) });
            TravelListMenu.Items.Add(new MenuItem() { Glyph = Icon.GetIcon("Edit"), Text = "Edit", NavigationDestination = typeof(TravelListItemEditPage) });
            TravelListMenu.Items.Add(new MenuItem() { Glyph = Icon.GetIcon("CheckList"), Text = "Checklist", NavigationDestination = typeof(TravelListItemChecklistPage) });
            TravelListMenu.Items.Add(new MenuItem() { Glyph = Icon.GetIcon("Map"), Text = "Places", NavigationDestination = typeof(TravelListItemPlacesPage) });
            TravelListMenu.Items.Add(new MenuItem() { Glyph = Icon.GetIcon("Route"), Text = "Routes", NavigationDestination = typeof(TravelListItemRoutesPage) });

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
            var item = (from i in TravelListMenu.Items
                        where (i as MenuItem).NavigationDestination == pageType
                        select i).FirstOrDefault();
            if (item != null)
            {
                TravelListMenu.SelectedItem = item;
            }
            else
            {
                TravelListMenu.SelectedIndex = -1;
            }
        }

        public void SetModel(TravelListItemViewModel model)
        {
            _model = model;
            if (_model.TravelListItemID > 0)
            {
                foreach (MenuItem item in TravelListMenu.Items)
                {
                    item.IsActive = true;
                }
            } else
            {
                foreach (MenuItem item in TravelListMenu.Items)
                {
                    if (item.Text == "Edit")
                    {
                        item.IsActive = true;
                    } else
                    {
                        item.IsActive = false;
                    }
                    
                }
            }

        }

        private void Menu_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is MenuItem menuItem && menuItem.IsNavigation)
            {
                Navigation.Navigate(menuItem.NavigationDestination, _model.TravelListItemID);
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
