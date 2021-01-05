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

        public TravelListItemViewModel viewModel { get; set; }    
        private List<CheckListItem> _checkListItems { get; set; }

        public TravelListItemChecklistPage()
        {
            this.InitializeComponent();
            _checkListItems = new List<CheckListItem>();
            ShowDeleteItem();
            
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            viewModel = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == (int)e.Parameter).First();
            Menu.SetModel(viewModel);
            // Send page type to menu.
            Menu.SetTab(GetType());
            base.OnNavigatedTo(e);
        }

        // saving when leaving view


        private async void AddItems(object sender, RoutedEventArgs e)
        {
            CheckBox checkbox = new CheckBox();
            CheckListItem checkListItem = new CheckListItem();
            checkListItem.Name = checkListItemInput.Text;
            _checkListItems.Add(checkListItem);
            
            checkList.Items.Add(checkbox);
            checkbox.Content = checkListItem;

            ShowDeleteItem();

            await viewModel.SaveAsync();

        }

        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            List<CheckBox> checkboxList = checkList.SelectedItems.Cast<CheckBox>().ToList();

            foreach(CheckBox checkBox in checkboxList)
            {
                checkList.Items.Remove(checkBox);
            }

           // CheckListItem checkListItem = (CheckListItem) checkbox.Content;           
            // checkList.Items.Remove(checkList.SelectedItem);
            ShowDeleteItem();
        }

        private void ShowDeleteItem()
        {
            deleteItem.Visibility = checkList.Items.Count > 0 ?  Visibility.Visible : Visibility.Collapsed;            
        }

        private void CheckList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

       
    }
      
}
