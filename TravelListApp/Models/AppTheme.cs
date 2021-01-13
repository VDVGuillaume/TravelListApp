using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListApp.Services.Theming;
using TravelListApp.ViewModels;

namespace TravelListApp.Models
{
    public class AppTheme : ObservableObject
    {
        public ThemeManager.Themes ThemeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        bool _isSelected = false;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty(ref _isSelected, value); }
        }
    }
}
