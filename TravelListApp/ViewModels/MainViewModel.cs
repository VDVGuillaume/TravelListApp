using Microsoft.Toolkit.Uwp.Helpers;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
            Task.Run(GetTravelListListAsync);
            Task.Run(GetCountriesAsync);
        }

        public string MapServiceToken { get; set; }

        // public ObservableCollection<TravelListItemViewModel> TravelListItems { get; set; }

        ObservableCollection<TravelListItemViewModel> _travelListItemViewModel = new ObservableCollection<TravelListItemViewModel>();
        ObservableCollection<TravelListItemImageViewModel> _travelListItemImageViewModel = new ObservableCollection<TravelListItemImageViewModel>();

        public ObservableCollection<TravelListItemViewModel> TravelListItems
        {
            get
            {
                return _travelListItemViewModel;
            }
            set
            {
                if (_travelListItemViewModel != value)
                {
                    _travelListItemViewModel = value;
                    OnPropertyChanged(nameof(TravelListItems));
                }
            }
        }

        public ObservableCollection<TravelListItemImageViewModel> TravelListImages
        {
            get
            {
                return _travelListItemImageViewModel;
            }
            set
            {
                if (_travelListItemImageViewModel != value)
                {
                    _travelListItemImageViewModel = value;
                    OnPropertyChanged(nameof(TravelListImages));
                }
            }
        }

        public ObservableCollection<Country> Countries { get; set; }

        private TravelListItemViewModel _selectedTravelList;

        /// <summary>
        /// Gets or sets the selected customer, or null if no customer is selected. 
        /// </summary>
        public TravelListItemViewModel SelectedTravelList
        {
            get => _selectedTravelList;
            set => SetProperty(ref _selectedTravelList, value);
        }

        private bool _isLoading = false;

        public bool IsLoading
        {
            get => _isLoading;
            set => SetProperty(ref _isLoading, value);
        }

        public async Task GetTravelListListAsync()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() => IsLoading = true);

            TravelListItems = new ObservableCollection<TravelListItemViewModel>();

            var travelLists = await App.Repository.TravelLists.GetAllTravelLists();
            if (travelLists == null)
            {
                return;
            }

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                TravelListItems.Clear();
                foreach (var c in travelLists)
                {
                    TravelListItems.Add(new TravelListItemViewModel(c));
                }
                IsLoading = false;
            });
        }

        public async Task GetCountriesAsync()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() => IsLoading = true);

            Countries = new ObservableCollection<Country>();
            var countries = await App.Repository.Countries.GetAllCountries();
            if (countries == null)
            {
                return;
            }

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Countries.Clear();
                foreach (var c in countries)
                {
                    Countries.Add(c);
                }
                IsLoading = false;
            });

        }
    }
}
