using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using TravelListApp.Models;
using TravelListApp.ViewModels;
using TravelListModels;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
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
        public CheckBox Checkbox;
        public ObservableCollection<string> ListOfCategories { get; set; }
        private ObservableCollection<CheckListItem> ObservablecheckListItems;
        private static readonly Regex _regex = new Regex("[^0-9.-]+");

        public TravelListItemChecklistPage()
        {
            this.InitializeComponent();

            ListOfCategories = new ObservableCollection<string>()
            {
                new Category("Add New Category").Name
            };
            NewCategory.ItemsSource = ListOfCategories;

           

        }

        private void OrderByCategory(object sender, RoutedEventArgs e)
        {


        }
              
        private bool NumberCheck(string text)
        {
           return !_regex.IsMatch(text);
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == (int)e.Parameter).First();

            Menu.SetModel(ViewModel);
            Menu.SetTab(GetType());
            base.OnNavigatedTo(e);           

            LoadCategories();
            LoadItems();
            LoadProgress();
           

        }

        private async void LoadItems()
        {
            ObservablecheckListItems = new ObservableCollection<CheckListItem>();
            List<TravelCheckListItem> Items = await ViewModel.GetTravelCheckListItems();           
            foreach (TravelCheckListItem item in Items)
            {
               
                ObservablecheckListItems.Add(new CheckListItem() { Name = item.Name, Amount = item.Amount, Category = item.Category,
                    Checked = item.Checked, TravelCheckListItemID = item.TravelCheckListItemID,TravelListItemID = item.TravelListItemID });
            }
        }


        private async void LoadCategories()
        {
            List<Category> categories = await ViewModel.GetCategoriesAsync();

            if (categories.Count > 0)
                foreach (Category category in categories)
                {
                    ListOfCategories.Add(category.Name);
                }

        }

        private void LoadProgress()
        {
            Progress.Maximum = ObservablecheckListItems.Count;
            Progress.Value = ObservablecheckListItems.Where(x => x.Checked).Count();
        }




        private async void AddItem(object sender, RoutedEventArgs e)
        {

            Category category = new Category(NewCategory.Text);

            if (!ListOfCategories.Contains(category.Name))
            {
                ListOfCategories.Add(category.Name);
                category.UserId = LoginPage.account.Id;
                await ViewModel.SaveCategoryAsync(category);
            }

            if (NumberCheck(NewAmount.Text))
            {

                CheckListItem checkListItem = new CheckListItem() { Name = NewItem.Text, Amount = Convert.ToInt32(NewAmount.Text), Checked = (bool)NewCheck.IsChecked, Category = category.Name };
                checkListItem.IsNew = true;
                try
                {
                    await ViewModel.SaveChecklistAsync(checkListItem);
                }
                catch { }
                ObservablecheckListItems.Add(checkListItem);
                LoadProgress();
                checkListItem.IsNew = false;
            }

            else { ErrorLabel.Text = "Only numbers are allowed in the amount field, please try again."; };

        }

        private void AddCategory(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems[0].ToString() == "Add New Category")
                NewCategory.IsEditable = true;
            else
                NewCategory.IsEditable = false;

        }


        private async void DeleteItem(object sender, RoutedEventArgs e)
        {

            Button btn = sender as Button;
            CheckListItem itemToDelete = (CheckListItem)btn.DataContext;
            itemToDelete.ToRemove = true;

            await ViewModel.SaveChecklistAsync(itemToDelete);
            ObservablecheckListItems.Remove(itemToDelete);
            LoadProgress();
        }



        private async void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (this.CheckListTable.IsLoaded)
            {
                CheckBox cbx = sender as CheckBox;
                CheckListItem ItemToUpdate = (CheckListItem)cbx.DataContext;
                ItemToUpdate.Checked = (bool)cbx.IsChecked;
                ObservablecheckListItems.Where(x => x.TravelCheckListItemID == ItemToUpdate.TravelCheckListItemID).ToList().ForEach(x => x.Checked = (bool)cbx.IsChecked);
                LoadProgress();
                await ViewModel.SaveChecklistAsync(ItemToUpdate);
            }
        }

       
    }

}