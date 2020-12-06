using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListApp.Services.Validation
{
    public interface IValidatableModel : IBindable
    {
        bool Validate(bool validateAfter);

        void Revert();

        void MarkAsClean();

        ObservableDictionary<string, IProperty> Properties { get; }

        ObservableCollection<string> Errors { get; }

        Action<IValidatableModel> Validator { set; get; }

        bool IsValid { get; }

        bool IsDirty { get; }
    }
}
