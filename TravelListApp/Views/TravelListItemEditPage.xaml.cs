using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TravelListApp.Mvvm;
using TravelListApp.Services.Icons;
using TravelListApp.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                ViewModel = new TravelListItemViewModel
                {
                    IsNewTravelList = true,
                    IsInEdit = true
                };
            }
            else
            {
                ViewModel = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == (int)e.Parameter).First();
                ViewModel.StartEdit();
            }
            Menu.SetModel(ViewModel);
            Menu.SetTab(GetType());
            base.OnNavigatedTo(e);
        }

        private async void SaveAppBar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await ViewModel.SaveAsync();
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

        public async Task<ImageSource> SaveToImageSource(byte[] imageBuffer)
        {
            ImageSource imageSource = null;
            using (MemoryStream stream = new MemoryStream(imageBuffer))
            {
                var ras = stream.AsRandomAccessStream();
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(BitmapDecoder.JpegDecoderId, ras);
                var provider = await decoder.GetPixelDataAsync();
                byte[] buffer = provider.DetachPixelData();
                WriteableBitmap bitmap = new WriteableBitmap((int)decoder.PixelWidth, (int)decoder.PixelHeight);
                await bitmap.PixelBuffer.AsStream().WriteAsync(buffer, 0, buffer.Length);
                imageSource = bitmap;
            }
            return imageSource;
        }
        public async Task<byte[]> SaveToBytesAsync(ImageSource imageSource)
        {
            byte[] imageBuffer;
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var file = await localFolder.CreateFileAsync("temp.jpg", CreationCollisionOption.ReplaceExisting);
            using (var ras = await file.OpenAsync(FileAccessMode.ReadWrite, StorageOpenOptions.None))
            {
                WriteableBitmap bitmap = imageSource as WriteableBitmap;
                var stream = bitmap.PixelBuffer.AsStream();
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.JpegEncoderId, ras);
                encoder.SetPixelData(BitmapPixelFormat.Bgra8, BitmapAlphaMode.Ignore, (uint)bitmap.PixelWidth, (uint)bitmap.PixelHeight, 96.0, 96.0, buffer);
                await encoder.FlushAsync();

                var imageStream = ras.AsStream();
                imageStream.Seek(0, SeekOrigin.Begin);
                imageBuffer = new byte[imageStream.Length];
                var re = await imageStream.ReadAsync(imageBuffer, 0, imageBuffer.Length);

            }
            await file.DeleteAsync(StorageDeleteOption.Default);
            return imageBuffer;
        }
        private async void Convert_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker fileOpenPicker = new FileOpenPicker();
            fileOpenPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            fileOpenPicker.FileTypeFilter.Add(".jpg");
            fileOpenPicker.ViewMode = PickerViewMode.Thumbnail;

            var inputFile = await fileOpenPicker.PickSingleFileAsync();

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
            //SaveToBytesAsync(imgSource);
            bitmap.CopyToBuffer(imgSource.PixelBuffer);
            byte[] dataArrayTobeSent = await SaveToBytesAsync(imgSource);
        }
    }


}
