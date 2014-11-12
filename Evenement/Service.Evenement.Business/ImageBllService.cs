using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Configuration;
using Service.Evenement.Business.Interface;

namespace Service.Evenement.Business
{
    public class ImageBllService : IImageBllService
    {
        private EvenementDalService _evenementDalService;

        public EvenementDalService EvenementDalService
        {
            get
            {
                if ( _evenementDalService == null )
                    _evenementDalService = new EvenementDalService();
                return _evenementDalService;
            }
            set
            {
                _evenementDalService = value;
            }
        }

        private static string UriHost = ConfigurationSettings.AppSettings.Get("UriHost");

        private static string AccountName = ConfigurationSettings.AppSettings.Get("StorageAccount");

        private static string StoragePassword = ConfigurationSettings.AppSettings.Get("StoragePassword");

        // A stocké dans le fichier de conf encodé en MD5
        private static StorageCredentials credentials = new StorageCredentials(AccountName, StoragePassword);

        private static CloudStorageAccount account = new CloudStorageAccount(credentials, true);

        private const string ContainerName = "pictures";

        private CloudBlobClient blob;

        private string SaveImage ( string fileName, byte[] content )
        {
            blob = account.CreateCloudBlobClient();

            var container = blob.GetContainerReference(ContainerName);

            var blobFromSasCredential = container.GetBlockBlobReference(fileName);

            blobFromSasCredential.UploadFromByteArray(content, 0, content.Length);

            return blobFromSasCredential.Uri.ToString();
        }
    }
}
