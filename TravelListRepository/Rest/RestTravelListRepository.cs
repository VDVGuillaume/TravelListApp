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

        public async Task<TravelListItem> CreateTravelList(TravelListItem tl) =>
            await _http.PostAsync<TravelListItem, TravelListItem>("travellists", tl);
        
        public async Task DeleteTravelList(TravelListItem tl) =>
            await _http.DeleteAsync("travellists", tl.TravelListItemID);

        public async Task<IEnumerable<TravelListItem>> GetAllTravelLists(string userId) =>
            await _http.GetAsync<IEnumerable<TravelListItem>>($"travellists/GetAllTravelLists?value={userId}");

        public async Task<TravelListItem> GetTravelListById(int id) =>
            await _http.GetAsync<TravelListItem>($"travellists/{id}");

        public async Task UpdateTravelList(int id, TravelListItem tl) =>
            await _http.PutAsync<TravelListItem, TravelListItem>($"travellists/{id}", tl);

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
