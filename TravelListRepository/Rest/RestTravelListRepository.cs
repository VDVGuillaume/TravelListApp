using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Rest
{
    class RestTravelListRepository : ITravelListItemRepo
    {
        private readonly HttpHelper _http;

        public RestTravelListRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task CreateTravelList(TravelListItem tl) =>
            await _http.PostAsync<TravelListItem, TravelListItem>("travellists", tl);
        
        public async Task DeleteTravelList(TravelListItem tl) =>
            await _http.DeleteAsync("travellist", tl.TravelListItemID);

        public async Task<IEnumerable<TravelListItem>> GetAllTravelLists() =>
            await _http.GetAsync<IEnumerable<TravelListItem>>("travellists");

        public async Task<TravelListItem> GetTravelListById(int id) =>
            await _http.GetAsync<TravelListItem>($"travellists/{id}");

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task UpdateTravelList(TravelListItem tl)
        {
            throw new NotImplementedException();
        }
    }
}
