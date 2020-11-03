using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public class PointOfInterest
    {
        public string DisplayName { get; set; }
        public Geopoint Location { get; set; }
        public Uri ImageSourceUri { get; set; }
        public Point NormalizedAnchorPoint { get; set; }
    }
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TravelListItemMapsPage : Page
    {
        public TravelListItemMapsPage()
        {
            this.InitializeComponent();
            _pinUri = new Uri("ms-appx:///Assets/MapPin.png");
            _pointOfInterests = new List<PointOfInterest>();
            _addPointMode = false;
            _removePointMode = false;
            SaveIcon = new ButtonItem() { Glyph = Icon.GetIcon("Save"), Text = "Save" };
            AddPointIcon = new ButtonItem() { Glyph = Icon.GetIcon("Pin"), Text = "Pin" };
            RemovePointIcon = new ButtonItem() { Glyph = Icon.GetIcon("Clear"), Text = "Clear" };
        }

        public ButtonItem SaveIcon { get; set; }
        public ButtonItem AddPointIcon { get; set; }
        public ButtonItem RemovePointIcon { get; set; }
        private Boolean _addPointMode { get; set; }
        private Boolean _removePointMode { get; set; }
        private Uri _pinUri { get; set; }
        private List<PointOfInterest> _pointOfInterests { get; set; }
        private PointOfInterest _selectedPointOfInterests { get; set; }

        public TravelListItemViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == (int)e.Parameter).First();
            Menu.SetModel(ViewModel);
            // Send page type to menu.
            Menu.SetTab(GetType());
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
            foreach (TravelPointOfInterest point in ViewModel.Points)
            {
                _pointOfInterests.Add(
                    new PointOfInterest()
                    {
                        DisplayName = point.Name,
                        ImageSourceUri = _pinUri,
                        NormalizedAnchorPoint = new Point(0.5, 1),
                        Location = new Geopoint(new BasicGeoposition()
                        {
                            Latitude = (double)point.Latitude,
                            Longitude = (double)point.Longitude
                        })
                    }
                );
            }
            MapItems.ItemsSource = new List<PointOfInterest>();
            MapItems.ItemsSource = _pointOfInterests;
        }

        private void MapUserTapped(MapControl sender, MapInputEventArgs args)
        {
            _removePointMode = false;
            RemovePointCommandButton.Foreground = ((SolidColorBrush)Application.Current.Resources["PageForegroundBrush"]);
            _selectedPointOfInterests = null;
            if (!_addPointMode) { return; }

            //to get a basicgeoposition of wherever the user clicks on the map
            BasicGeoposition basgeo_edit_position = args.Location.Position;

            _pointOfInterests.Add(
                new PointOfInterest()
                {
                    DisplayName = "Place One",
                    ImageSourceUri = _pinUri,
                    NormalizedAnchorPoint = new Point(0.5, 1),
                    Location = new Geopoint(new BasicGeoposition()
                    {
                        Latitude = basgeo_edit_position.Latitude,
                        Longitude = basgeo_edit_position.Longitude
                    })
                }
                );
            MapItems.ItemsSource = new List<PointOfInterest>();
            MapItems.ItemsSource = _pointOfInterests;
        }

        private void AddPointAppBar_Click(object sender, RoutedEventArgs e)
        {
            _addPointMode = !_addPointMode;
            _removePointMode = false;
            _selectedPointOfInterests = null;
            if (_addPointMode)
            {
                AddPointCommandButton.Foreground = ((SolidColorBrush)Application.Current.Resources["ActionBrush"]);
            } else
            {
                AddPointCommandButton.Foreground = ((SolidColorBrush)Application.Current.Resources["PageForegroundBrush"]);
            }
            
        }

        private void RemovePointAppBar_Click(object sender, RoutedEventArgs e)
        {
            if (_removePointMode && _selectedPointOfInterests != null)
            {
                _removePointMode = false;
                RemovePointCommandButton.Foreground = ((SolidColorBrush)Application.Current.Resources["PageForegroundBrush"]);
                _pointOfInterests.Remove(_selectedPointOfInterests);
                MapItems.ItemsSource = new List<PointOfInterest>();
                MapItems.ItemsSource = _pointOfInterests;
                _selectedPointOfInterests = null;
            }
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

    }

}
