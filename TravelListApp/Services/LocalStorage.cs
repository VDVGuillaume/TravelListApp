using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace TravelListApp.Services
{
    public static class LocalStorage
    {
        public static async Task<StorageFile> AsStorageFile(this byte[] byteArray, string fileName)
        {
            var storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await Windows.Storage.FileIO.WriteBytesAsync(sampleFile, byteArray);
            return sampleFile;
        }

        public static async Task<WriteableBitmap> getImageFromStorageFile(StorageFile file)
        {
            SoftwareBitmap softwareBitmap;
            using (IRandomAccessStream stream = await file.OpenAsync(FileAccessMode.Read))
            {
                // Create the decoder from the stream
                BitmapDecoder decoder = await BitmapDecoder.CreateAsync(stream);
                // Get the SoftwareBitmap representation of the file
                softwareBitmap = await decoder.GetSoftwareBitmapAsync();
            }
            var bitmap = softwareBitmap;
            var imgSource = new WriteableBitmap(bitmap.PixelWidth, bitmap.PixelHeight);
            bitmap.CopyToBuffer(imgSource.PixelBuffer);
            return imgSource;
        }
    }
}
