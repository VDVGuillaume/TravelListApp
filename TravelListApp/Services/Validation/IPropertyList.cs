using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelListApp.Services.Validation
{
    public interface IPropertyList : IBindable
    {
        event EventHandler ValueChanged;

        void Revert();

        void MarkAsClean();

        ObservableCollection<string> Errors { get; }

        bool IsValid { get; }

        bool IsDirty { get; }

        bool IsOriginalSet { get; }
    }
}
