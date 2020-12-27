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
        public ButtonItem AddItem { get; set; }
        public ButtonItem RemoveItem { get; set; }       
        private List<CheckListItem> _checkListItems { get; set; }

        public TravelListItemChecklistPage()
        {
            this.InitializeComponent();
            _checkListItems = new List<CheckListItem>();            
            AddItem = new ButtonItem() { Glyph = Icon.GetIcon("Pin"), Text = "Pin" };
            RemoveItem = new ButtonItem() { Glyph = Icon.GetIcon("Clear"), Text = "Clear" };
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


        private void AddItems(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = new CheckBox();
            CheckListItem checkListItem = new CheckListItem();
            checkListItem.Name = checkListItemInput.Text;
            _checkListItems.Add(checkListItem);

            checkList.Items.Add(checkbox);
            checkbox.Content = checkListItem.Name;

        }

        private void RemoveItems()
        {

        }


        private void CheckList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       
    }
      
}
