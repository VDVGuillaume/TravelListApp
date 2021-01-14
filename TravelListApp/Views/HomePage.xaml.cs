﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelListApp.Models;
using TravelListApp.Services.Navigation;
using TravelListApp.ViewModels;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelListApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        // public ThemeSelectionViewModel ThemeViewModel { get; } = App.ThemeViewModel;

        public HomePage()
        {
            this.InitializeComponent();
            ViewModel = App.ViewModel.FirstUpcommingTravelList;
            CarouselControl.ItemsSource = cImages;
            Size s = App.ViewModel.GetCurrentViewSize();
            CarouselControl.Height = s.Height / 5;
            // DescriptionTextBlock.Width = s.Width / 1.75;
        }

        public TravelListItemViewModel ViewModel { get; set; }
        public ObservableCollection<CarouselImage> cImages = new ObservableCollection<CarouselImage>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (ViewModel != null)
            {
                GridHasNoTravelListControl.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                GridHasTravelListControl.Visibility = Windows.UI.Xaml.Visibility.Visible;
                if (ViewModel.Items.Count > 0)
                {
                    ProgressCheck.Maximum = ViewModel.Items.Count;
                    ProgressCheck.Value = ViewModel.Items.Where(x => x.Checked).Count();
                }
                foreach (var item in ViewModel.convertedImages)
                {
                    cImages.Add(item);
                }
            } else
            {
                GridHasNoTravelListControl.Visibility = Windows.UI.Xaml.Visibility.Visible;
                GridHasTravelListControl.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

        }

        private async void GoToButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var button = sender as Button;
            var TravelListItemID = button.Tag;
            await App.ViewModel.GetAllDataTravelListAsync();
            Navigation.Navigate(typeof(TravelListItemPage), TravelListItemID);
        }

        private void CreateButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Navigation.Navigate(typeof(TravelListItemEditPage));
        }

        private void SeedButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
