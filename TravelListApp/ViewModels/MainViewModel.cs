using Microsoft.Toolkit.Uwp.Helpers;
using System.Collections.ObjectModel;
using Windows.Foundation;
using System.Threading.Tasks;
using TravelListApp.Views;
using TravelListModels;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;

namespace TravelListApp.ViewModels
{
    public class MainViewModel : BindableBase
    {
        public MainViewModel()
        {
            //Task.Run(GetTravelListListAsync);
            //Task.Run(GetCountriesAsync);
        }

        public async Task GetAllDataAsync()
        {
            await Task.Run(GetCountriesAsync);
            await Task.Run(GetTravelListListAsync);
        }

        public async Task GetAllDataCountriesAsync()
        {
            await Task.Run(GetCountriesAsync);
        }

        public async Task GetAllDataTravelListAsync()
        {
            await Task.Run(GetTravelListListAsync);
        }

        public async Task GetFirstUpcomingAsync()
        {
            await Task.Run(GetFirstUpcomingTravelListAsync);
        }

        public string MapServiceToken { get; set; }

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
        /// Gets or sets the selected TravelList, or null if no TravelList is selected. 
        /// </summary>
        public TravelListItemViewModel SelectedTravelList
        {
            get => _selectedTravelList;
            set => SetProperty(ref _selectedTravelList, value);
        }

        private TravelListItemViewModel _firstUpcommingTravelList;
        /// <summary>
        /// Gets or sets the selected TravelList, or null if no TravelList is selected. 
        /// </summary>
        public TravelListItemViewModel FirstUpcommingTravelList
        {
            get => _firstUpcommingTravelList;
            set
            {
                SetProperty(ref _firstUpcommingTravelList, value);
                // OnPropertyChanged(nameof(FirstUpcommingTravelList));
            }
        }

        private bool _isLoading = false;

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                SetProperty(ref _isLoading, value);
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public void SetLoader()
        {
            if (IsLoading)
            {
               IsLoading = false;
            }
            else
            {
                IsLoading = true;
            }
        }

        private bool _isReady = false;

        public bool IsReady
        {
            get => _isReady;
            set => SetProperty(ref _isReady, value);
        }

        public async Task GetFirstUpcomingTravelListAsync()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() => IsLoading = true);

            var travelList = await App.Repository.TravelLists.GetFirstUpcomingTravelList(LoginPage.account.Id);
            if (travelList == null)
            {
                FirstUpcommingTravelList = null;
                await DispatcherHelper.ExecuteOnUIThreadAsync(() => IsLoading = false);
                return;
            }

            await DispatcherHelper.ExecuteOnUIThreadAsync(async () =>
            {
                var newModel = new TravelListItemViewModel(travelList);
                await newModel.ConvertImagesTask();
                FirstUpcommingTravelList = newModel;
                IsLoading = false;
            });
        }

        public async Task GetTravelListListAsync()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() => IsLoading = true);

            TravelListItems = new ObservableCollection<TravelListItemViewModel>();

            var travelLists = await App.Repository.TravelLists.GetAllTravelLists(LoginPage.account.Id);
            if (travelLists == null)
            {
                return;
            }

            await DispatcherHelper.ExecuteOnUIThreadAsync(async () =>
            {
                TravelListItems.Clear();
                foreach (var c in travelLists)
                {
                    var newModel = new TravelListItemViewModel(c);
                    await newModel.ConvertImagesTask();
                    TravelListItems.Add(newModel);
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
        public Size GetCurrentViewSize()
        {
            // Get the visible bounds for current view
            var visibleBounds = ApplicationView.GetForCurrentView().VisibleBounds;

            // Get the scale factor from display information
            var scaleFactor = DisplayInformation.GetForCurrentView().RawPixelsPerViewPixel;

            double newWidth = visibleBounds.Width * scaleFactor;

            var newHeight = visibleBounds.Height * scaleFactor;

            // Get the application screen size
            var size = new Size(newWidth, newHeight);
            return size;
        }


    }
}
