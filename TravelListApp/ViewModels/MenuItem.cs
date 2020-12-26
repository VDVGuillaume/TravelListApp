using System;
using System.Windows.Input;
using TravelListApp.Services.Theming;
using Windows.UI.Xaml.Media;

namespace TravelListApp.ViewModels
{
    class MenuItem : BindableBase
    {
        private string _glyph;
        private string _text;
        private bool _isActive = true;
        private string _color;
        private DelegateCommand _command;
        private Type _navigationDestination;

        public string Glyph
        {
            get { return _glyph; }
            set { SetProperty(ref _glyph, value); }
        }

        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        public bool IsActive
        {
            get { return _isActive; }
            set { if (value)
                {
                    Color = "ActiveBrush";
                } else
                {
                    Color = "NotActiveBrush";
                }
                SetProperty(ref _isActive, value); }
        }

        public string Color
        {
            get { return _color; }
            set { SetProperty(ref _color, value); }
        }

        public SolidColorBrush Resource
        {
            get { return Theme.GetResource(_color); }
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
