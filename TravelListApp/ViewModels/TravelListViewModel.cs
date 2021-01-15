using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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

        public MainViewModel ViewModel => App.ViewModel;

        public TravelListViewModel()
        {
            Prefs = new ObservableCollection<PrefItem>();
            SelectedPref = new PrefItem { Name = "Country" };
            Prefs.Add(SelectedPref);
            Prefs.Add(new PrefItem { Name = "StartDate" });
            Search = "";
            GetTravelListsItemsGroupedByParam();
            ViewModel.TravelListItems.CollectionChanged += Name_CollectionChanged;
        }

        public async Task RefreshData()
        {
            IsLoading = true;
            await App.ViewModel.GetAllDataTravelListAsync();
            GetTravelListsItemsGroupedByParam();
            IsLoading = false;
        }

        private bool _isLoading;

        public bool IsLoading
        {
            get => _isLoading ;
            set
            {
                SetProperty(ref _isLoading, value);
            }
        }

        private string _search;

        public string Search
        {
            get => _search;
            set
            {
                SetProperty(ref _search, value);
            }
        }

        private ObservableCollection<PrefItem> _Prefs;

        public ObservableCollection<PrefItem> Prefs
        {
            get => _Prefs;
            set
            {
                SetProperty(ref _Prefs, value);
            }
        }

        private PrefItem _selectedPref;

        public PrefItem SelectedPref
        {
            get => _selectedPref;
            set
            {
                SetProperty(ref _selectedPref, value);
            }
        }

        private ObservableCollection<TravelListByParam> _Items;

        public ObservableCollection<TravelListByParam> Items
        {
            get => _Items;
            set
            {
                SetProperty(ref _Items, value);
            }
        }

        private void Name_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            GetTravelListsItemsGroupedByParam();
        }

        public void GetTravelListsItemsGroupedByParam()
        {
            var propertyInfo = typeof(TravelListItemViewModel).GetProperty(SelectedPref.Name);

            var travelListsSearch = ViewModel.TravelListItems
                .Where(w =>
                w.Name.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0 |
                w.Description.IndexOf(Search, StringComparison.OrdinalIgnoreCase) >= 0);

            IEnumerable<TravelListByParam> travelListsByParam;
            if (propertyInfo.PropertyType == typeof(System.DateTime))
            {
                travelListsByParam = travelListsSearch.OrderBy(x => propertyInfo.GetValue(x, null)).GroupBy(x => ((DateTime)propertyInfo.GetValue(x, null)).ToString("D", DateTimeFormatInfo.InvariantInfo))
                .Select(x => new TravelListByParam { Name = x.Key.ToString(), Items = new ObservableCollection<TravelListItemViewModel>(x.ToList()) });
            } else
            {
                travelListsByParam = travelListsSearch.OrderBy(x => propertyInfo.GetValue(x, null)).GroupBy(x => propertyInfo.GetValue(x, null))
                .Select(x => new TravelListByParam { Name = x.Key.ToString(), Items = new ObservableCollection<TravelListItemViewModel>(x.ToList()) });
            }
            
            Items = new ObservableCollection<TravelListByParam>(travelListsByParam.ToList());
        }

    }

    public class TravelListByParam
    {
        public string Name { get; set; }
        public ObservableCollection<TravelListItemViewModel> Items { get; set; }
    }

    public class PrefItem
    {
        public string Name { get; set; }
    }
}
