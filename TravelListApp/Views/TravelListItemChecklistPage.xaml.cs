using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelListApp.Services.Icons;
using TravelListApp.ViewModels;
using TravelListModels;
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
    public sealed partial class TravelListItemChecklistPage : Page
    {

        public TravelListItemViewModel ViewModel { get; set; }
        public ButtonItem SaveIcon { get; set; }
        public ButtonItem AddItemIcon { get; set; }
        public ButtonItem RemoveItemIcon { get; set; }
        private List<CheckListItem> _checkListItems { get; set; }

        public TravelListItemChecklistPage()
        {
            this.InitializeComponent();
            _checkListItems = new List<CheckListItem>();
            SaveIcon = new ButtonItem() { Glyph = Icon.GetIcon("Save"), Text = "Save" };
            AddItemIcon = new ButtonItem() { Glyph = Icon.GetIcon("Pin"), Text = "Pin" };
            RemoveItemIcon = new ButtonItem() { Glyph = Icon.GetIcon("Clear"), Text = "Clear" };
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == (int)e.Parameter).First();
            Menu.SetModel(ViewModel);
            // Send page type to menu.
            Menu.SetTab(GetType());
            base.OnNavigatedTo(e);
        }

        // saving when leaving view


        private void AddItems()
        {
           
        }

        private void RemoveItems()
        {

        }

        private void CheckItem()
        {

        }

        private void ListBox_GettingFocus(UIElement sender, GettingFocusEventArgs args)
        {

        }
    }
      
}
