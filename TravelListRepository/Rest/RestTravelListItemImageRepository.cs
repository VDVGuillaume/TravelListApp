using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Rest
{
    class RestTravelListItemImageRepository : ITravelListItemImageRepo
    {
        private readonly HttpHelper _http;

        public RestTravelListItemImageRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task<TravelListItemImage> GetTravelListImageById(int id) =>
    await _http.GetAsync<TravelListItemImage>($"travellistimages/{id}");

        public async Task CreateTravelListImage(TravelListItemImage tl) =>
            await _http.PostAsync<TravelListItemImage, TravelListItemImage>("travellistimages", tl);
        
        public async Task DeleteTravelListImage(TravelListItemImage tl) =>
            await _http.DeleteAsync("travellistimage", tl.TravelListItemID);

        public async Task UpdateTravelListImage(int id, TravelListItemImage tl) =>
            await _http.PutAsync<TravelListItemImage, TravelListItemImage>($"travellistimages/{id}", tl);

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
