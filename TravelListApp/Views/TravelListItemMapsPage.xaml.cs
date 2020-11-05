using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelListApp.Models;
using TravelListApp.Mvvm;
using TravelListApp.Services.Icons;
using TravelListApp.ViewModels;
using TravelListModels;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
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
    public sealed partial class TravelListItemMapsPage : Page
    {
        public TravelListItemMapsPage()
        {
            this.InitializeComponent();
            _pointOfInterests = new List<PointOfInterest>();
            _addPointMode = false;
            _removePointMode = false;
            SaveIcon = new ButtonItem() { Glyph = Icon.GetIcon("Save"), Text = "Save" };
            AddPointIcon = new ButtonItem() { Glyph = Icon.GetIcon("Pin"), Text = "Pin" };
            RemovePointIcon = new ButtonItem() { Glyph = Icon.GetIcon("Clear"), Text = "Clear" };
            _placeNameTextBoxSize = SecondaryTileCommandBar.ActualWidth / 2;
        }

        public ButtonItem SaveIcon { get; set; }
        public ButtonItem AddPointIcon { get; set; }
        public ButtonItem RemovePointIcon { get; set; }
        private double _placeNameTextBoxSize { get; set; }
        private Boolean _addPointMode { get; set; }
        private Boolean _removePointMode { get; set; }
        private Uri _pinUri { get; set; }
        private List<PointOfInterest> _pointOfInterests { get; set; }
        private PointOfInterest _selectedPointOfInterests { get; set; }

        public TravelListItemViewModel ViewModel { get; set; }

        private void CommandBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (SecondaryTileCommandBar == null)
            {
                return;
            }

            // Only react to change in Width.
            if (e.NewSize.Width != e.PreviousSize.Width)
            {
                PlaceNameTextBox.Width = SecondaryTileCommandBar.ActualWidth / 2;
            }
        }        

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == (int)e.Parameter).First();
            Menu.SetModel(ViewModel);
            // Send page type to menu.
            Menu.SetTab(GetType());
            myMap.MapServiceToken = App.ViewModel.MapServiceToken;
            AddPoints();
            base.OnNavigatedTo(e);
        }

        private void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            Geopoint zoomPoint = new Geopoint(new BasicGeoposition() { Latitude = (double)ViewModel.Latitude, Longitude = (double)ViewModel.Longitude });
            myMap.Center = zoomPoint;
            myMap.ZoomLevel = 5;
        }

        private void AddPoints()
        {
            MapItems.ItemsSource = new List<PointOfInterest>();
            MapItems.ItemsSource = ViewModel.syncPoints.FindAll(p => p.ToRemove == false);
        }

        private void MapUserTapped(MapControl sender, MapInputEventArgs args)
        {
            _removePointMode = false;
            RemovePointCommandButton.Foreground = ((SolidColorBrush)Application.Current.Resources["PageForegroundBrush"]);
            _selectedPointOfInterests = null;
            if (!_addPointMode) { return; }

            //to get a basicgeoposition of wherever the user clicks on the map
            BasicGeoposition basgeo_edit_position = args.Location.Position;

            PointOfInterest newPoint =
            new PointOfInterest()
            {
                Name = PlaceNameTextBox.Text,
                ImageSourceUri = new Uri("ms-appx:///Assets/MapPin.png"),
                NormalizedAnchorPoint = new Point(0.5, 1),
                Latitude = (decimal)basgeo_edit_position.Latitude,
                Longitude = (decimal)basgeo_edit_position.Longitude,
                Location = new Geopoint(new BasicGeoposition()
                {
                    Latitude = (double)basgeo_edit_position.Latitude,
                    Longitude = (double)basgeo_edit_position.Longitude
                }),
                TravelListItemID = ViewModel.TravelListItemID,
                IsNew = true
            };

            ViewModel.syncPoints.Add(newPoint);
            AddPoints();
            PlaceNameTextBox.Text = "";
        }

        private void AddPointAppBar_Click(object sender, RoutedEventArgs e)
        {
            _addPointMode = !_addPointMode;
            _removePointMode = false;
            _selectedPointOfInterests = null;
            if (_addPointMode)
            {
                PlaceNameTextBox.IsEnabled = true;
                AddPointCommandButton.Foreground = ((SolidColorBrush)Application.Current.Resources["ActionBrush"]);
            } else
            {
                PlaceNameTextBox.IsEnabled = false;
                AddPointCommandButton.Foreground = ((SolidColorBrush)Application.Current.Resources["PageForegroundBrush"]);
            }
            
        }

        private void RemovePointAppBar_Click(object sender, RoutedEventArgs e)
        {
            if (_removePointMode && _selectedPointOfInterests != null)
            {
                _removePointMode = false;
                RemovePointCommandButton.Foreground = ((SolidColorBrush)Application.Current.Resources["PageForegroundBrush"]);
                _selectedPointOfInterests.ToRemove = true;
                AddPoints();
                _selectedPointOfInterests = null;
            }
        }

        private async void SaveAppBar_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SavePointsAsync();
        }

        private void mapItemButton_Click(object sender, RoutedEventArgs e)
        {
            var buttonSender = sender as Button;
            _selectedPointOfInterests = buttonSender.DataContext as PointOfInterest;
            _addPointMode = false;
            AddPointCommandButton.Foreground = ((SolidColorBrush)Application.Current.Resources["PageForegroundBrush"]);
            _removePointMode = true;
            RemovePointCommandButton.Foreground = ((SolidColorBrush)Application.Current.Resources["ActionBrush"]);
        }

        /// <summary>
        /// Initializes the AutoSuggestBox portion of the search box.
        /// </summary>
        private void BingSearchBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (BingSearchBox != null)
            {
                BingSearchBox.AutoSuggestBox.QuerySubmitted += BingSearchBox_QuerySubmitted;
                // BingSearchBox.AutoSuggestBox.TextChanged += BingSearchBox_TextChanged;
                BingSearchBox.AutoSuggestBox.PlaceholderText = "Search address...";
            }
        }

        /// <summary>
        /// Filters the customer list based on the search text.
        /// </summary>
        private async void BingSearchBox_QuerySubmitted(AutoSuggestBox sender,
            AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (String.IsNullOrEmpty(args.QueryText))
            {
                sender.ItemsSource = null;
            }
            else
            {
                Location location = await ViewModel.GetBingSearchResultsAsync(args.QueryText);
                List<Resources> resourceSets = location.resourceSets.ToList();
                List<Resource> resources = resourceSets[0].resources.OrderByDescending(r => r.confidence == "High")
                .ThenByDescending(r => r.confidence == "Medium")
                .ThenByDescending(r => r.confidence == "Low").ToList();
                List<string> names = resources.Select(x => x.name).ToList();
                sender.ItemsSource = names;

                if (resources.Count > 0)
                {
                    Geopoint zoomPoint = new Geopoint(new BasicGeoposition() { Latitude = (double)resources[0].point.coordinates[0], Longitude = (double)resources[0].point.coordinates[1] });
                    myMap.Center = zoomPoint;
                    myMap.ZoomLevel = 12;
                }

            }
        }

        ///// <summary>
        ///// Updates the search box items source when the user changes the search text.
        ///// </summary>
        //private async void BingSearchBox_TextChanged(AutoSuggestBox sender,
        //    AutoSuggestBoxTextChangedEventArgs args)
        //{
        //    // We only want to get results when it was a user typing,
        //    // otherwise we assume the value got filled in by TextMemberPath
        //    // or the handler for SuggestionChosen.
        //    if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        //    {
        //        // If no search query is entered, refresh the complete list.
        //        if (String.IsNullOrEmpty(sender.Text))
        //        {

        //            sender.ItemsSource = null;
        //        }
        //        else
        //        {
        //            await ViewModel.GetBingSearchResultsAsync(sender.Text);
        //            sender.ItemsSource = null;
                    
        //        }
        //    }
        //}

    }

}
