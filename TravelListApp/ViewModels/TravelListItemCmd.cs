using System;
using System.Windows.Input;

namespace TravelListApp.ViewModels
{
    public class TravelListItemCmd : BindableBase
    {

        private string _name;
        private string _description;
        private string _image;
        private string _country;
        private DelegateCommand _command;
        private Type _navigationDestination;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        public string Image
        {
            get { return _image; }
            set { SetProperty(ref _image, value); }
        }

        public string Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }

        public ICommand Command
        {
            get { return _command; }
            set { SetProperty(ref _command, (DelegateCommand)value); }
        }

        public Type NavigationDestination
        {
            get { return _navigationDestination; }
            set { SetProperty(ref _navigationDestination, value); }
        }

        public bool IsNavigation => _navigationDestination != null;
    }
}
