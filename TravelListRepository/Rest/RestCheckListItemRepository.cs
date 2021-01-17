using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Rest
{
    class RestCheckListItemRepository : ICheckListItemRepo

    {

        private readonly HttpHelper _http;

        public RestCheckListItemRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task<TravelCheckListItem> CreateCheckListItemAsync(TravelCheckListItem checkListItem) =>
            await _http.PostAsync<TravelCheckListItem, TravelCheckListItem>("travelchecklistitem", checkListItem);


        public async Task DeleteCheckListItemAsync(TravelCheckListItem checkListItem) =>
            await _http.DeleteAsync("travelchecklistitem",checkListItem.TravelCheckListItemID);

        public async Task<IEnumerable<TravelCheckListItem>> GetCheckList(int travelListId) =>
            await _http.GetAsync<IEnumerable<TravelCheckListItem>>($"travelchecklistitem/GetChecklist?value={travelListId}");


        public async Task UpdateCheckListItemAsync(int id, TravelCheckListItem checkListItem) =>
         await _http.PutAsync<TravelCheckListItem, TravelCheckListItem>($"travelchecklistitem/{id}", checkListItem);

       
        public async Task<TravelCheckListItem> GetCheckListItemById(int id) =>        
         await _http.GetAsync<TravelCheckListItem>($"travelchecklistitem/{id}");


        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}