using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TravelListApp.Mvvm;

namespace TravelListApp.Mvvm
{
    public class ButtonItem : BindableBase
    {
        private string _glyph;
        private string _text;

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
    }
}
