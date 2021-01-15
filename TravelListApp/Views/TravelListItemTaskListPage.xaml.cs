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
        private ObservableCollection<TaskListItem> ObservableTaskListItems;

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
            LoadProgress();
        }

        private async void LoadItems()
        {
            ObservableTaskListItems = new ObservableCollection<TaskListItem>();
            List<TravelTaskListItem> Items = await ViewModel.GetTravelTaskListItems();
            Progress.Maximum = Items.Count;
            foreach (TravelTaskListItem item in Items)
            {               
                ObservableTaskListItems.Add(new TaskListItem()
                {
                    Name = item.Name,                   
                    TravelTaskListItemID = item.TravelTaskListItemID,
                    TravelListItemID = item.TravelListItemID
                });
            }
        }



        private void LoadProgress()
        {
            Progress.Maximum = ObservableTaskListItems.Count;
            Progress.Value = ObservableTaskListItems.Where(x => x.Checked).Count();
        }

        


        private async void AddItem(object sender, RoutedEventArgs e)
        {


            TaskListItem taskListItem = new TaskListItem() { Name = NewItem.Text, Checked = (bool)NewCheck.IsChecked };
            taskListItem.IsNew = true;
            try
            {
                await ViewModel.SaveTasklistAsync(taskListItem);
            }
            catch { }


            ObservableTaskListItems.Add(taskListItem);
            LoadProgress();
            taskListItem.IsNew = false;

        }
      

        private async void DeleteItem(object sender, RoutedEventArgs e)
        {

            Button btn = sender as Button;
            TaskListItem itemToDelete = (TaskListItem)btn.DataContext;
            itemToDelete.ToRemove = true;

            await ViewModel.SaveTasklistAsync(itemToDelete);
            ObservableTaskListItems.Remove(itemToDelete);
            LoadProgress();
        }



        private async void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (this.TaskListTable.IsLoaded)
            {
                CheckBox cbx = sender as CheckBox;
                TaskListItem ItemToUpdate = (TaskListItem)cbx.DataContext;
                ItemToUpdate.Checked = (bool)cbx.IsChecked;
                ObservableTaskListItems.Where(x => x.TravelTaskListItemID == ItemToUpdate.TravelTaskListItemID).ToList().ForEach(x => x.Checked = (bool)cbx.IsChecked);
                LoadProgress();
                await ViewModel.SaveTasklistAsync(ItemToUpdate);
            }
        }


    }

}