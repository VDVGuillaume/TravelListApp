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

        public async Task<TravelPointOfInterest> GetTravelPointOfInterestById(int id) =>
            await _http.GetAsync<TravelPointOfInterest>($"TravelPointOfInterests/{id}");

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
