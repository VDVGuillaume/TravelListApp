using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TravelListModels;

namespace TravelListRepository.Rest
{
    class RestCategoryRepository : ICategoryRepo
    {
        private readonly HttpHelper _http;

        public RestCategoryRepository(string baseUrl)
        {
            _http = new HttpHelper(baseUrl);
        }

        public async Task CreateCategory(Category category) =>
            await _http.PostAsync<Category, Category>("category", category);
      

        public async Task<IEnumerable<Category>> GetAllCategories(string userId) =>
            await _http.GetAsync<IEnumerable<Category>>($"category/GetAllCategories?value={userId}");

 
        public bool SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
