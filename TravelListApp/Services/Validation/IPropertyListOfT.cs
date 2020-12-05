using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListApp.Services.Validation
{
    public interface IPropertyListOfT<T> : IProperty
    {
        List<T> OriginalValue { get; set; }

        List<T> Value { get; set; }
    }
}
