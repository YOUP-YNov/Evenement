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
using Service.Evenement.Dal.Dao.Request;
using Service.Evenement.Dal.Dao;
using AutoMapper;
using Service.Evenement.Business.BusinessModels;
using Logger;

namespace Service.Evenement.Business
{
    /// <summary>
    /// Service business Image
    /// </summary>
    public class ImageBllService : IImageBllService
    {
        /// <summary>
        /// Constructeur par défaut de ImageBllService
        /// </summary>
        public ImageBllService ()
        {
            ConnectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;
            AccountName = ConfigurationManager.AppSettings["StorageAccount"];
            StoragePassword = ConfigurationManager.AppSettings["StoragePassword"];
            UriHost = ConfigurationManager.AppSettings["UriHost"];
            account = CloudStorageAccount.Parse(ConnectionString);
        }

        #region Donnée membre

        /// <summary>
        /// Donnée membre représentant l'accès à la DalEvenement
        /// </summary>
        private EvenementDalService _evenementDalService;

        /// <summary>
        /// Donnée membre représentant l'url du logger
        /// </summary>
        private string _LoggerUri;

        /// <summary>
        /// Template de format pour les Url des images
        /// </summary>
        private const string UriImageTemplate = "{0}{1}/{2}";

        /// <summary>
        /// Donnée membre représentant les informations d'authentification
        /// aux services Azure Cloud Storage
        /// </summary>
        private StorageCredentials credentials;

        /// <summary>
        /// Donnée membre représentant l'url du serveur Azure cloud Storage
        /// </summary>
        private string UriHost;

        /// <summary>
        /// Donnée membre représentant la connectionString au serveur Azure
        /// </summary>
        private string ConnectionString;

        /// <summary>
        /// Donnée membre représentant le nom de compte Azure utilisée pour héberger les Images
        /// </summary>
        private string AccountName;

        /// <summary>
        /// Donnée membre représentant le password pour le stockage des images
        /// </summary>
        private string StoragePassword;

        /// <summary>
        /// Donnée membre représentant le compte Cloud Azure Storage
        /// </summary>
        private CloudStorageAccount account;

        /// <summary>
        /// Donnée membre représentant le nom du container sur le serveur Azure
        /// </summary>
        private const string ContainerName = "pictures";

        /// <summary>
        /// Donnée membre représentant le blob aux quelles on accèdent sur le Cloud
        /// </summary>
        private CloudBlobClient blob;

        #endregion

        #region Propriétées

        /// <summary>
        /// Récupère ou assigne de la Dal évènement
        /// </summary>
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

        /// <summary>
        /// Récupère ou assigne l'Uri de l'api du Logger
        /// </summary>
        public string LoggerUri
        {
            get
            {
                if ( String.IsNullOrWhiteSpace(_LoggerUri) )
                    _LoggerUri = ConfigurationManager.ConnectionStrings["LoggerUri"].ToString();
                return _LoggerUri;
            }
            set
            {
                _LoggerUri = value;
            }
        }

        #endregion

        #region Méthode Publique

        /// <summary>
        /// Sauvegarde une Image sur le serveur Azure Cloud Storage
        /// </summary>
        /// <param name="fileName">Représente le nom du fichier</param>
        /// <param name="content">Représente le contenu du documents sous forme de byte Array</param>
        /// <returns>L'url de l'image sur le serveur de stockage</returns>
        private string SaveImage ( string fileName, byte[] content )
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
                new LErreur(e, "Service.Evenement.Business", "SaveImage Error", 0).Save(LoggerUri);
            }
            return String.Format(UriImageTemplate, UriHost, ContainerName, fileNameKey);
        }

        /// <summary>
        /// Retourne la liste des blob contenu sur le serveur Azure Storage
        /// </summary>
        /// <returns>Liste des Blob</returns>
        public IEnumerable<IListBlobItem> getBlobList ()
        {
            blob = account.CreateCloudBlobClient();

            var container = blob.GetContainerReference(ContainerName);

            return container.ListBlobs(null, false);
        }

        /// <summary>
        /// Sauvegarde une Image sur Azure et l'associe à un évènement
        /// </summary>
        /// <param name="fileName">Nom du fichier a uploader</param>
        /// <param name="content">Contenu binaire du fichier</param>
        /// <param name="EvenementId">Id de l'évènement associé</param>
        /// <returns>Toutes les Images liées à l'évènement</returns>
        public IEnumerable<EventImageBll> SaveImageToEvent ( string fileName, byte[] content , long EvenementId)
        {
            if ( String.IsNullOrWhiteSpace(fileName) || content == null )
                return null;

            var ImageUrl = SaveImage(fileName, content);

            if(String .IsNullOrWhiteSpace(ImageUrl))
                return null;
            
            EventImageDao request = new EventImageDao(){
                EvenementId = EvenementId,
                Url = new StringBuilder(ImageUrl)
            };

            var result = EvenementDalService.CreateImage(request);

            return Mapper.Map<IEnumerable<EventImageDao>, IEnumerable<EventImageBll>>(result);
        }

        /// <summary>
        /// Enregistre une Image partir de son Url et l'associe à un évènement
        /// </summary>
        /// <param name="urlImage">Url de l'image</param>
        /// <param name="EventId">Id de l'évènement</param>
        /// <returns>Toutes les Images liées à l'évènement</returns>
        public IEnumerable<EventImageBll> SaveImageFromUrl ( string urlImage , long EventId)
        {
            if ( String.IsNullOrEmpty(urlImage) )
                return null;

            EventImageDao request = new EventImageDao()
            {
                EvenementId = EventId,
                Url = new StringBuilder(urlImage)
            };

            var result = EvenementDalService.CreateImage(request);

            return Mapper.Map<IEnumerable<EventImageDao>, IEnumerable<EventImageBll>>(result);
        }

        /// <summary>
        /// Récupère toutes les images liées à un évènement
        /// </summary>
        /// <param name="EvenementId">Id de l'évènement</param>
        /// <returns>Toutes les Images liées à l'évènement</returns>
        public IEnumerable<EventImageBll> GetAllImageByEvent ( long EvenementId )
        {
            if ( EvenementId == 0 )
                return null;

            var request = new EvenementDalRequest()
            {
                EvenementId = EvenementId
            };

            var result = EvenementDalService.GetImageByEventId(request);

            return Mapper.Map<IEnumerable<EventImageDao>, IEnumerable<EventImageBll>>(result);
        }

        /// <summary>
        /// Supprime une Image
        /// </summary>
        /// <param name="ImageId">Id de l'image a supprimer</param>
        /// <returns>Code d'état Suppression</returns>
        public ImageDeleteEnum DeleteImage ( long? ImageId )
        {
            if ( !ImageId.HasValue )
                return ImageDeleteEnum.RequestError;

            EvenementDalRequest request = new EvenementDalRequest()
            {
                ImageId = ImageId.Value
            };

            var result = EvenementDalService.DeleteImage(request);

            switch ( result )
            {
                case 1:
                    return ImageDeleteEnum.Deleted;
                case 2:
                    return ImageDeleteEnum.RequestError;
                case 3:
                    return ImageDeleteEnum.ExceptionError;
                case 0:
                    return ImageDeleteEnum.UnknowError;
                default:
                    return ImageDeleteEnum.UnknowError;
            }
        }

        #endregion

        #region Méthode Test

        /// <summary>
        /// Lire un fichier une le disque à partir d'un chemin d'accès
        /// </summary>
        /// <param name="path">Chemin d'accès au fichier</param>
        /// <returns>Tableau de byte représentatnt l'image</returns>
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

        #endregion
    }
}
