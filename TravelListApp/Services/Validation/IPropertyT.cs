using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListApp.Services.Validation
{
    public interface IProperty<T> : IProperty
    {
        T OriginalValue { get; set; }

        T Value { get; set; }
    }
}
