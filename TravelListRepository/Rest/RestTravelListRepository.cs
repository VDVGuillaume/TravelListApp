using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Rest
{
    class RestTravelListRepository : ITravelListRepo
    {
        private readonly HttpHelper _http;

        public RestTravelListRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task CreateTravelList(TravelList tl) =>
            await _http.PostAsync<TravelList, TravelList>("travellist", tl);
        
        public async Task DeleteTravelList(TravelList tl) =>
            await _http.DeleteAsync("travellist", tl.TravelListID);

        public async Task<IEnumerable<TravelList>> GetAllTravelLists() =>
            await _http.GetAsync<IEnumerable<TravelList>>("travellist");

        public async Task<TravelList> GetTravelListById(int id) =>
            await _http.GetAsync<TravelList>($"travellist/{id}");

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }

        public Task UpdateTravelList(TravelList tl)
        {
            throw new NotImplementedException();
        }
    }
}
