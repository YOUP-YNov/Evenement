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

namespace Service.Evenement.Dal
{
    public class EvenementDalService : IEvenementDalService
    {
        #region Données Membres

        private EvenementTableAdapter _eventDalService;

        private LieuEvenementTableAdapter _lieuDalService;

        private ImageTableAdapter _imageDalService;

        private CategorieTableAdapter _categorieDalService;

        private SubscriptionEventTableAdapter _SubscriptionDalService;

        #endregion

        #region Propriétés

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
        public IEnumerable<EvenementCategorieDao> GetAllCategorie ( EvenementDalRequest request )
        {
            if ( request == null || request.Categorie == null )
            {
                long? param = null;

                var result = CategorieDalService.GetEvenementCategorie(param);

                return result.ToCategorieDao();
            }
            
            var result2 = CategorieDalService.GetEvenementCategorie( request.Categorie.Id );

            return result2.ToCategorieDao();
        }

        public IEnumerable<EventImageDao> GetImageByEventId ( EvenementDalRequest request )
        {
            if ( request == null )
                return null;
            // Tricks a enlever dans la version finale ( Boxing du 64 vers 32 ) Drop & Create PROC + Refresh Dataset
            var result = ImageDalService.GetImagesByEventId(Convert.ToInt32(request.EvenementId));
            return result.ToImageDao();
        }

        public IEnumerable<EventImageDao> CreateImage ( EvenementDalRequest request, EventImageDao image )
        {
            if ( request == null || image == null )
                return null;

            // Tricks a enlever dans la version finale ( Boxing du 64 vers 32 ) Drop & Create PROC + Refresh Dataset
            var result = ImageDalService.AddImageToEvent(Convert.ToInt32(request.EvenementId), image.Url.ToString());
            return result.ToImageDao();
        }

        public IEnumerable<EvenementDao> GetLieuEvenementByVille ( EvenementDalRequest request )
        {
            if ( request == null )
                return null;

            var daoObject = LieuEventDalService.GetLieuEvenementByVille(request.Ville.ToString());
            return daoObject.ToEvenementDao();
        }

        public IEnumerable<EvenementDao> GetLieuEvenementByCP ( EvenementDalRequest request )
        {
            if ( request == null )
                return null;
            // Tricks a enlever dans la version finale ( Boxing du 64 vers 32 ) Drop & Create PROC + Refresh Dataset
            var daoObject = LieuEventDalService.GetLieuEvenementByCP(Convert.ToInt32(request.CodePostale));
            return daoObject.ToEvenementDao();
        }

        public IEnumerable<EvenementDao> GetLieuEvenementById ( EvenementDalRequest request )
        {
            if ( request == null )
                return null;

            var daoObject = LieuEventDalService.GetLieuEvenementById(request.LieuEvenementId);
            return daoObject.ToEvenementDao();
        }

        public IEnumerable<EvenementDao> CreateLieuEvenement ( EvenementDalRequest request, EventLocationDao location )
        {
            if( request == null || location == null )
                return null;
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

        public IEnumerable<EvenementDao> GetAllEvenement(DateTime? date_search, bool? premium, int max_result, long? categorie, long? max_id, string orderby = null, string text_search = null)
        {
            var result = EventDalService.GetEvenements(max_id,categorie,date_search,text_search,premium,max_result);

            return result.ToEvenementDao();
        }

        public IEnumerable<EvenementDao> GetEvenementByProfil(long id_profil)
        {
            var result = EventDalService.GetEventByProfil(id_profil);

            return result.ToEvenementDao();
        }

        public IEnumerable<EvenementDao> GetEvenementByCPAndCategorie ( EvenementDalRequest request )
        {
            if ( request == null )
                return null;
            // Tricks a enlever dans la version finale ( Boxing du 64 vers 32 ) Drop & Create PROC + Refresh Dataset
            var daoObject = EventDalService.GetEventByCPAndCateg(Convert.ToInt32(request.CodePostale), request.Categorie.Libelle.ToString());
            return daoObject.ToEvenementDao();
        }

        public IEnumerable<EvenementDao> GetEvenementByCP ( EvenementDalRequest request )
        {
            if ( request == null )
                return null;
            // Tricks a enlever dans la version finale ( Boxing du 64 vers 32 ) Drop & Create PROC + Refresh Dataset
            var daoObject = EventDalService.GetEventByCP(Convert.ToInt32(request.CodePostale));
            return daoObject.ToEvenementDao();
        }

        public IEnumerable<EvenementDao> UpdateEvenement ( EvenementDao Event )
        {
            if ( Event == null || Event.EventAdresse == null || Event.Categorie == null)
                return null;
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

        public IEnumerable<EvenementDao> UpdateStateEvenement ( EvenementDao Event )
        {
            if ( Event == null || Event.EtatEvenement == null)
                return null;

            var result = EventDalService.UpdateStateEvent(Event.Id, Event.EtatEvenement.Nom.ToString(), DateTime.Now);

            return result.ToEvenementDao();
        }

        public IEnumerable<EvenementDao> CreateEvenement ( EvenementDalRequest request, EvenementDao Event )
        {
            if ( Event == null || request == null || Event.EventAdresse == null || Event.Categorie == null)
                return null;

            var result = EventDalService.CreateEvenement(
                                request.UserId,
                                Event.Categorie.Id,
                                Event.DateEvenement,
                                DateTime.Now,
                                null,
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

        public EvenementDao GetLieuId(decimal latitude, decimal longitude)
        {

            var result = LieuEventDalService.GetLieuExist(latitude, longitude);
            if (result != null)
            {
                if(result.ToEvenementDao().Count()>0)
                {
                    return null;
                }
                else
                {
                    return result.ToEvenementDao().First();
                }
            }
            return null;
        }

        public EvenementDao getEvenementId(EvenementDalRequest request)
        {
            var daoRequest = EventDalService.GetEvenementById(request.EvenementId);
            return daoRequest.ToEvenementDao().FirstOrDefault();
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
            var result = SubscriptionDalService.GetParticipationByUserAndEventId(request.EvenementId, request.UserId);
            return result.ToSubscriberDao();
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
            var result = SubscriptionDalService.GetParticipantByEvent(request.EvenementId);
            return result.ToSubscriberDao();
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
            var result = SubscriptionDalService.GetEventParticipationByUserId(request.UserId);
            return result.ToSubscriberDao();
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
