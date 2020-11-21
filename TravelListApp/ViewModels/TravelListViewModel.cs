using System.Collections.ObjectModel;
using System.Linq;

namespace TravelListApp.ViewModels
{
    public class TravelListViewModel : BindableBase
    {

        public ObservableCollection<TravelListByCountry> Items { get; set; }

        public TravelListViewModel()
        {
            getTravelListsItemsByCountry();
            ViewModel.TravelListItems.CollectionChanged += Name_CollectionChanged;
        }


        private void Name_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
             getTravelListsItemsByCountry();
        }

        public MainViewModel ViewModel => App.ViewModel;


        public void getTravelListsItemsByCountry()
        {
            var travelListsByCountry = ViewModel.TravelListItems.OrderBy(x => x.Country).GroupBy(x => x.Country)
                .Select(x => new TravelListByCountry { Name = x.Key, Items = new ObservableCollection<TravelListItemViewModel>(x.ToList()) });

            Items = new ObservableCollection<TravelListByCountry>(travelListsByCountry.ToList());
        }

    }

    public class TravelListByCountry
    {
        public string Name { get; set; }
        public ObservableCollection<TravelListItemViewModel> Items { get; set; }
    }
}
