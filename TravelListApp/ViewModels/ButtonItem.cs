namespace TravelListApp.ViewModels
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
