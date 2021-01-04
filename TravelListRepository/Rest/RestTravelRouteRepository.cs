using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Rest
{
    class RestTravelRouteRepository : ITravelRouteRepo
    {
        private readonly HttpHelper _http;

        public RestTravelRouteRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task CreateTravelRoute(TravelRoute tl) =>
            await _http.PostAsync<TravelRoute, TravelRoute>("TravelRoutes", tl);

        public async Task<TravelRoute> GetTravelRouteById(int id) =>
            await _http.GetAsync<TravelRoute>($"TravelRoutes/{id}");

        public async Task DeleteTravelRoute(TravelRoute tl) =>
            await _http.DeleteAsync("TravelRoutes", tl.TravelRouteID);

        public async Task UpdateTravelRoute(int id, TravelRoute tl) =>
            await _http.PutAsync<TravelRoute, TravelRoute>($"TravelRoutes/{id}", tl);

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
