using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TravelListApp.Models;
using TravelListModels;
using Windows.Devices.Geolocation;
using Windows.Foundation;

namespace TravelListApp.ViewModels
{
    public class TravelListItemImageViewModel : BindableBase, IEditableObject
    {

        public TravelListItemImageViewModel(TravelListItemImage model = null) => Model = model ?? new TravelListItemImage();

        private TravelListItemImage _model;

        public TravelListItemImage Model
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
        public int TravelListItemImageID
        {
            get => Model.TravelListItemImageID;
        }

        /// <summary>
        /// Gets or sets the TravelListItemID's.
        /// </summary>
        public int TravelListItemID
        {
            get => Model.TravelListItemID;
            set
            {
                if (value != Model.TravelListItemID)
                {
                    Model.TravelListItemID = value;
                    IsModified = true;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TravelListItemID));
                }
            }
        }

        /// <summary>
        /// Gets ImageData
        /// </summary>
        public byte[] ImageData
        {
            get => Model.ImageData;
            set
            {
                if (value != Model.ImageData)
                {
                    Model.ImageData = value;
                    IsModified = true;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ImageData));
                }
            }
        }


        /// <summary>
        /// Saves travellist data that has been edited.
        /// </summary>
        public async Task SaveAsync()
        {
            IsInEdit = false;
            IsModified = false;
            if (IsNewTravelListImage)
            {
                IsNewTravelListImage = false;
                App.ViewModel.TravelListImages.Add(this);
                await App.Repository.TravelListImages.CreateTravelListImage(Model);
            } else
            {
                await App.Repository.TravelListImages.UpdateTravelListImage(Model.TravelListItemID, Model);
            }
        }

        /// <summary>
        /// Raised when the user cancels the changes they've made to the customer data.
        /// </summary>
        public event EventHandler AddNewTravelListImageCanceled;

        /// <summary>
        /// Cancels any in progress edits.
        /// </summary>
        public async Task CancelEditsAsync()
        {
            if (IsNewTravelListImage)
            {
                AddNewTravelListImageCanceled?.Invoke(this, EventArgs.Empty);
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
                await RefreshTravelListImagesAsync();
                IsModified = false;
            }
        }

        /// <summary>
        /// Reloads all of the customer data.
        /// </summary>
        public async Task RefreshTravelListImagesAsync()
        {
            Model = await App.Repository.TravelListImages.GetTravelListImageById(Model.TravelListItemID);
        }

        private bool _isNewTravelListIamge;

        /// <summary>
        /// Gets or sets a value that indicates whether this is a new customer.
        /// </summary>
        public bool IsNewTravelListImage
        {
            get => _isNewTravelListIamge;
            set => SetProperty(ref _isNewTravelListIamge, value);
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
