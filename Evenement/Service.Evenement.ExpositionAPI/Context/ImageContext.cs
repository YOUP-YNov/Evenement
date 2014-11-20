using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Service.Evenement.Business;
using Service.Evenement.Business.Response;

namespace Service.Evenement.ExpositionAPI.Context
{
    /// <summary>
    /// Classe statique threadSafe avec initialisation tardive pour l'accès au service Image Bll
    /// </summary>
    public static class ImageContext
    {
        /// <summary>
        /// Donnée membre représentant l'accés au service image
        /// </summary>
        private static Lazy<ImageBllService> _imageBusinessService;

        /// <summary>
        /// Récupère ou assigne l'accés au service image
        /// </summary>
        public static ImageBllService ImageBusinessService { 
            get { return _imageBusinessService.Value; }
            set { _imageBusinessService = new Lazy<ImageBllService>(() => { return new ImageBllService(); }); }
        }

        /// <summary>
        /// Supprime une Image
        /// </summary>
        /// <param name="ImageId">Id de l'image a supprimer</param>
        /// <returns>Objet englobant de réponse Service</returns>
        public static ResponseObject DeleteImage ( long? ImageId )
        {
            long? imageId = ImageId;
            ResponseObject result = new ResponseObject() { Value = ImageBusinessService.DeleteImage(imageId).ToString() };
            return result;
        }

        /// <summary>
        /// Crée une image via son url et la lie a un évènement
        /// </summary>
        /// <param name="urlFile">url du fichier</param>
        /// <param name="EventId">id de l'évènement</param>
        /// <returns>Objet englobant de réponse Service</returns>
        public static ResponseObject PostImageByUri ( string urlFile, long EventId )
        {
            ResponseObject result = new ResponseObject() { Value = ImageBusinessService.SaveImageFromUrl(urlFile, EventId) };
            return result;
        }

        /// <summary>
        /// Crée une image à partir d'un tableau de byte
        /// </summary>
        /// <param name="fileName">Nom du fichier</param>
        /// <param name="evenement_id">Id de l'évènement</param>
        /// <param name="content">Tableau de byte représentant le document</param>
        /// <returns>Objet englobant de réponse Service</returns>
        public static ResponseObject PostImage ( string fileName, long evenement_id, byte[] content )
        {
            ResponseObject result = new ResponseObject() { Value = ImageBusinessService.SaveImageToEvent(fileName, content, evenement_id) };
            return result;
        }

        /// <summary>
        /// Récuèpre l'ensembles des images d'un évènement
        /// </summary>
        /// <param name="id">Id de l'évènement</param>
        /// <returns>Objet englobant de réponse Service</returns>
        public static ResponseObject GetImagesEvenement ( long id )
        {
            ResponseObject result = new ResponseObject() { Value = ImageBusinessService.GetAllImageByEvent(id) };
            return result;
        }
    }
}