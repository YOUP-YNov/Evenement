using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal.Interface;
using Service.Evenement.Dal.Dal.EventDalServiceTableAdapters;
using Service.Evenement.Dal.Dao;
using Service.Evenement.Dal.Mappeur;
using EvenementDao = Service.Evenement.Dal.Dao.EvenementDao;
using Service.Evenement.Dal.Dal;
using Service.Evenement.Dal.Dao.Request;
using System.Configuration;
using Logger;

namespace Service.Evenement.Dal
{
    public class EvenementDalService : IEvenementDalService
    {
        #region Données Membres

        /// <summary>
        /// Donnée membre représentant l'accès au dataset Evenement
        /// </summary>
        private EvenementTableAdapter _eventDalService;

        /// <summary>
        /// Donnée membre représentant l'accès au dataset LieuEvenement
        /// </summary>
        private LieuEvenementTableAdapter _lieuDalService;

        /// <summary>
        /// Donnée membre représentant l'accès au dataset Image
        /// </summary>
        private ImageTableAdapter _imageDalService;

        /// <summary>
        /// Donnée membre représentant l'accès au dataset Categorie
        /// </summary>
        private CategorieTableAdapter _categorieDalService;

        /// <summary>
        /// Donnée membre représentant l'accès au dataset Suscription
        /// </summary>
        private SubscriptionEventTableAdapter _SubscriptionDalService;

        /// <summary>
        /// Donnée membre représentant l'url du logger
        /// </summary>
        private string _LoggerUri;

        #endregion

        #region Propriétés

        /// <summary>
        /// Récupère ou assigne l'Uri de l'api du Logger
        /// </summary>
        public string LoggerUri 
        {
            get 
            { 
                if(String.IsNullOrWhiteSpace(_LoggerUri))
                    _LoggerUri = ConfigurationManager.AppSettings["LoggerUri"].ToString();
                return _LoggerUri;
            }
            set
            {
                _LoggerUri = value;
            }
        }

        /// <summary>
        /// Récupère ou assigne l'accès au tableAdapter Evenement
        /// </summary>
        public EvenementTableAdapter EventDalService 
        {
            get
            {
                if ( _eventDalService == null )
                    _eventDalService = new EvenementTableAdapter();
                return _eventDalService;
            }
            set
            {
                _eventDalService = value;
            }
        }

        /// <summary>
        /// Récupère ou assigne l'accès au tableAdapter Lieu Evenement
        /// </summary>
        public LieuEvenementTableAdapter LieuEventDalService
        {
            get
            {
                if ( _lieuDalService == null )
                    _lieuDalService = new LieuEvenementTableAdapter();
                return _lieuDalService;
            }
            set
            {
                _lieuDalService = value;
            }
        }

        /// <summary>
        /// Récupère ou assigne l'accès au tableAdapter Image
        /// </summary>
        public ImageTableAdapter ImageDalService
        {
            get
            {
                if ( _imageDalService == null )
                    _imageDalService = new ImageTableAdapter();
                return _imageDalService;
            }
            set
            {
                _imageDalService = value;
            }
        }

        /// <summary>
        /// Récupère ou assigne l'accès au tableAdapter Categorie
        /// </summary>
        public CategorieTableAdapter CategorieDalService 
        {
            get
            {
                if ( _categorieDalService == null )
                    _categorieDalService = new CategorieTableAdapter();
                return _categorieDalService;
            }
            set
            {
                _categorieDalService = value;
            }
        }

        /// <summary>
        /// Récupère ou assigne l'accès au tableAdapter Suscription
        /// </summary>
        public SubscriptionEventTableAdapter SubscriptionDalService 
        { 
            get
            {
                if ( _SubscriptionDalService == null )
                    _SubscriptionDalService = new SubscriptionEventTableAdapter();
                return _SubscriptionDalService;
            }
            set
            {
                _SubscriptionDalService = value;
            }
        }

        #endregion

        #region Méthode Public

        /// <summary>
        /// Récupère toutes les catégories d'évènement
        /// </summary>
        /// <param name="request"> Requete Dal </param>
        /// <returns>Liste des catégories d'évènement</returns>
        public IEnumerable<EvenementCategorieDao> GetAllCategorie ( EvenementDalRequest request )
        {
            if ( request == null || request.Categorie == null )
            {
                try
                {
                    long? param = null;

                    var result = CategorieDalService.GetEvenementCategorie(param);

                    return result.ToCategorieDao();
                }
                catch ( Exception e )
                {
                    new LErreur(e, "Service.Evenement.Dal", "GetAllCategorie Error", 0).Save(LoggerUri);
                }
            }
            try
            {
                var result2 = CategorieDalService.GetEvenementCategorie(request.Categorie.Id);
                return result2.ToCategorieDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetAllCategorie Error", 0).Save(LoggerUri);
            }
            return null;            
        }

        /// <summary>
        /// Récupère l'image grâce a l'évènement id
        /// </summary>
        /// <param name="request"> Dal request </param>
        /// <returns>Liste d'images</returns>
        public IEnumerable<EventImageDao> GetImageByEventId ( EvenementDalRequest request )
        {
            if ( request == null )
                return null;
            try
            {
                // Tricks a enlever dans la version finale ( Boxing du 64 vers 32 ) Drop & Create PROC + Refresh Dataset
                var result = ImageDalService.GetImagesByEventId(request.EvenementId);
                return result.ToImageDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetImageByEventId Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Création d'une image liée à un évènement
        /// </summary>
        /// <param name="image">Image a crée</param>
        /// <returns>L'image crée</returns>
        public IEnumerable<EventImageDao> CreateImage (EventImageDao image )
        {
            if (image == null )
                return null;

            try
            {
                // Tricks a enlever dans la version finale ( Boxing du 64 vers 32 ) Drop & Create PROC + Refresh Dataset
                var result = ImageDalService.AddPhotoToEvent(image.EvenementId, image.Url.ToString());
                return result.ToImageDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "CreateImage Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Permet de supprimer une Image relative à un évènement
        /// </summary>
        /// <param name="request">requete Dal</param>
        /// <returns>Code retour concernant l'opération</returns>
        public int DeleteImage ( EvenementDalRequest request )
        {
            if ( request == null || request.ImageId == 0)
                return 2;

            int result = 0;
            // Tricks a enlever dans la version finale ( Boxing du 64 vers 32 ) Drop & Create PROC + Refresh Dataset
            try
            {
                result = ImageDalService.DeleteImage(request.ImageId);
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "DeleteImage Error", 0).Save(LoggerUri);
            }
            
            return result;
        }

        /// <summary>
        /// Rècupére une liste de lieu en fonction de la ville
        /// </summary>
        /// <param name="request">requete Dal</param>
        /// <returns>Lieu située dans la ville</returns>
        public IEnumerable<EvenementDao> GetLieuEvenementByVille ( EvenementDalRequest request )
        {
            if ( request == null )
                return null;

            try
            {
                var daoObject = LieuEventDalService.GetLieuEvenementByVille(request.Ville.ToString());
                return daoObject.ToEvenementDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetLieuEvenementByVille Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Récupère le lieu en fonction de son codepostale
        /// </summary>
        /// <param name="request"> requete contenant le code Postale </param>
        /// <returns>Lieu choisi</returns>
        public IEnumerable<EvenementDao> GetLieuEvenementByCP ( EvenementDalRequest request )
        {
            if ( request == null )
                return null;

            try
            {
                // Tricks a enlever dans la version finale ( Boxing du 64 vers 32 ) Drop & Create PROC + Refresh Dataset
                var daoObject = LieuEventDalService.GetLieuEvenementByCP(Convert.ToInt32(request.CodePostale));
                return daoObject.ToEvenementDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetLieuEvenementByCP Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Récupère Le lieu en fonction de son id
        /// </summary>
        /// <param name="request">Requete contenant l'id du lieu</param>
        /// <returns>Lieu choisi</returns>
        public IEnumerable<EvenementDao> GetLieuEvenementById ( EvenementDalRequest request )
        {
            if ( request == null )
                return null;
            try
            {
                var daoObject = LieuEventDalService.GetLieuEvenementById(request.LieuEvenementId);
                return daoObject.ToEvenementDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetLieuEvenementById Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Crée un lieu
        /// </summary>
        /// <param name="request">Requete de lieu / Liaison Id Evenement</param>
        /// <param name="location">Lieu a crée</param>
        /// <returns>Lieu</returns>
        public IEnumerable<EvenementDao> CreateLieuEvenement ( EvenementDalRequest request, EventLocationDao location )
        {
            if( request == null || location == null )
                return null;

            try
            {
                // Tricks a enlever dans la version finale ( Boxing du 64 vers 32 ) Drop & Create PROC + Refresh Dataset
                var result = LieuEventDalService.CreateLieu(
                                    location.Ville.ToString(),
                                    location.CodePostale.ToString(),
                                    location.Adresse.ToString(),
                                    location.Longitude,
                                    location.Latitude,
                                    location.Pays.ToString(),
                                    "tt");

                return result.ToEvenementDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "CreateLieuEvenement Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Récupère tout les évènements en fonctions des filtres actifs
        /// </summary>
        /// <param name="date_search">Date de recherche</param>
        /// <param name="premium">Evenement premium</param>
        /// <param name="max_result">Resultat maximum</param>
        /// <param name="categorie">Categorie d'évènement</param>
        /// <param name="max_id"> ??? </param>
        /// <param name="orderby"> Filtre d'ordre </param>
        /// <param name="text_search"> Champs de recherche texte </param>
        /// <param name="startRange"> Offset start </param>
        /// <param name="endRange"> Offset end </param>
        /// <returns>Liste d'évènements</returns>
        public IEnumerable<EvenementDao> GetAllEvenement(DateTime? date_search, bool? premium, int max_result, long? categorie, long? max_id, string orderby = null, string text_search = null, DateTime? startRange = null, DateTime? endRange = null)
        {
            try
            {
                var result = EventDalService.GetEvenements(max_id, categorie, date_search, text_search, premium, max_result, startRange, endRange);

                return result.ToEvenementDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetEvenementByProfil Error", 0).Save(LoggerUri);
            }
            return null;
        }
        /// <summary>
        /// récupère les événements signalés
        /// </summary>
        /// <returns> liste d'événements signalés</returns>
        public IEnumerable<EvenementDao> GetReportedEvents()
        {
            try
            {
                var result = EventDalService.GetReportedEvents();
                return result.ToEvenementDao();

            }
            catch (Exception e)
            {

                new LErreur(e, "Service.Evenement.Dal", "GetReportedEvents Error", 0).Save(LoggerUri);
            }
            return null;

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dept"></param>
        /// <returns></returns>
        public IEnumerable<EvenementDao> GetEvenementByDept(int[] dept)
        {
            try
            {
                List<EvenementDao> result = new List<EvenementDao>();
                foreach (var item in dept)
                {
                    result.AddRange(EventDalService.GetEvenementByDept(item.ToString()).ToEvenementDao());
                }
                return result;
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetEvenementByProfil Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_profil"></param>
        /// <returns></returns>
        public IEnumerable<EvenementDao> GetEvenementByProfil(long id_profil)
        {
            try
            {
                var result = EventDalService.GetEventByProfil(id_profil);

                return result.ToEvenementDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetEvenementByProfil Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Récupère les évenements selon un état donné
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public IEnumerable<EvenementDao> GetEvenementByState(EventStateDao state)
        {
            try
            {
                var result = EventDalService.GetEventByState(state.Id);

                return result.ToEvenementDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetEvenementByState Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Récupère l'évènement grâce a son Code Postal et sa catégorie
        /// </summary>
        /// <param name="request">requete DalService</param>
        /// <returns>L'évènement correspondant</returns>
        public IEnumerable<EvenementDao> GetEvenementByCPAndCategorie ( EvenementDalRequest request )
        {
            if ( request == null )
                return null;
            // Tricks a enlever dans la version finale ( Boxing du 64 vers 32 ) Drop & Create PROC + Refresh Dataset
            try
            {
                var daoObject = EventDalService.GetEventByCPAndCateg(Convert.ToInt32(request.CodePostale), request.Categorie.Libelle.ToString());
                return daoObject.ToEvenementDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetEvenementByCPAndCategorie Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Recupère l'évènement en fonction du code postal
        /// </summary>
        /// <param name="request">request contenant le code postal</param>
        /// <returns>Evenement correspondant</returns>
        public IEnumerable<EvenementDao> GetEvenementByCP ( EvenementDalRequest request )
        {
            if ( request == null )
                return null;
            // Tricks a enlever dans la version finale ( Boxing du 64 vers 32 ) Drop & Create PROC + Refresh Dataset
            try 
            {
                var daoObject = EventDalService.GetEventByCP(Convert.ToInt32(request.CodePostale));
                return daoObject.ToEvenementDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetEvenementByCP Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Mets à jours un évenement
        /// </summary>
        /// <param name="Event">L'évènement a mettre a jours</param>
        /// <returns>Evenement mis a jours</returns>
        public IEnumerable<EvenementDao> UpdateEvenement ( EvenementDao Event )
        {
            if ( Event == null || Event.EventAdresse == null || Event.Categorie == null)
                return null;

            try 
            {
                var result = EventDalService.UpdateEvenement(
                                Event.Id,
                                Event.EventAdresse.Id,
                                Event.Categorie.Id,
                                Event.DateEvenement,
                                DateTime.Now,
                                Event.DateFinInscription,
                                Event.TitreEvenement.ToString(),
                                Event.DescriptionEvenement.ToString(),
                                Event.MinimumParticipant,
                                Event.MaximumParticipant,
                                Event.Price,
                                Event.Premium,
                                DateTime.Now,
                                Event.EventAdresse.Ville.ToString(),
                                Event.EventAdresse.CodePostale.ToString(),
                                Event.EventAdresse.Adresse.ToString(),
                                Event.EventAdresse.Longitude,
                                Event.EventAdresse.Latitude,
                                Event.EventAdresse.Pays.ToString());

                return result.ToEvenementDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "UpdateStateEvenement Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Update l'état d'un évenement
        /// </summary>
        /// <param name="Event">Event a update</param>
        /// <returns>Evenement mis à jours</returns>
        public IEnumerable<EvenementDao> UpdateStateEvenement ( EvenementDao Event )
        {
            if ( Event == null || Event.EtatEvenement == null)
                return null;
            try
            {
                var result = EventDalService.UpdateStateEvent(Event.Id, Event.EtatEvenement.Nom.ToString(), DateTime.Now);

                return result.ToEvenementDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "UpdateStateEvenement Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Crée un evenement
        /// </summary>
        /// <param name="request">requête contenant les paramètres de création</param>
        /// <param name="Event">Evenement a crée</param>
        /// <returns>Evenement crée</returns>
        public IEnumerable<EvenementDao> CreateEvenement ( EvenementDalRequest request, EvenementDao Event )
        {
            if ( Event == null || request == null || Event.EventAdresse == null || Event.Categorie == null)
                return null;
            try
            {
                var result = EventDalService.CreateEvenement(
                                Event.OrganisateurId,
                                Event.Categorie.Id,
                                Event.DateEvenement,
                                DateTime.Now,
                                DateTime.Now,
                                Event.DateFinInscription,
                                Event.TitreEvenement.ToString(),
                                Event.DescriptionEvenement.ToString(),
                                Event.MinimumParticipant,
                                Event.MaximumParticipant,
                                Event.Statut,
                                Event.Price,
                                Event.Premium,
                                Event.DateMiseEnAvant,
                                1, // A voir pour changer
                                Event.EventAdresse.Ville.ToString(),
                                Event.EventAdresse.CodePostale.ToString(),
                                Event.EventAdresse.Adresse.ToString(),
                                Event.EventAdresse.Longitude,
                                Event.EventAdresse.Latitude,
                                Event.EventAdresse.Pays.ToString()
                                );

                return result.ToEvenementDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "CreateEvenement Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Récupère le lieu à partir de sa latitude et sa longitude
        /// </summary>
        /// <param name="latitude">Latitude de l'evenement cherché</param>
        /// <param name="longitude">Longitude de l'évènement cherché</param>
        /// <returns>Evenement si trouvé</returns>
        public EvenementDao GetLieuId(decimal latitude, decimal longitude)
        {
            try
            {
                var result = LieuEventDalService.GetLieuExist(latitude, longitude);
                if ( result != null )
                {
                    if ( result.ToEvenementDao().Count() > 0 )
                    {
                        return null;
                    }
                    else
                    {
                        return result.ToEvenementDao().First();
                    }
                }
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetLieuId Error", 0).Save(LoggerUri);
            }
            return null;
        }

        public EvenementDao getEvenementId(EvenementDalRequest request)
        {
            try
            {
                var daoRequest = EventDalService.GetEvenementById(request.EvenementId);
                return daoRequest.ToEvenementDao().FirstOrDefault();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "getEvenementId Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Permet d'inscrire un utilisateur à une evenement. Si celui-ci participe deja a l'évenement
        /// alors il sera désincrit.
        /// </summary>
        /// <param name="request"> Parametre de requete contenant le userId et l'evenementId</param>
        /// <returns>L'état de l'inscription du user par rapport a cette evenement</returns>
        public IEnumerable<EvenementSubcriberDao> SubscribeEvenement ( EvenementDalRequest request )
        {
            if ( request == null && request.EvenementId == 0 && request.UserId == 0 )
                return null;

            try
            {

                EventDalService.SubscribeOrUnsubscribe(request.EvenementId, request.UserId);
                var result = SubscriptionDalService.GetParticipantByEvent(request.EvenementId);
                return result.ToSubscriberDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "SubscribeEvenement Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Recupere la liste de tout les utilisateurs qui ont souscrit a un evenement, Annulé ou non
        /// </summary>
        /// <param name="request"> Parametre de requete contenant l'evenementId</param>
        /// <returns>La liste des personnes qui sont inscrites a l'evenement</returns>
        public IEnumerable<EvenementSubcriberDao> GetSubscribersByEvent ( EvenementDalRequest request )
        {
            if ( request == null && request.EvenementId == 0 )
                return null;
            try
            {
                var result = SubscriptionDalService.GetParticipantByEvent(request.EvenementId);
                return result.ToSubscriberDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetSubscribersByEvent Error", 0).Save(LoggerUri);
            }
            return null;
        }

        /// <summary>
        /// Recupere la liste de tous les evenements aux quelles un utilisateur s'est inscrit
        /// </summary>
        /// <param name="request"> Parametre de requete contenant le userId</param>
        /// <returns>La liste des personnes qui sont inscrites a l'evenement</returns>
        public IEnumerable<EvenementSubcriberDao> GetSubscriptionByUser ( EvenementDalRequest request )
        {
            if ( request == null && request.UserId == 0 )
                return null;
            try
            {
                var result = SubscriptionDalService.GetEventParticipationByUserId(request.UserId);
                return result.ToSubscriberDao();
            }
            catch ( Exception e )
            {
                new LErreur(e, "Service.Evenement.Dal", "GetSubscriptonByUser Error", 0).Save(LoggerUri);
            }

            return null;
        }

        /// <summary>
        /// Retourne le nombre de participants à un événement
        /// </summary>
        /// <param name="id">Id de l'événement</param>
        /// <returns></returns>
        public int GetParticipantNbByEvent(long id)
        {
            try
            {
                return (int)EventDalService.GetParticipantNbByEvent(id);
            }
            catch (Exception e)
            {
                new LErreur(e, "Service.Evenement.Dal", "GetParticipantNbByEvent Error", 0).Save(LoggerUri);
            }

            return 0;
        }



        #endregion
        
        #region IDisposable
        void IDisposable.Dispose ()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
