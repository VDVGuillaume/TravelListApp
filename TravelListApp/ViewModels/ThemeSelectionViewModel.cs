using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelListApp.Models;
using TravelListApp.Services.Theming;

namespace TravelListApp.ViewModels
{
    public class ThemeSelectionViewModel : BaseViewModel
    {
        public ThemeSelectionViewModel()
        {
            Title = "Select Theme";

            //Initialize the List with the theme details, you want to add in the app
            Themes = new List<AppTheme>()
            {
                new AppTheme() { ThemeId = ThemeManager.Themes.Light, Title = "Light Theme", Description = "Gives a light theme experience" },
                new AppTheme() { ThemeId = ThemeManager.Themes.Dark, Title = "Dark Theme", Description = "Gives a dark theme experience" },
            };

            //Find the Current/Last selected theme, and set the IsSelected property for that specific theme item in the list.
            var selectedTheme = Themes.FirstOrDefault(x => x.ThemeId == ThemeManager.CurrentTheme());
            selectedTheme.IsSelected = true;
        }

        List<AppTheme> _themes;
        public List<AppTheme> Themes
        {
            get { return _themes; }
            set { SetProperty(ref _themes, value); }
        }

        AppTheme _selectedTheme;
        public AppTheme SelectedTheme
        {
            get { return _selectedTheme; }
            set
            {
                SetProperty(ref _selectedTheme, value);
                if (value != null) {
                    SelectedThemeName = value.ThemeId.ToString();
                    OnThemeSelected(value);
                }
            }
        }

        string _selectedThemeName = "Light";
        public string SelectedThemeName
        {
            get { return _selectedThemeName; }
            set
            {
                SetProperty(ref _selectedThemeName, value);
            }
        }

        /// <summary>
        /// Invokes when you select any Theme from the ListView
        /// </summary>
        /// <param name="selectedTheme"></param>
        private void OnThemeSelected(AppTheme selectedTheme)
        {
            foreach (var t in Themes)
            {
                t.IsSelected = t.ThemeId == selectedTheme.ThemeId;
            }
            ThemeManager.ChangeTheme(selectedTheme.ThemeId);
        }
    }
}
