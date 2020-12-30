using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TravelListApp.Models;
using TravelListApp.Services.Icons;
using TravelListApp.Services.Validation;
using TravelListApp.ViewModels;
using TravelListModels;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Graphics.Display;
using Windows.Services.Maps;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelListApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TravelListItemRoutesPage : Page
    {
        public TravelListItemRoutesPage()
        {
            this.InitializeComponent();
            _pointOfInterests = new List<PointOfInterest>();
            Errors = new ObservableUniqueCollection<string>();
            ErrorsList.ItemsSource = Errors;
            MapItemsListViewer.Height = 0;
        }

        public ButtonItem SaveIcon = new ButtonItem() { Glyph = Icon.GetIcon("Save"), Text = "Save" };
        public ButtonItem AddPointIcon = new ButtonItem() { Glyph = Icon.GetIcon("Pin"), Text = "Pin" };
        public ButtonItem RemovePointIcon = new ButtonItem() { Glyph = Icon.GetIcon("Clear"), Text = "Clear" };
        public ButtonItem ShowListIcon = new ButtonItem() { Glyph = Icon.GetIcon("List"), Text = "List" };
        public ButtonItem PathIcon = new ButtonItem() { Glyph = Icon.GetIcon("Path"), Text = "Path" };
        public ButtonItem AddIcon = new ButtonItem() { Glyph = Icon.GetIcon("Add"), Text = "Add" };
        private Boolean _addPointMode { get; set; }
        private Boolean _removePointMode { get; set; }
        private Uri _pinUri { get; set; }
        private List<PointOfInterest> _pointOfInterests { get; set; }
        private PointOfInterest _selectedPointOfInterests { get; set; }
        public ObservableUniqueCollection<string> Errors { get; set; }

        public PointOfInterest Start { get; set; }
        public PointOfInterest End { get; set; }

        public TravelListItemViewModel ViewModel { get; set; }

        private async void ShowRouteOnMap(RoutesOfPointOfInterest route)
        {

            // Start at Microsoft in Redmond, Washington.
            BasicGeoposition startLocation = new BasicGeoposition() { Latitude = (double)route.Start.Latitude, Longitude = (double)route.Start.Longitude };

            // End at the city of Seattle, Washington.
            BasicGeoposition endLocation = new BasicGeoposition() { Latitude = (double)route.End.Latitude, Longitude = (double)route.End.Longitude };

            // Get the route between the points.
            MapRouteFinderResult routeResult =
                  await MapRouteFinder.GetDrivingRouteAsync(
                  new Geopoint(startLocation),
                  new Geopoint(endLocation),
                  MapRouteOptimization.Time,
                  MapRouteRestrictions.None);

            if (routeResult.Status == MapRouteFinderStatus.Success)
            {
                // Use the route to initialize a MapRouteView.
                MapRouteView viewOfRoute = new MapRouteView(routeResult.Route);
                viewOfRoute.RouteColor = Colors.Yellow;
                viewOfRoute.OutlineColor = Colors.Black;

                // Assign viewOfRoute to Route object
                route.ViewOfRoute = viewOfRoute;

                // Add the new MapRouteView to the Routes collection
                // of the MapControl.
                myMap.Routes.Add(viewOfRoute);

                // Fit the MapControl to the route.
                await myMap.TrySetViewBoundsAsync(
                      routeResult.Route.BoundingBox,
                      null,
                      Windows.UI.Xaml.Controls.Maps.MapAnimationKind.None);
            }
        }

        private void RemoveRouteOnMap(RoutesOfPointOfInterest route)
        {

            // Add the new MapRouteView to the Routes collection
            // of the MapControl.
            myMap.Routes.Remove(route.ViewOfRoute);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == (int)e.Parameter).First();
            Menu.SetModel(ViewModel);
            // Send page type to menu.
            Menu.SetTab(GetType());
            myMap.MapServiceToken = App.ViewModel.MapServiceToken;
            Sync();
            base.OnNavigatedTo(e);
        }

        private void MyMap_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (RoutesOfPointOfInterest route in ViewModel.syncRoutes.Where(p => p.ToRemove == false))
            {
                if (route.Start != null && route.End != null)
                    ShowRouteOnMap(route);
            }
        }

        private void AddRoute_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            RoutesOfPointOfInterest newRoute = new RoutesOfPointOfInterest();
            newRoute.TravelListItemID = ViewModel.TravelListItemID;
            newRoute.StartTravelPointOfInterestID = Start.TravelPointOfInterestID;
            newRoute.Start = Start;
            newRoute.EndTravelPointOfInterestID = End.TravelPointOfInterestID;
            newRoute.End = End;
            newRoute.IsNew = true;
            ViewModel.syncRoutes.Add(newRoute);
            Sync();
            ShowRouteOnMap(newRoute);
        }

        private void RemoveRoute_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var button = sender as Button;
            Guid LocalId = (Guid)button.Tag;

            RoutesOfPointOfInterest rpoi = ViewModel.syncRoutes.Single(p => p.LocalId == LocalId);
            if (rpoi != null)
            {
                rpoi.ToRemove = true;
                RemoveRouteOnMap(rpoi);
            }
            Sync();
        }

        

        private void Sync()
        {
            MapItems.ItemsSource = ViewModel.syncPoints.FindAll(p => p.ToRemove == false);
            MapItemsList.ItemsSource = ViewModel.syncRoutes.Where(p => p.ToRemove == false);
            StartPoint.ItemsSource = ViewModel.syncPoints.FindAll(p => p.ToRemove == false);
            EndPoint.ItemsSource = ViewModel.syncPoints.FindAll(p => p.ToRemove == false);
        }


        private void MapItemsList_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var textbox = sender as TextBox;
            Guid LocalId = (Guid)textbox.Tag;
            PointOfInterest poi = ViewModel.syncPoints.Find(p => p.LocalId == LocalId);
            if (!poi.Name.Equals(textbox.Text))
            {
                poi.IsUpdate = true;
            }
        }

        public static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }


        private void ShowListAppBar_Click(object sender, RoutedEventArgs e)
        {
            if (MapItemsListViewer.Height == 0)
            {
                Size s = GetCurrentDisplaySize();
                MapItemsListViewer.Height = s.Height / 5;
            }
            else
            {
                MapItemsListViewer.Height = 0;
            }

        }

        private async void SaveAppBar_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveRoutesAsync();
        }

        public static Size GetCurrentDisplaySize()
        {
            var displayInformation = DisplayInformation.GetForCurrentView();
            TypeInfo t = typeof(DisplayInformation).GetTypeInfo();
            var props = t.DeclaredProperties.Where(x => x.Name.StartsWith("Screen") && x.Name.EndsWith("InRawPixels")).ToArray();
            var w = props.Where(x => x.Name.Contains("Width")).First().GetValue(displayInformation);
            var h = props.Where(x => x.Name.Contains("Height")).First().GetValue(displayInformation);
            var size = new Size(System.Convert.ToDouble(w), System.Convert.ToDouble(h));
            switch (displayInformation.CurrentOrientation)
            {
                case DisplayOrientations.Landscape:
                case DisplayOrientations.LandscapeFlipped:
                    size = new Size(Math.Max(size.Width, size.Height), Math.Min(size.Width, size.Height));
                    break;
                case DisplayOrientations.Portrait:
                case DisplayOrientations.PortraitFlipped:
                    size = new Size(Math.Min(size.Width, size.Height), Math.Max(size.Width, size.Height));
                    break;
            }
            return size;
        }

    }

}
