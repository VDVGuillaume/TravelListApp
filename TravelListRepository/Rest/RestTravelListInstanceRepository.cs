using System;
using System.Collections.Generic;
using System.Text;

namespace TravelListRepository.Rest
{
    public class RestTravelListInstanceRepository : ITravelListInstance
    {
        private readonly string _url;

        public RestTravelListInstanceRepository(string url)
        {
            _url = url;
        }

        public ITravelListItemRepo TravelLists => new RestTravelListRepository(_url);

        public ITravelPointOfInterestRepo Points => new RestTravelPointOfInterestRepository(_url);

        public ITravelListItemImageRepo TravelListImages => new RestTravelListItemImageRepository(_url);

        public ITravelRouteRepo Routes => new RestTravelRouteRepository(_url);

        public ICountryRepo Countries => new RestCountryRepository(_url);

        public IBingRepo Bing => new RestBingRepository(_url);

        public ICategoryRepo Categories => new RestCategoryRepository(_url);

        public ICheckListItemRepo CheckLists => new RestCheckListItemRepository(_url);

        public ITaskListItemRepo TaskLists => new RestTaskListItemRepository(_url);

    }
}
