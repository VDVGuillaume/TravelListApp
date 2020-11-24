using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

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
    }
}
