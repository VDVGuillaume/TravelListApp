using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TravelListApp.Services.Validation
{
    public class PropertyList<T> : IPropertyListOfT<T>, INotifyPropertyChanged
    {
        public PropertyList()
        {
            Errors.CollectionChanged += (s, e) => RaisePropertyChanged(nameof(IsValid));
        }

        public event EventHandler ValueChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Revert() => Value = OriginalValue;

        public void MarkAsClean() => OriginalValue = Value;

        public override string ToString() => Value?.ToString();

        public ObservableCollection<string> Errors { get; } = new ObservableCollection<string>();

        public bool IsValid => !Errors.Any();

        public bool IsDirty
        {
            get
            {
                if (Value == null)
                    return OriginalValue != null;
                return !Value.Equals(OriginalValue);
            }
        }

        List<T> _Value = new List<T>();
        public List<T> Value
        {
            get { return _Value; }
            set
            {
                if (!IsOriginalSet)
                    OriginalValue = value;
                Set(ref _Value, value);
                ValueChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        bool _IsOriginalSet = false;
        public bool IsOriginalSet
        {
            get { return _IsOriginalSet; }
            private set { Set(ref _IsOriginalSet, value); }
        }

        List<T> _OriginalValue = default(List<T>);
        public List<T> OriginalValue
        {
            get { return _OriginalValue; }
            set
            {
                IsOriginalSet = true;
                Set(ref _OriginalValue, value);
            }
        }

        private bool Set<V>(ref V storage, V value, [CallerMemberName]string callerMemberName = null)
        {
            if (Equals(storage, value))
                return false;
            storage = value;
            RaisePropertyChanged(callerMemberName);
            RaisePropertyChanged(nameof(IsDirty));
            return true;
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
