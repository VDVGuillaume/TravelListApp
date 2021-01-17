using System;
using System.Linq;
using System.Threading.Tasks;
using TravelListModels;
using Windows.Storage;
using System.IO;
using TravelListApp.Views;
using Windows.ApplicationModel;
using System.Collections.Generic;

namespace TravelListApp.Seeding
{
    public class SeedingData
    {
        public SeedingData()
        {

        }

        public async Task LoadData()
        {
            
            await CreateTravelListByName("Spain", "Spain", 5);
            await CreateTravelListByName("Costa Rica" ,"CostaRica", 30);
            await CreateTravelListByName("Japan", "Japan", 50);
        }

        private async Task CreateTravelListByName(string name, string imageName, int days)
        {
            try
            {
                var newTravelListItem = new TravelListItem();
                newTravelListItem.UserId = LoginPage.account.Id;
                newTravelListItem.Name = name;
                newTravelListItem.Description = name +" Description";
                newTravelListItem.StartDate = DateTime.Now.AddDays(days);
                newTravelListItem.EndDate = DateTime.Now.AddDays(days + 10); ;
                newTravelListItem.Country = name;
                Country country = App.ViewModel.Countries.Where(x => x.Name == name).First();
                newTravelListItem.Latitude = country.LatLng[0];
                newTravelListItem.Longitude = country.LatLng[1];

                var item = await CreateTravelList(newTravelListItem);
                await CreateImageByName(imageName + "1", item);
                await CreateImageByName(imageName + "2", item);

                TravelPointOfInterest start = null;
                TravelPointOfInterest end = null;

                if (string.Equals(name,"Spain"))
                {
                    start = await CreateTravelPointOfInterest(new TravelPointOfInterest() { Name = "Bilbao", Latitude = 43.24M, Longitude = -2.92m, TravelListItemID = item.TravelListItemID });
                    end = await CreateTravelPointOfInterest(new TravelPointOfInterest() { Name = "Pamplona", Latitude = 42.78M, Longitude = -1.63m, TravelListItemID = item.TravelListItemID });
                } else if (string.Equals(name, "Costa Rica"))
                {
                    start = await CreateTravelPointOfInterest(new TravelPointOfInterest() { Name = "San Jose", Latitude = 9.93M, Longitude = -84.06m, TravelListItemID = item.TravelListItemID });
                    end = await CreateTravelPointOfInterest(new TravelPointOfInterest() { Name = "Orotina", Latitude = 9.91M, Longitude = -84.52m, TravelListItemID = item.TravelListItemID });
                } else if (string.Equals(name, "Japan"))
                {
                    start = await CreateTravelPointOfInterest(new TravelPointOfInterest() { Name = "Tokyo", Latitude = 35.68M, Longitude = 139.73m, TravelListItemID = item.TravelListItemID });
                    end = await CreateTravelPointOfInterest(new TravelPointOfInterest() { Name = "Nagano", Latitude = 36.59M, Longitude = 138.12m, TravelListItemID = item.TravelListItemID });
                }

                if (start != null && end != null)
                {
                    await CreateTravelRoute(new TravelRoute()
                    {
                        Driving = true,
                        StartTravelPointOfInterestID = start.TravelPointOfInterestID,
                        EndTravelPointOfInterestID = end.TravelPointOfInterestID,
                        TravelListItemID = item.TravelListItemID,
                    });
                }


            }
            catch (Exception) { }
        }

        private async Task CreateImageByName(string name, TravelListItem item)
        {
            try
            {
                string fileToLaunch = @"Assets\Seeding\"+ name + ".jpg";
                StorageFile inputFile = await Package.Current.InstalledLocation.GetFileAsync(fileToLaunch);
                byte[] dataArrayTobeSent = await ConvertImageToByte(inputFile);

                var newTravelListItemImage = new TravelListItemImage();
                newTravelListItemImage.TravelListItemID = item.TravelListItemID;
                newTravelListItemImage.ImageName = Guid.NewGuid() + inputFile.FileType;
                newTravelListItemImage.ImageData = dataArrayTobeSent;
                await CreateTravelListImage(newTravelListItemImage);
            }
            catch (FileNotFoundException) { }
        }

        private async Task<TravelListItem> CreateTravelList(TravelListItem travelListItem)
        {
            return await App.Repository.TravelLists.CreateTravelList(travelListItem);
        }

        private async Task CreateTravelListImage(TravelListItemImage travelListItemImage)
        {
            await App.Repository.TravelListImages.CreateTravelListImage(travelListItemImage);
        }

        private async Task<TravelPointOfInterest> CreateTravelPointOfInterest(TravelPointOfInterest travelPointOfInterest)
        {
            return await App.Repository.Points.CreateTravelPointOfInterest(travelPointOfInterest);
        }

        private async Task CreateTravelRoute(TravelRoute travelRoute)
        {
            await App.Repository.Routes.CreateTravelRoute(travelRoute);
        }

        /// <summary>
        /// Convert Image to byte[]
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<byte[]> ConvertImageToByte(StorageFile file)
        {
            using (var inputStream = await file.OpenSequentialReadAsync())
            {
                Stream readStream = inputStream.AsStreamForRead();
                byte[] byteArray = new byte[readStream.Length];
                await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                return byteArray;
            }

        }
    }
}
