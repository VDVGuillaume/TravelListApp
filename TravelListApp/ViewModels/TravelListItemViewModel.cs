using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TravelListApp.Models;
using TravelListApp.Services;
using TravelListApp.Services.Validation;
using TravelListModels;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace TravelListApp.ViewModels
{
    //public class TravelListItemViewModel : BindableBase, IEditableObject
    public class TravelListItemViewModel : ValidatableModelBase
    {

        // public TravelListItemViewModel(TravelListItem model = null) => Model = model ?? new TravelListItem();

        public TravelListItemViewModel(TravelListItem model = null)
        {
            this.Model = model;
        }

        private TravelListItem _model;

        public TravelListItem Model
        {
            get => _model;
            set
            {
                if (value != null) {
                    if (_model != value)
                    {
                        _model = value;
                        this.TravelListItemID = _model.TravelListItemID;
                        this.Name = _model.Name;
                        this.Description = _model.Description;
                        this.StartDate = _model.StartDate;
                        this.EndDate = _model.EndDate;
                        this.Country = _model.Country;
                        this.Latitude = _model.Latitude;
                        this.Longitude = _model.Longitude;
                        this.Items = _model.Items.ToList();
                        this.Points = _model.Points.ToList();
                        this.Images = _model.Images.ToList();
                    }
                } else {
                    _model = new TravelListItem();
                }

                this.Validator = that => { Validation_Executed(that as TravelListItemViewModel); };
            }
        }

        /// <summary>
        /// Gets or sets the TravelListItemID's.
        /// </summary>
        public int TravelListItemID
        {
            get => Read<int>();
            set
            {
                Model.TravelListItemID = value;
                Write(value);
            }
        }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public string Name
        {
            get => Read<string>();
            set
            {
                Model.Name = value;
                Write(value);
            }
        }

        /// <summary>
        /// Gets or sets Description.
        /// </summary>
        public string Description
        {
            get => Read<string>();
            set
            {
                Model.Description = value;
                Write(value);
            }
        }

        /// <summary>
        /// Gets or sets StartDate.
        /// </summary>
        public DateTime StartDate
        {
            get => Read<DateTime>();
            set
            {
                Model.StartDate = value;
                Write(value);
            }
        }

        /// <summary>
        /// Gets or sets EndDate.
        /// </summary>
        public DateTime EndDate
        {
            get => Read<DateTime>();
            set
            {
                Model.EndDate = value;
                Write(value);
            }
        }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public string Country
        {
            get => Read<string>();
            set
            {
                Model.Country = value;
                Write(value);
                Country country = App.ViewModel.Countries.Where(x => x.Name == value).First();
                Latitude = country.LatLng[0];
                Longitude = country.LatLng[1];
            }
        }

        public decimal Latitude
        {
            get => Read<decimal>();
            set
            {
                Model.Latitude = value;
                Write(value);
            }
        }

        public decimal Longitude
        {
            get => Read<decimal>();
            set
            {
                Model.Longitude = value;
                Write(value);
            }
        }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public List<CheckListItem> Items
        {
            get => ReadList<CheckListItem>().ToList();
            set
            {
                Model.Items = value;
                WriteList(value);
            }
            //set
            //{
            //    if (value != Model.Items)
            //    {
            //        Model.Items = value;
            //        Write(value);
            //    }
            //}
        }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public List<TravelPointOfInterest> Points
        {
            get => ReadList<TravelPointOfInterest>().ToList();
            set
            {
                Model.Points = value;
                WriteList(value);
                FillSyncPoints();
            }
            //get => Model.Points.ToList();
            //set
            //{
            //    if (value != Model.Points)
            //    {
            //        Model.Points = value;
            //        IsModified = true;
            //    }
            //}
        }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public List<TravelListItemImage> Images
        {
            get => ReadList<TravelListItemImage>().ToList();
            set
            {
                Model.Images = value;
                WriteList(value);
                ConvertImages();
            }
        }

        /// <summary>
        /// Gets or sets the customer's first name.
        /// </summary>
        public List<ListItemImage> imageChangesCheck
        {
            get => ReadList<ListItemImage>().ToList();
            set
            {
                WriteList(value);
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

        public async Task ConvertImagesTask()
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

        private void Validation_Executed(TravelListItemViewModel c)
        {
            if (string.IsNullOrEmpty(c.Name))
            {
                c.Properties[nameof(c.Name)].Errors.Add("Name is mandatory.");
            }

            if (string.IsNullOrEmpty(c.Description))
            {
                c.Properties[nameof(c.Description)].Errors.Add("Description is mandatory.");
            }

            // Compare two properties.
            if (c.EndDate < c.StartDate)
            {
                // Unfortunately errors have to be assigned to a property.
                c.Properties[nameof(c.EndDate)].Errors.Add("EndDate should come after StartDate.");
            }

            if (string.IsNullOrEmpty(c.Country))
            {
                c.Properties[nameof(c.Country)].Errors.Add("Country is mandatory.");
            }

            // Compare two properties.
            if (c.Images.Count == 0 && (c.imageChanges.Count == 0 || c.imageChanges.FindAll(image => image.IsSet).Count == 0))
            {
                // Unfortunately errors have to be assigned to a property.
                c.Properties[nameof(c.Images)].Errors.Add("Image is mandatory.");
            }

        }

        /// <summary>
        /// Saves travellist data that has been edited.
        /// </summary>
        public async Task SaveAsync()
        {
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
            await ConvertImagesTask();
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
        /// Discards any edits that have been made, restoring the original values.
        /// </summary>
        public async Task RevertChangesAsync()
        {
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

        // private bool _isNewTravelList;

        /// <summary>
        /// Gets or sets a value that indicates whether this is a new customer.
        /// </summary>
        public bool IsNewTravelList
        {
            get => Read<bool>();
            set => Write(value);
            // set => SetProperty(ref _isNewTravelList, value);
        }


        public bool IsModified { get; set; }

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
