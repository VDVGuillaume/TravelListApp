using System.Threading.Tasks;
using TravelListApp.Services.Navigation;
using TravelListApp.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelListApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TravelListPage
    {
        public static TravelListViewModel ViewModel { get; } = new TravelListViewModel();
        public TravelListPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            MyProgressGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
            await ViewModel.RefreshData();
            groupedItemsViewSource.Source = ViewModel.Items;
            var collectionGroups = groupedItemsViewSource.View.CollectionGroups;
            ((ListViewBase)this.Zoom.ZoomedOutView).ItemsSource = collectionGroups;
            MyProgressGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }

        private void AddButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Navigation.Navigate(typeof(TravelListItemEditPage));
        }

        private void GoToButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var button = sender as Button;
            var TravelListItemID = button.Tag;
            Navigation.Navigate(typeof(TravelListItemPage), TravelListItemID);
        }
    }
}
