using Microsoft.Toolkit.Uwp.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TravelListApp.ViewModels;
using TravelListApp.Views;
using TravelListModels;

namespace TravelListApp.Mvvm
{
    public class TravelListViewModel : BindableBase, IEditableObject
    {

        public ObservableCollection<TravelListByCountry> Items { get; set; }

        public TravelListViewModel()
        {
            getTravelListsItemsByCountry();
        }

        public MainViewModel ViewModel => App.ViewModel;


        public void getTravelListsItemsByCountry()
        {
            var travelListsByCountry = ViewModel.TravelListItems.GroupBy(x => x.Country)
                .Select(x => new TravelListByCountry { Name = x.Key, Items = new ObservableCollection<TravelListItem>(x.ToList()) });

            Items = new ObservableCollection<TravelListByCountry>(travelListsByCountry.ToList());
        }

        public void BeginEdit()
        {
            throw new NotImplementedException();
        }

        public void CancelEdit()
        {
            throw new NotImplementedException();
        }

        public void EndEdit()
        {
            throw new NotImplementedException();
        }
    }

    public class TravelListByCountry
    {
        public string Name { get; set; }
        public ObservableCollection<TravelListItem> Items { get; set; }
    }
}
