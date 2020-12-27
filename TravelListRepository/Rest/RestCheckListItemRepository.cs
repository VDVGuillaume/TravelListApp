using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TravelListModels;
using TravelListRepository;
using TravelListRepository.Rest;

namespace TravelList.Repository.Rest
{
    class RestCheckListItemRepository : ICheckListItemRepo

    {

        private readonly HttpHelper _http;

        public RestCheckListItemRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }


        public async Task CreateCheckListItem(CheckListItem checkListItem)
        {
            await _http.PostAsync<CheckListItem,CheckListItem>("CheckList", checkListItem);
        }

        public async Task<IEnumerable<CheckListItem>> GetCheckList(int travelListId) =>
            await _http.GetAsync<IEnumerable<CheckListItem>>($"CheckList/{travelListId}");

        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
