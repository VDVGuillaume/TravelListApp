using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelListApp.Models;
using TravelListApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelListApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TravelListItemPage : Page
    {
        public TravelListItemPage()
        {
            this.InitializeComponent();
            CarouselControl.ItemsSource = cImages;
            Size s = App.ViewModel.GetCurrentViewSize();
            CarouselControl.Height = s.Height/3;
        }

        public TravelListItemViewModel ViewModel { get; set; }

        public ObservableCollection<CarouselImage> cImages = new ObservableCollection<CarouselImage>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == (int)e.Parameter).First();
            foreach (var item in ViewModel.convertedImages)
            {
                cImages.Add(item);
            }
            // Send page model to menu.
            Menu.SetModel(ViewModel);
            // Send page type to menu.
            Menu.SetTab(GetType());
            base.OnNavigatedTo(e);
        }
    }
}
