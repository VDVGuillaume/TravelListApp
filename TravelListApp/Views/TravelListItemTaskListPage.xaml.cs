using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TravelListApp.Models;
using TravelListApp.ViewModels;
using TravelListModels;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelListApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TravelListItemTasklistPage : Page
    {

        public TravelListItemViewModel ViewModel { get; set; }        
        private ObservableCollection<TravelTaskListItem> ObservableTaskListItems;

        public TravelListItemTasklistPage()
        {
            this.InitializeComponent();           

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == (int)e.Parameter).First();

            Menu.SetModel(ViewModel);
            Menu.SetTab(GetType());
            base.OnNavigatedTo(e);

            LoadItems();
           
        }

        private async void LoadItems()
        {
            ObservableTaskListItems = new ObservableCollection<TravelTaskListItem>();
            List<TravelTaskListItem> Items = await ViewModel.GetTravelTaskListItems();            
            foreach (TravelTaskListItem item in Items)
            {
                ObservableTaskListItems.Add(item);
            }
            LoadProgress();
        }



        private void LoadProgress()
        {
            Progress.Maximum = ObservableTaskListItems.Count;
            Progress.Value = ObservableTaskListItems.Where(x => x.Checked).Count();
        }

        


        private async void AddItem(object sender, RoutedEventArgs e)
        {

            TravelTaskListItem taskListItem = new TravelTaskListItem() { Name = NewItem.Text, Checked = (bool)NewCheck.IsChecked };
            taskListItem = await ViewModel.SaveTasklistAsync(taskListItem);                        
            ObservableTaskListItems.Add(taskListItem);
            LoadProgress();           

        }
      

        private async void DeleteItem(object sender, RoutedEventArgs e)
        {

            Button btn = sender as Button;
            TravelTaskListItem itemToDelete = (TravelTaskListItem)btn.DataContext;
          
            await ViewModel.DeleteTasklistAsync(itemToDelete);
            ObservableTaskListItems.Remove(itemToDelete);
            LoadProgress();
        }



        private async void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (this.TaskListTable.IsLoaded)
            {
                CheckBox cbx = sender as CheckBox;
                TravelTaskListItem ItemToUpdate = (TravelTaskListItem)cbx.DataContext;
                ItemToUpdate.Checked = (bool)cbx.IsChecked;
                ObservableTaskListItems.Where(x => x.TravelTaskListItemID == ItemToUpdate.TravelTaskListItemID).ToList().ForEach(x => x.Checked = (bool)cbx.IsChecked);
                LoadProgress();
                await ViewModel.UpdateTasklistAsync(ItemToUpdate);
            }
        }


    }

}