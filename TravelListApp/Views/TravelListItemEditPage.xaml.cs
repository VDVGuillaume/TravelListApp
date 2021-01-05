using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TravelListApp.Models;
using TravelListApp.Services;
using TravelListApp.Services.Icons;
using TravelListApp.ViewModels;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using TravelListApp.Services.Navigation;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using TravelListModels;
using Windows.UI.Core;

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
            DeleteIcon = new ButtonItem() { Glyph = Icon.GetIcon("Clear"), Text = "Clear" };
            ImageUri = new BitmapImage(new Uri("ms-appx:///Assets/StoreLogo.png"));
            CarouselControl.ItemsSource = cImages;
        }

        public BitmapImage ImageUri { get; set; }
        public TravelListItemViewModel ViewModel { get; set; }
        public ButtonItem SaveIcon { get; set; }
        public ButtonItem DeleteIcon { get; set; }
        public byte[] imageData { get; set; }
        public string imageName { get; set; }
        public ObservableCollection<CarouselImage> cImages = new ObservableCollection<CarouselImage>();

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter == null)
            {
                ViewModel = new TravelListItemViewModel
                {
                    IsNewTravelList = true
                };
                ViewModel.StartDate = DateTime.Today;
                ViewModel.EndDate = DateTime.Today;
                DeleteCommandButton.IsEnabled = false;
            }
            else
            {
                ViewModel = App.ViewModel.TravelListItems.Where(travelList => travelList.Model.TravelListItemID == (int)e.Parameter).First();
                // StorageFile sfile = await LocalStorage.AsStorageFile(ViewModel.Images[0].ImageData, ViewModel.Images[0].ImageName);
                // imgbit.Source = ViewModel.firstConvertedImage;
                foreach (var item in ViewModel.convertedImages)
                {
                    cImages.Add(item);
                }
                if (ViewModel.imageChanges.Count > 0)
                {
                    ViewModel.imageChanges.Clear();
                }
            }
            ViewModel.PropertyChanged += (obj, ev) => SaveCommandButton.IsEnabled = ViewModel.IsValid;
            ViewModel.Validate();
            Menu.SetModel(ViewModel);
            Menu.SetTab(GetType());
            base.OnNavigatedTo(e);
            
        }

        private bool navigateFlag = false;
        protected async override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (!navigateFlag)
            {
                e.Cancel = true;
                var result = await ShowDialog();
                if (result)
                {
                    navigateFlag = true;
                    this.Frame.Navigate(e.SourcePageType, e.Parameter);
                } else
                {
                    Menu.SetTab(GetType());
                }
            }
        }

        private async Task<bool> ShowDialog()
        {
            var dialog = new Windows.UI.Popups.MessageDialog(
             "Do you like to continue? you have unsaved changes.",
             "Unsaved changes");

            dialog.Commands.Add(new Windows.UI.Popups.UICommand("Yes") { Id = 0 });
            dialog.Commands.Add(new Windows.UI.Popups.UICommand("No") { Id = 1 });

            dialog.DefaultCommandIndex = 0;
            dialog.CancelCommandIndex = 1;

            var result = await dialog.ShowAsync();

            if (result.Id.Equals(0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void SaveAppBar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (ViewModel.IsNewTravelList || ViewModel.IsDirty)
            {
                MyProgressGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                MyProgressRing.IsActive = true;
                await ViewModel.SaveAsync();
                await ViewModel.ConvertImagesTask();
                MyProgressGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MyProgressRing.IsActive = false;
                Navigation.Navigate(typeof(TravelListItemPage), ViewModel.TravelListItemID);
            }
            else if (ViewModel.imageChanges.Count > 0)
            {
                MyProgressGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                MyProgressRing.IsActive = true;
                await ViewModel.SaveImagesAsync();
                await ViewModel.ConvertImagesTask();
                MyProgressGrid.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                MyProgressRing.IsActive = false;
            }
        }

        private async void DeleteAppBar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!ViewModel.IsNewTravelList)
            {
                MyProgressGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                MyProgressRing.IsActive = true;
                await ViewModel.DeleteAsync();
                MyProgressGrid.Visibility = Windows.UI.Xaml.Visibility.Visible;
                MyProgressRing.IsActive = false;
                Navigation.Navigate(typeof(TravelListPage));
            }
        }

        private void DeleteImageAppBar_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var button = sender as Button;
            var ImageName = button.Tag;
            CarouselImage imageToDelete = cImages.First(image => image.ImageName.Equals(ImageName));
            if(imageToDelete != null)
            {
                imageToDelete.ToRemove = true;
                if (imageToDelete.IsNew)
                {
                    ViewModel.imageChanges.Remove(imageToDelete);
                }
                else if (imageToDelete.TravelListItemImageID > 0)
                {
                    ViewModel.imageChanges.Add(imageToDelete);
                    
                }
                ViewModel.ImageChangesCheck = new List<CarouselImage>();
                ViewModel.ImageChangesCheck = ViewModel.imageChanges;
                cImages.Remove(imageToDelete);
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
            // imgbit.Source = imgSource;
            bitmap.CopyToBuffer(imgSource.PixelBuffer);
            byte[] dataArrayTobeSent = await ConvertImageToByte(inputFile);
            imageName = Guid.NewGuid() + inputFile.FileType;
            imageData = dataArrayTobeSent;
            TravelListItemImage liImage = new TravelListItemImage()
            {
                ImageData = imageData,
                ImageName = imageName,
            };
            CarouselImage cImage = await ViewModel.ConvertImageTask(liImage);
            cImage.IsNew = true;
            ViewModel.imageChanges.Add(cImage);
            ViewModel.ImageChangesCheck = new List<CarouselImage>();
            ViewModel.ImageChangesCheck = ViewModel.imageChanges;
            cImages.Add(cImage);
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
