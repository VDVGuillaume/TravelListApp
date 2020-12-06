using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TravelListApp.Models;
using TravelListApp.Services;
using TravelListApp.Services.Icons;
using TravelListApp.ViewModels;
using TravelListModels;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TravelListApp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TravelListItemEditPage : Page
    {
        public TravelListItemEditPage()
        {
            this.InitializeComponent();
            SaveIcon = new ButtonItem() { Glyph = Icon.GetIcon("Save"), Text = "Save" };
            ImageUri = new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png"));

        }

        public BitmapImage ImageUri { get; set; }
        public TravelListItemViewModel ViewModel { get; set; }
        public ButtonItem SaveIcon { get; set; }
        public byte[] imageData { get; set; }
        public string imageName { get; set; }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                ViewModel = new TravelListItemViewModel
                {
                    IsNewTravelList = true
                };
                ViewModel.StartDate = DateTime.Today;
                ViewModel.EndDate = DateTime.Today;
            }
            else
            {
                ViewModel = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == (int)e.Parameter).First();
                StorageFile sfile = await LocalStorage.AsStorageFile(ViewModel.Images[0].ImageData, ViewModel.Images[0].ImageName);
                imgbit.Source = ViewModel.firstConvertedImage;
            }
            ViewModel.PropertyChanged += (obj, ev) => SaveCommandButton.IsEnabled = ViewModel.IsValid;
            ViewModel.Validate();
            Menu.SetModel(ViewModel);
            Menu.SetTab(GetType());
            base.OnNavigatedTo(e);
            
        }

        private async void SaveAppBar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ViewModel.IsNewTravelList || ViewModel.IsDirty)
            {
                ViewModel.imageChanges.Add(new ListItemImage()
                {
                    ImageData = imageData,
                    ImageName = imageName,
                    IsNew = true
                });
                await ViewModel.SaveAsync();

            }
        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            ((Image)sender).Source = new BitmapImage(new Uri("./Assets/StoreLogo.png", UriKind.Relative));
        }

        void Image_Loaded(object sender, RoutedEventArgs e)
        {
            Image img = sender as Image;
            if (img != null)
            {
                BitmapImage bitmapImage = new BitmapImage();
                img.Width = bitmapImage.DecodePixelWidth = 280;
                bitmapImage.UriSource = new Uri("ms-appx:///Assets/StoreLogo.png");
                img.Source = bitmapImage;
            }
        }

        private async void Convert_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.ViewMode = PickerViewMode.Thumbnail;

            StorageFile inputFile = await fileOpenPicker.PickSingleFileAsync();

            if (inputFile == null)
            {
                // The user cancelled the picking operation
                return;
            }
            SoftwareBitmap softwareBitmap;
            using (IRandomAccessStream stream = await inputFile.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                // Get the SoftwareBitmap representation of the file
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            }
            //var bitmap = new SoftwareBitmap(BitmapPixelFormat.Bgra8, 200, 200);
            var bitmap = softwareBitmap;
            var imgSource = new WriteableBitmap(bitmap.PixelWidth, bitmap.PixelHeight);
            imgbit.Source = imgSource;
            bitmap.CopyToBuffer(imgSource.PixelBuffer);
            byte[] dataArrayTobeSent = await ConvertImageToByte(inputFile);
            imageName = Guid.NewGuid() + inputFile.FileType;
            imageData = dataArrayTobeSent;
            ViewModel.imageChanges.Add(new ListItemImage()
            {
                ImageData = imageData,
                ImageName = imageName,
                IsSet = true
            });
            ViewModel.imageChangesCheck = ViewModel.imageChanges;
        }

        /// <summary>
        /// Convert Image to byte[]
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private async Task<byte[]> ConvertImageToByte(StorageFile file)
        {
            using (var inputStream = await file.OpenSequentialReadAsync())
            {
                Stream readStream = inputStream.AsStreamForRead();
                byte[] byteArray = new byte[readStream.Length];
                await readStream.ReadAsync(byteArray, 0, byteArray.Length);
                return byteArray;
            }

        }
    }


}
