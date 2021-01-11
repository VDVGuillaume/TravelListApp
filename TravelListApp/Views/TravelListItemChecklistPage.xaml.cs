﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TravelListApp.Models;
using TravelListApp.ViewModels;
using TravelListModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
        private ObservableCollection<CheckListItem> ObservablecheckListItems = new ObservableCollection<CheckListItem>();

        public TravelListItemChecklistPage()
        {
            this.InitializeComponent();

            ListOfCategories = new ObservableCollection<string>()
            {
                new Category("Add New Category").Name
            };
            NewCategory.ItemsSource = ListOfCategories;

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == (int)e.Parameter).First();

            Menu.SetModel(ViewModel);
            Menu.SetTab(GetType());
            base.OnNavigatedTo(e);

            foreach (var item in ViewModel.Items)
            {
                CheckListItem checkListItem = new CheckListItem() { Name = item.Name, Amount = item.Amount, Checked = item.Checked, Category = item.Category, TravelCheckListItemID = item.TravelCheckListItemID, TravelListItemID = item.TravelListItemID };
                ObservablecheckListItems.Add(checkListItem);
            }

            LoadCategories();

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



        private async void AddItem(object sender, RoutedEventArgs e)
        {

            Category category = new Category(NewCategory.Text);

            if (!ListOfCategories.Contains(category.Name))
            {
                ListOfCategories.Add(category.Name);
                category.UserId = LoginPage.account.Id;
                await ViewModel.SaveCategoryAsync(category);
            }

            CheckListItem checkListItem = new CheckListItem() { Name = NewItem.Text, Amount = Convert.ToInt32(NewAmount.Text), Checked = (bool)NewCheck.IsChecked, Category = category.Name };
            checkListItem.IsNew = true;            
            await ViewModel.SaveChecklistAsync(checkListItem);
            ObservablecheckListItems.Add(checkListItem);
            checkListItem.IsNew = false;

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
        }



        private async void CheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (this.CheckListTable.IsLoaded)
            {
                CheckBox cbx = sender as CheckBox;
                CheckListItem ItemToUpdate = (CheckListItem)cbx.DataContext;
                ItemToUpdate.Checked = (bool)cbx.IsChecked;                
                await ViewModel.SaveChecklistAsync(ItemToUpdate);
            }
        }


    }

}