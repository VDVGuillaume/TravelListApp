using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TravelListApp.Models;
using TravelListApp.Services;
using TravelListModels;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

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
                    FillSyncPoints();
                    ConvertImages();
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
            set
            {
                if (value != Model.TravelListItemID)
                {
                    Model.TravelListItemID = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(TravelListItemID));
                }
            }
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
                    Country country = App.ViewModel.Countries.Where(x => x.Name == value).First();
                    Latitude = country.LatLng[0];
                    Longitude = country.LatLng[1];
                    IsModified = true;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Country));
                }
            }
        }

        public decimal Latitude
        {
            get => Model.Latitude;
            set
            {
                if (value != Model.Latitude)
                {
                    Model.Latitude = value;
                    IsModified = true;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Latitude));
                }
            }
        }

        public decimal Longitude
        {
            get => Model.Longitude;
            set
            {
                if (value != Model.Longitude)
                {
                    Model.Longitude = value;
                    IsModified = true;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Longitude));
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

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public List<CheckListItem> Items
        {
            get => Model.Items.ToList();
            set
            {
                if (value != Model.Items)
                {
                    Model.Items = value;
                    IsModified = true;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Items));
                }
            }
        }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public List<TravelPointOfInterest> Points
        {
            get => Model.Points.ToList();
            set
            {
                if (value != Model.Points)
                {
                    Model.Points = value;
                    IsModified = true;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Points));
                }
            }
        }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public List<TravelListItemImage> Images
        {
            get => Model.Images.ToList();
            set
            {
                if (value != Model.Images)
                {
                    Model.Images = value;
                    IsModified = true;
                    ConvertImages();
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Images));
                }
            }
        }

        public List<ListItemImage> imageChanges = new List<ListItemImage>();

        /// <summary>
        /// Gets or sets convertImages.
        /// </summary>
        public List<WriteableBitmap> convertedImages = new List<WriteableBitmap>();

        public WriteableBitmap firstConvertedImage
        {
            get => convertedImages.FirstOrDefault();
        }

        public async void ConvertImages()
        {
            convertedImages = new List<WriteableBitmap>();
            foreach (TravelListItemImage image in Images)
            {
                StorageFile sfile = await LocalStorage.AsStorageFile(image.ImageData, image.ImageName);
                convertedImages.Add(await LocalStorage.getImageFromStorageFile(sfile));
            }
        }

        /// <summary>
        /// Gets or sets newPoints.
        /// </summary>
        public List<PointOfInterest> syncPoints = new List<PointOfInterest>();

        public void FillSyncPoints()
        {
            syncPoints = new List<PointOfInterest>();
            foreach (TravelPointOfInterest point in Points)
            {
                syncPoints.Add(new PointOfInterest() {
                    TravelPointOfInterestID = point.TravelPointOfInterestID,
                    Name = point.Name,
                    ImageSourceUri = new Uri("ms-appx:///Assets/MapPin.png"),
                    NormalizedAnchorPoint = new Point(0.5, 1),
                    Latitude = (decimal)point.Latitude,
                    Longitude = (decimal)point.Longitude,
                    Location = new Geopoint(new BasicGeoposition()
                    {
                        Latitude = (double)point.Latitude,
                        Longitude = (double)point.Longitude
                    }),
                    TravelListItemID = point.TravelListItemID
                });
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
                var item = await App.Repository.TravelLists.CreateTravelList(Model);
                this.TravelListItemID = item.TravelListItemID;
                App.ViewModel.TravelListItems.Add(this);
                foreach (ListItemImage ic in imageChanges)
                {
                    if (ic.IsNew)
                    {
                        ic.TravelListItemID = this.TravelListItemID;
                        await App.Repository.TravelListImages.CreateTravelListImage(ic);
                    }
                    if (ic.ToRemove)
                    {
                        await App.Repository.TravelListImages.DeleteTravelListImage(ic);
                    }
                }
                imageChanges.Clear();
                var newModel = await App.Repository.TravelLists.GetTravelListById(item.TravelListItemID);
                this.Model = newModel;
            } else
            {
                await App.Repository.TravelLists.UpdateTravelList(Model.TravelListItemID, Model);
                var item = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == Model.TravelListItemID).First();
                if (item != null)
                {
                    item.Model = _model;
                }
                foreach (ListItemImage ic in imageChanges)
                {
                    if (ic.IsNew)
                    {
                        ic.TravelListItemID = this.TravelListItemID;
                        await App.Repository.TravelListImages.CreateTravelListImage(ic);
                    }
                    if (ic.ToRemove)
                    {
                        await App.Repository.TravelListImages.DeleteTravelListImage(ic);
                    }
                }
                imageChanges.Clear();
                var newModel = await App.Repository.TravelLists.GetTravelListById(Model.TravelListItemID);
                this.Model = newModel;
            }
        }

        /// <summary>
        /// Saves travellist data that has been edited.
        /// </summary>
        public async Task SavePointsAsync()
        {
            foreach (PointOfInterest point in syncPoints)
            {
                if (point.IsNew)
                {
                    await App.Repository.Points.CreateTravelPointOfInterest(point);
                }
            }
        }


        /// <summary>
        /// Saves travellist data that has been edited.
        /// </summary>
        public async Task<Location> GetBingSearchResultsAsync(string search)
        {
            Location location = await App.Repository.Bing.GetLocationByQuery(search);
            return location;
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
                await RefreshAsync();
                IsModified = false;
            }
        }

        /// <summary>
        /// Reloads all of the customer data.
        /// </summary>
        public async Task RefreshAsync()
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
