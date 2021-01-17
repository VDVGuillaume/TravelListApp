using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Rest
{
    class RestTaskListItemRepository : ITaskListItemRepo

    {

        private readonly HttpHelper _http;

        public RestTaskListItemRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task<TravelTaskListItem> CreateTaskListItemAsync(TravelTaskListItem taskListItem) =>
            await _http.PostAsync<TravelTaskListItem, TravelTaskListItem>("traveltasklistitem", taskListItem);


        public async Task DeleteTaskListItemAsync(TravelTaskListItem taskListItem) =>
            await _http.DeleteAsync("traveltasklistitem", taskListItem.TravelTaskListItemID);

        public async Task<IEnumerable<TravelTaskListItem>> GetTaskList(int travelListId) =>
            await _http.GetAsync<IEnumerable<TravelTaskListItem>>($"traveltasklistitem/GetTasklist?value={travelListId}");


        public async Task UpdateTaskListItemAsync(int id, TravelTaskListItem taskListItem) =>
         await _http.PutAsync<TravelTaskListItem, TravelTaskListItem>($"traveltasklistitem/{id}", taskListItem);


        public async Task<TravelTaskListItem> GetTaskListItemById(int id) =>
         await _http.GetAsync<TravelTaskListItem>($"traveltasklistitem/{id}");


        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}