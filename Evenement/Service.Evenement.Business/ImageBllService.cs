using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Service.Evenement.Business.Interface;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Collections.Specialized;

namespace Service.Evenement.Business
{
    public class ImageBllService : IImageBllService
    {
        public ImageBllService ()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            AccountName = ConfigurationManager.AppSettings["StorageAccount"];
            StoragePassword = ConfigurationManager.AppSettings["StoragePassword"];
            UriHost = ConfigurationManager.AppSettings["UriHost"];
            //credentials = new StorageCredentials(AccountName, StoragePassword);
            account = CloudStorageAccount.Parse(ConnectionString);
        }

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

        private const string UriImageTemplate = "{0}{1}/{2}";

        private StorageCredentials credentials;

        private string UriHost;

        private string ConnectionString;

        private string AccountName;

        private string StoragePassword;

        // A stocké dans le fichier de conf encodé en MD5
        private CloudStorageAccount account;

        private const string ContainerName = "pictures";

        private CloudBlobClient blob;

        public string SaveImage ( string fileName, byte[] content )
        {
            blob = account.CreateCloudBlobClient();

            var container = blob.GetContainerReference(ContainerName);

            string fileNameKey = DateTime.UtcNow + "-" + fileName;

            var blobFromSasCredential = container.GetBlockBlobReference(fileNameKey);

            try
            {
                blobFromSasCredential.UploadFromByteArray(content, 0, content.Length);
            }
            catch ( Microsoft.WindowsAzure.Storage.StorageException e )
            {
                //LOGERoor
            }
            return String.Format(UriImageTemplate, UriHost, ContainerName, fileNameKey);
        }

        public IEnumerable<IListBlobItem> getBlobList ()
        {
            blob = account.CreateCloudBlobClient();

            var container = blob.GetContainerReference(ContainerName);

            return container.ListBlobs(null, false);
        }

        public byte[] GetImageFromFile ( string path )
        {
            byte[] arr;
            using ( MemoryStream ms = new MemoryStream() )
            {
                Bitmap image1 = (Bitmap) Image.FromFile(path, true);
                image1.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                arr = ms.ToArray();
            }
            return arr;
        }
    }
}
