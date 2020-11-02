using Microsoft.Toolkit.Uwp.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListApp.Mvvm;
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

        // public ObservableCollection<TravelListItemViewModel> TravelListItems { get; set; }

        ObservableCollection<TravelListItemViewModel> _travelListItemViewModel = new ObservableCollection<TravelListItemViewModel>();

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
