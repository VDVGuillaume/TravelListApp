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
        public MainViewModel() => Task.Run(GetTravelListListAsync);

        public ObservableCollection<TravelListItem> TravelListItems { get; set; }

        private TravelListViewModel _selectedTravelList;

        /// <summary>
        /// Gets or sets the selected customer, or null if no customer is selected. 
        /// </summary>
        public TravelListViewModel SelectedTravelList
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

            TravelListItems = new ObservableCollection<TravelListItem>();

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
                    TravelListItems.Add(c);
                }
                IsLoading = false;
            });
        }
    }
}
