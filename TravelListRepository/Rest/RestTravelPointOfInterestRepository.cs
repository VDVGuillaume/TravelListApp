using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Rest
{
    class RestTravelPointOfInterestRepository : ITravelPointOfInterestRepo
    {
        private readonly HttpHelper _http;

        public RestTravelPointOfInterestRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task<TravelPointOfInterest> CreateTravelPointOfInterest(TravelPointOfInterest tl) =>
            await _http.PostAsync<TravelPointOfInterest, TravelPointOfInterest>("TravelPointOfInterests", tl);

        public async Task<TravelPointOfInterest> GetTravelPointOfInterestById(int id) =>
            await _http.GetAsync<TravelPointOfInterest>($"TravelPointOfInterests/{id}");

        public async Task DeleteTravelPointOfInterest(TravelPointOfInterest tl) =>
            await _http.DeleteAsync("TravelPointOfInterests", tl.TravelPointOfInterestID);

        public async Task UpdateTravelPointOfInterest(int id, TravelPointOfInterest tl) =>
            await _http.PutAsync<TravelPointOfInterest, TravelPointOfInterest>($"TravelPointOfInterests/{id}", tl);

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
