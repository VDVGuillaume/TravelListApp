using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace TravelListApp.ViewModels
{
    public class TravelListViewModel : BindableBase
    {

        // public ObservableCollection<TravelListByCountry> Items { get; set; }

        public MainViewModel ViewModel => App.ViewModel;

        public TravelListViewModel()
        {
            getTravelListsItemsByCountry();
            ViewModel.TravelListItems.CollectionChanged += Name_CollectionChanged;
        }

        public async Task RefreshData()
        {
            IsLoading = true;
            await App.ViewModel.GetAllDataTravelListAsync();
            getTravelListsItemsByCountry();
            //ViewModel.TravelListItems.CollectionChanged += Name_CollectionChanged;
            IsLoading = false;
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading ;
            set
            {
                SetProperty(ref _isLoading, value);
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        private ObservableCollection<TravelListByCountry> _Items;

        public ObservableCollection<TravelListByCountry> Items
        {
            get => _Items;
            set
            {
                SetProperty(ref _Items, value);
                OnPropertyChanged(nameof(Items));
            }
        }

        private void Name_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
             getTravelListsItemsByCountry();
        }

        public void getTravelListsItemsByCountry()
        {
            var travelListsByCountry = ViewModel.TravelListItems.OrderBy(x => x.Country).GroupBy(x => x.Country)
                .Select(x => new TravelListByCountry { Name = x.Key, Items = new ObservableCollection<TravelListItemViewModel>(x.ToList()) });

            Items = new ObservableCollection<TravelListByCountry>(travelListsByCountry.ToList());
        }

        public void GetFilterTravelListsItemsByCountry(System.Collections.Generic.IEnumerable<TravelListItemViewModel> filter)
        {
            var travelListsByCountry = filter.OrderBy(x => x.Country).GroupBy(x => x.Country)
                .Select(x => new TravelListByCountry { Name = x.Key, Items = new ObservableCollection<TravelListItemViewModel>(x.ToList()) });

            Items = new ObservableCollection<TravelListByCountry>(travelListsByCountry.ToList());
        }

        public void SearchTravelListsItems(string search)
        {
            var travelLists = ViewModel.TravelListItems
                .Where(w => 
                w.Name.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0 | 
                w.Description.IndexOf(search, StringComparison.OrdinalIgnoreCase) >= 0);
            GetFilterTravelListsItemsByCountry(travelLists);
        }

    }

    public class TravelListByCountry
    {
        public string Name { get; set; }
        public ObservableCollection<TravelListItemViewModel> Items { get; set; }
    }
}
