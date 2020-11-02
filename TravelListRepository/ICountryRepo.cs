using TravelListModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListRepository
{
    public interface ICountryRepo
    {
        Task<IEnumerable<Country>> GetAllCountries();
    }
}
