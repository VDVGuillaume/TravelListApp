﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListApp.Mvvm;
using TravelListModels;

namespace TravelListApp.ViewModels 
{
    public class TravelListItemViewModel : BindableBase, IEditableObject
    {

        public TravelListItemViewModel(TravelListItem model = null) => Model = model ?? new TravelListItem();

        private TravelListItem _model;

        public TravelListItem Model
        {
            get => _model;
            set
            {
                if (_model != value)
                {
                    _model = value;

                    // Raise the PropertyChanged event for all properties.
                    OnPropertyChanged(string.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the TravelListItemID's.
        /// </summary>
        public int TravelListItemID
        {
            get => Model.TravelListItemID;
        }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public string Name
        {
            get => Model.Name;
            set
            {
                if (value != Model.Name)
                {
                    Model.Name = value;
                    IsModified = true;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public string Description
        {
            get => Model.Description;
            set
            {
                if (value != Model.Description)
                {
                    Model.Description = value;
                    IsModified = true;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public string Country
        {
            get => Model.Country;
            set
            {
                if (value != Model.Country)
                {
                    Model.Country = value;
                    IsModified = true;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Country));
                }
            }
        }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public string Image
        {
            get => Model.Image;
            set
            {
                if (value != Model.Image)
                {
                    Model.Image = value;
                    IsModified = true;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Image));
                }
            }
        }

        public List<String> Countries = App.ViewModel.Countries.Select(x => x.Name).ToList();

        /// <summary>
        /// Saves travellist data that has been edited.
        /// </summary>
        public async Task SaveAsync()
        {
            IsInEdit = false;
            IsModified = false;
            if (IsNewTravelList)
            {
                IsNewTravelList = false;
                App.ViewModel.TravelListItems.Add(this);
                await App.Repository.TravelLists.CreateTravelList(Model);
            } else
            {
                await App.Repository.TravelLists.UpdateTravelList(Model);
            }
        }

        /// <summary>
        /// Raised when the user cancels the changes they've made to the customer data.
        /// </summary>
        public event EventHandler AddNewTravelListCanceled;

        /// <summary>
        /// Cancels any in progress edits.
        /// </summary>
        public async Task CancelEditsAsync()
        {
            if (IsNewTravelList)
            {
                AddNewTravelListCanceled?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                await RevertChangesAsync();
            }
        }

        /// <summary>
        /// Enables edit mode.
        /// </summary>
        public void StartEdit() => IsInEdit = true;

        /// <summary>
        /// Discards any edits that have been made, restoring the original values.
        /// </summary>
        public async Task RevertChangesAsync()
        {
            IsInEdit = false;
            if (IsModified)
            {
                await RefreshCustomerAsync();
                IsModified = false;
            }
        }

        /// <summary>
        /// Reloads all of the customer data.
        /// </summary>
        public async Task RefreshCustomerAsync()
        {
            Model = await App.Repository.TravelLists.GetTravelListById(Model.TravelListItemID);
        }

        private bool _isNewTravelList;

        /// <summary>
        /// Gets or sets a value that indicates whether this is a new customer.
        /// </summary>
        public bool IsNewTravelList
        {
            get => _isNewTravelList;
            set => SetProperty(ref _isNewTravelList, value);
        }

        private bool _isInEdit = false;

        /// <summary>
        /// Gets or sets a value that indicates whether the customer data is being edited.
        /// </summary>
        public bool IsInEdit
        {
            get => _isInEdit;
            set => SetProperty(ref _isInEdit, value);
        }

        public bool IsModified { get; set; }

        /// <summary>
        /// Called when a bound DataGrid control causes the travellist to enter edit mode.
        /// </summary>
        public void BeginEdit()
        {
            // Not used.
        }

        /// <summary>
        /// Called when a bound DataGrid control cancels the edits that have been made to a travellist.
        /// </summary>
        public async void CancelEdit() => await CancelEditsAsync();

        /// <summary>
        /// Called when a bound DataGrid control commits the edits that have been made to a customer.
        /// </summary>
        public async void EndEdit() => await SaveAsync();
    }
}
