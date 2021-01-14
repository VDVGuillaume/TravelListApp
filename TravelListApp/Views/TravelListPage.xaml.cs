using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TravelListApp.Services.Icons;
using TravelListApp.Services.Navigation;
using TravelListApp.ViewModels;
using Windows.UI.Xaml;
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
        public ButtonItem SettingsIcon = new ButtonItem() { Glyph = Icon.GetIcon("Settings"), Text = "Prefs" };
        public ButtonItem AddIcon = new ButtonItem() { Glyph = Icon.GetIcon("Add"), Text = "Add" };
        public static TravelListViewModel ViewModel { get; } = new TravelListViewModel();
        public TravelListPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        public PrefItem SelectedPref
        {
            get => ViewModel.SelectedPref;
            set
            {
                ViewModel.SelectedPref = value;
            }
        }

        public ObservableCollection<PrefItem> Prefs
        {
            get => ViewModel.Prefs;
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            await ViewModel.RefreshData();
            // groupedItemsViewSource.Source = ViewModel.Items;
            var collectionGroups = groupedItemsViewSource.View.CollectionGroups;
            ((ListViewBase)this.Zoom.ZoomedOutView).ItemsSource = collectionGroups;
        }

        private void AddButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Navigation.Navigate(typeof(TravelListItemEditPage));
        }

        //private void Pref_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    var prefItem = e.ClickedItem as PrefItem;
        //    ViewModel.GetTravelListsItemsGroupedByParam();
        //    var collectionGroups = groupedItemsViewSource.View.CollectionGroups;
        //    ((ListViewBase)this.Zoom.ZoomedOutView).ItemsSource = collectionGroups;
        //}

        private void Pref_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var listView = sender as ListView;
            if (listView.SelectedItem != null)
            {
                ViewModel.GetTravelListsItemsGroupedByParam();
                var collectionGroups = groupedItemsViewSource.View.CollectionGroups;
                ((ListViewBase)this.Zoom.ZoomedOutView).ItemsSource = collectionGroups;
            }
        }

        private void GoToButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var button = sender as Button;
            var TravelListItemID = button.Tag;
            Navigation.Navigate(typeof(TravelListItemPage), TravelListItemID);
        }

        /// <summary>
        /// Initializes the AutoSuggestBox portion of the search box.
        /// </summary>
        private void TravelSearchBox_Loaded(object sender, RoutedEventArgs e)
        {
            if (TravelSearchBox != null)
            {
                TravelSearchBox.AutoSuggestBox.Text = ViewModel.Search;
                TravelSearchBox.AutoSuggestBox.QuerySubmitted += TravelSearchBox_QuerySubmitted;
                TravelSearchBox.AutoSuggestBox.TextChanged += TravelSearchBox_TextChanged;
                TravelSearchBox.AutoSuggestBox.PlaceholderText = "Search travellist...";
            }
        }

        /// <summary>
        /// Filters the customer list based on the search text.
        /// </summary>
        private void TravelSearchBox_QuerySubmitted(AutoSuggestBox sender,
            AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (String.IsNullOrEmpty(args.QueryText))
            {
                ViewModel.Search = "";
                return;
            }
            else
            {
                ViewModel.Search = args.QueryText;
                ViewModel.GetTravelListsItemsGroupedByParam();
                var collectionGroups = groupedItemsViewSource.View.CollectionGroups;
                ((ListViewBase)this.Zoom.ZoomedOutView).ItemsSource = collectionGroups;
            }
        }

        /// <summary>
        /// Updates the search box items source when the user changes the search text.
        /// </summary>
        private void TravelSearchBox_TextChanged(AutoSuggestBox sender,
            AutoSuggestBoxTextChangedEventArgs args)
        {
            // We only want to get results when it was a user typing,
            // otherwise we assume the value got filled in by TextMemberPath
            // or the handler for SuggestionChosen.
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                // If no search query is entered, refresh the complete list.
                if (String.IsNullOrEmpty(sender.Text))
                {
                    ViewModel.Search = "";
                    ViewModel.GetTravelListsItemsGroupedByParam();
                    var collectionGroups = groupedItemsViewSource.View.CollectionGroups;
                    ((ListViewBase)this.Zoom.ZoomedOutView).ItemsSource = collectionGroups;
                }
                //else
                //{
                //    ViewModel.SearchTravelListsItems((sender.Text);
                //}
            }
        }
    }
}
