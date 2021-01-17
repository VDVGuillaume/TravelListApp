using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using TravelListApp.Models;
using TravelListApp.Services;
using TravelListApp.Services.Validation;
using TravelListApp.Views;
using TravelListModels;
using Windows.ApplicationModel;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace TravelListApp.ViewModels
{
    public class TravelListItemViewModel : ValidatableModelBase
    {

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
                if (value != null)
                {
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
                        this.Tasks = _model.Tasks.ToList();
                        this.Points = _model.Points.ToList();
                        this.Images = _model.Images.ToList();
                        this.Routes = _model.Routes.ToList();
                    }
                }
                else
                {
                    _model = new TravelListItem();
                    _model.UserId = LoginPage.account.Id;
                }

                this.PlacesOrRoutesAreUpdated = false;
                this.ImageChangesCheck = new List<CarouselImage>();

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
        /// Gets or sets Country.
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

        /// <summary>
        /// Gets or sets Latitude.
        /// </summary>
        public decimal Latitude
        {
            get => Read<decimal>();
            set
            {
                Model.Latitude = value;
                Write(value);
            }
        }

        /// <summary>
        /// Gets or sets Longitude.
        /// </summary>
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
        /// Gets or sets Items.
        /// </summary>
        public List<TravelCheckListItem> Items
        {
            get => ReadList<TravelCheckListItem>().ToList();
            set
            {
                Model.Items = value;
                WriteList(value);
            }
        }

        /// <summary>
        /// Gets or sets Tasks.
        /// </summary>
        public List<TravelTaskListItem> Tasks
        {
            get => ReadList<TravelTaskListItem>().ToList();
            set
            {
                Model.Tasks = value;
                WriteList(value);
            }
        }

        /// <summary>
        /// Gets or sets Points.
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
        }

        /// <summary>
        /// Gets or sets Images.
        /// </summary>
        public List<TravelListItemImage> Images
        {
            get => ReadList<TravelListItemImage>().ToList();
            set
            {
                Model.Images = value;
                WriteList(value);                  
            }
        }

        /// <summary>
        /// Gets or sets Routes.
        /// </summary>
        public List<TravelRoute> Routes
        {
            get => ReadList<TravelRoute>().ToList();
            set
            {
                Model.Routes = value;
                WriteList(value);
                FillSyncRoutes();
            }
        }

        /// <summary>
        /// List of Countries
        /// </summary>
        public List<String> Countries = App.ViewModel.Countries.Select(x => x.Name).ToList();

        /// <summary>
        /// Gets or sets Images for validation
        /// </summary>
        public List<CarouselImage> ImageChangesCheck
        {
            get => ReadList<CarouselImage>().ToList();
            set
            {
                WriteList(value);
            }
        }

        // List of CarouselImage used in the editpage for triggering imageChanges validation
        public List<CarouselImage> imageChanges = new List<CarouselImage>();

        public WriteableBitmap defaultImage;

        /// <summary>
        /// Create defaut WriteableBitmap
        /// </summary>
        public async Task SetDefaultImage()
        {
            string fileToLaunch = @"Assets\Square150x150Logo.scale-200.png";
            var storageFile = await Package.Current.InstalledLocation.GetFileAsync(fileToLaunch);
            var stream = await storageFile.OpenReadAsync();

            var wb = new WriteableBitmap(492, 507); // size by magic?
            await wb.SetSourceAsync(stream);
            defaultImage = wb;
        }

        public List<CarouselImage> convertedImages = new List<CarouselImage>();

        /// <summary>
        /// Returns first converted WriteableBitmap
        /// </summary>
        public WriteableBitmap firstConvertedImage
        {
            get
            {
                if (convertedImages.FirstOrDefault() != null)
                {
                    return convertedImages.FirstOrDefault().Photo;
                } else
                {
                    return defaultImage;
                }
                
            }
        }

        /// <summary>
        /// Converts multiple image byte[] and enriches CarouselImage objects
        /// </summary>
        public async Task ConvertImagesTask()
        {
            await SetDefaultImage();
            convertedImages = new List<CarouselImage>();
            foreach(TravelListItemImage image in Images)
            {
                StorageFile sfile = await LocalStorage.AsStorageFile(image.ImageData, image.ImageName);
                CarouselImage cImage = new CarouselImage(image)
                {
                    Photo = await LocalStorage.getImageFromStorageFile(sfile)
                };
                convertedImages.Add(cImage);
            }
        }

        /// <summary>
        /// Converts image byte[] and enrich CarouselImage object
        /// </summary>
        public async Task<CarouselImage> ConvertImageTask(TravelListItemImage image)
        {
            StorageFile sfile = await LocalStorage.AsStorageFile(image.ImageData, image.ImageName);
            CarouselImage cImage = new CarouselImage(image)
            {
                Photo = await LocalStorage.getImageFromStorageFile(sfile)
            };
            return cImage;
        }

        public List<PointOfInterest> syncPoints = new List<PointOfInterest>();

        /// <summary>
        /// Fill up points for the places pages
        /// </summary>
        public void FillSyncPoints()
        {
            syncPoints = new List<PointOfInterest>();
            foreach (TravelPointOfInterest point in Points)
            {
                syncPoints.Add(new PointOfInterest(point));
            }
        }

        public ObservableCollection<RoutesOfPointOfInterest> syncRoutes = new ObservableCollection<RoutesOfPointOfInterest>();

        /// <summary>
        /// Fill up routes for the routes pages
        /// </summary>
        public void FillSyncRoutes()
        {
            syncRoutes.Clear();
            foreach (TravelRoute route in Routes)
            {
                RoutesOfPointOfInterest syncRoute = new RoutesOfPointOfInterest(route);
                syncRoute.Start = Points.Find(p => p.TravelPointOfInterestID == route.StartTravelPointOfInterestID);
                syncRoute.End = Points.Find(p => p.TravelPointOfInterestID == route.EndTravelPointOfInterestID);
                syncRoutes.Add(syncRoute);
            }
        }

        /// <summary>
        /// Dialog when leaving unsaved page
        /// </summary>
        public async Task<bool> ShowDialog()
        {
            var dialog = new Windows.UI.Popups.MessageDialog(
             "Do you like to continue? you have unsaved changes.",
             "Unsaved changes");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            if (result.Id.Equals(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validates the travellist model
        /// </summary>
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
            if (
                (c.Images.Count == 0 && c.imageChanges.FindAll(image => image.IsNew).Count == 0) ||
                ((c.Images.Count == c.imageChanges.FindAll(image => image.ToRemove).Count) && c.imageChanges.FindAll(image => image.IsNew).Count == 0)
               )
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
            if (IsNewTravelList)
            {
                IsNewTravelList = false;
                var item = await App.Repository.TravelLists.CreateTravelList(Model);
                this.TravelListItemID = item.TravelListItemID;
                App.ViewModel.TravelListItems.Add(this);
                await SaveImagesAsync();
                var newModel = await App.Repository.TravelLists.GetTravelListById(item.TravelListItemID);
                this.Model = newModel;
                MarkAsClean();
            } else
            {
                await App.Repository.TravelLists.UpdateTravelList(Model.TravelListItemID, Model);
                var item = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == Model.TravelListItemID).First();
                if (item != null)
                {
                    item.Model = _model;
                }
                await SaveImagesAsync();
                await RefreshAsync();
            }
        }

        /// <summary>
        /// Saves travellist images that has been edited.
        /// </summary>
        public async Task SaveImagesAsync()
        {
            foreach (CarouselImage ic in imageChanges)
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
        }

        /// <summary>
        /// Delete travellist.
        /// </summary>
        public async Task DeleteAsync()
        {
            await App.Repository.TravelLists.DeleteTravelList(Model);
            App.ViewModel.TravelListItems.Remove(this);
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {            
            IEnumerable<Category> categories = await App.Repository.Categories.GetAllCategories(LoginPage.account.Id);
            return categories.ToList();
        }

        public async Task SaveCategoryAsync(Category category)
        {
            await App.Repository.Categories.CreateCategory(category);
        }

        public async Task<List<TravelCheckListItem>> GetTravelCheckListItems()
        {
            IEnumerable<TravelCheckListItem> checkListItems = await App.Repository.CheckLists.GetCheckList(_model.TravelListItemID);
            return checkListItems.ToList();
        }

       
        /// <summary>
        /// Saves travellist checklist data that has been edited.
        /// </summary>
        public async Task<TravelCheckListItem> SaveChecklistAsync(TravelCheckListItem ci)
        {

                ci.TravelListItemID = _model.TravelListItemID;
                TravelCheckListItem item = await App.Repository.CheckLists.CreateCheckListItemAsync(ci);
                return item;
        }

        public async Task DeleteChecklistAsync(TravelCheckListItem ci)
        {
            await App.Repository.CheckLists.DeleteCheckListItemAsync(ci);
        }

        public async Task UpdateChecklistAsync(TravelCheckListItem ci)
        {
            await App.Repository.CheckLists.UpdateCheckListItemAsync(ci.TravelCheckListItemID, ci);
             
            
        }

        public async Task<List<TravelTaskListItem>> GetTravelTaskListItems()
        {
            IEnumerable<TravelTaskListItem> checkListItems = await App.Repository.TaskLists.GetTaskList(_model.TravelListItemID);
            return checkListItems.ToList();
        }

        /// <summary>
        /// Saves travellist task data that has been edited.
        /// </summary>
        public async Task<TravelTaskListItem> SaveTasklistAsync(TravelTaskListItem ci)
        {
            
                ci.TravelListItemID = _model.TravelListItemID;
                TravelTaskListItem item = await App.Repository.TaskLists.CreateTaskListItemAsync(ci);
                return item;
        }


        public async Task DeleteTasklistAsync(TravelTaskListItem ci)
        {
            await App.Repository.TaskLists.DeleteTaskListItemAsync(ci);
        }
        public async Task UpdateTasklistAsync(TravelTaskListItem ci)
        {
            await App.Repository.TaskLists.UpdateTaskListItemAsync(ci.TravelTaskListItemID, ci);
       
        }




        /// <summary>
        /// Saves travellist points data that has been edited.
        /// </summary>
        public async Task SavePointsAsync()
        {
            if (syncPoints.FindAll(p => p.IsNew == true || p.ToRemove == true || p.IsUpdate == true).Count > 0)
            {
                foreach (PointOfInterest point in syncPoints)
                {
                    if (point.IsNew && !point.ToRemove)
                    {
                        await App.Repository.Points.CreateTravelPointOfInterest(point);
                    }
                    else if (point.ToRemove && !point.IsNew)
                    {
                        await App.Repository.Points.DeleteTravelPointOfInterest(point);
                    }
                    else if (point.IsUpdate && !point.ToRemove && !point.IsNew)
                    {
                        await App.Repository.Points.UpdateTravelPointOfInterest(point.TravelPointOfInterestID,point);
                    }
                }
                await RefreshAsync();
            }
        }

        /// <summary>
        /// Saves travellist routes data that has been edited.
        /// </summary>
        public async Task SaveRoutesAsync()
        {
            if (syncRoutes.Where(p => p.IsNew == true || p.ToRemove == true || p.IsUpdate == true).Count() > 0)
            {
                foreach (RoutesOfPointOfInterest route in syncRoutes)
                {
                    if (route.IsNew && !route.ToRemove)
                    {
                        await App.Repository.Routes.CreateTravelRoute(route);
                    }
                    else if (route.ToRemove && !route.IsNew)
                    {
                        await App.Repository.Routes.DeleteTravelRoute(route);
                    }
                    else if (route.IsUpdate && !route.ToRemove && !route.IsNew)
                    {
                        await App.Repository.Routes.UpdateTravelRoute(route.TravelRouteID, route);
                    }
                }
                await RefreshAsync();
            }
        }

        /// <summary>
        /// Search Bing and return location object.
        /// </summary>
        public async Task<Location> GetBingSearchResultsAsync(string search)
        {
            Location location = await App.Repository.Bing.GetLocationByQuery(search);
            return location;
        }

        /// <summary>
        /// Discards any edits that have been made, restoring the original values.
        /// </summary>
        public async Task RevertChangesAsync()
        {
            if (IsDirty)
            {
                await RefreshAsync();
            }
        }

        /// <summary>
        /// Reloads all of the TravelList data.
        /// </summary>
        public async Task RefreshAsync()
        {
            Model = await App.Repository.TravelLists.GetTravelListById(Model.TravelListItemID);
            MarkAsClean();
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this is a new TravelList.
        /// </summary>
        public bool IsNewTravelList
        {
            get => Read<bool>();
            set => Write(value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether Places Or Routes Are Dirty.
        /// </summary>
        public bool PlacesOrRoutesAreUpdated
        {
            get => Read<bool>();
            set => Write(value);
        }

    }
}