using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TravelListApp.Mvvm;
using TravelListApp.Services.Icons;
using TravelListApp.ViewModels;
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
    public sealed partial class TravelListItemEditPage : Page
    {
        public TravelListItemEditPage()
        {
            this.InitializeComponent();
            SaveIcon = new ButtonItem() { Glyph = Icon.GetIcon("Save"), Text = "Save" };
        }

        public TravelListItemViewModel ViewModel { get; set; }
        public ButtonItem SaveIcon { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                ViewModel = new TravelListItemViewModel
                {
                    IsNewTravelList = true,
                    IsInEdit = true
                };
            }
            else
            {
                ViewModel = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == (int)e.Parameter).First();
                ViewModel.StartEdit();
            }
            Menu.SetModel(ViewModel);
            Menu.SetTab(GetType());
            base.OnNavigatedTo(e);
        }

        private async void SaveAppBar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.SaveAsync();
        }
    }


}
