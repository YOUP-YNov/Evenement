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
            var result = LieuEventDalService.CreateLieuEvenement(
                                location.Ville.ToString(),
                                Convert.ToInt32(location.CodePostale),
                                location.Adresse.ToString(),
                                location.Longitude,
                                location.Latitude,
                                location.Pays.ToString(),
                                location.Nom.ToString());

            return result.ToEvenementDao();
        }

        public IEnumerable<EvenementDao> GetAllEvenement ()
        {
            var result = EventDalService.GetAllEvent();

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
            if ( Event == null || Event.EventAdresse == null || Event.Categorie == null || Event.EtatEvenement == null )
                return null;

            var result = EventDalService.UpdateEvenement(
                                Event.Id,
                                Event.EventAdresse.Id,
                                Event.Categorie.Id,
                                Convert.ToInt32(Event.DateEvenement.ToString("ddMMYYhhmmss")),
                                Convert.ToInt32(Event.DateModification.ToString("ddMMYYhhmmss")),
                                Convert.ToInt32(Event.DateFinInscription.ToString("ddMMYYhhmmss")),
                                Event.TitreEvenement.ToString(),
                                Event.DescriptionEvenement.ToString(),
                                Event.MinimumParticipant,
                                Event.MaximumParticipant,
                                Event.Statut,
                                Event.Price,
                                Event.Premium,
                                Convert.ToInt32(Event.DateMiseEnAvant.ToString("ddMMYYhhmmss")),
                                Event.EtatEvenement.Id);

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

            var result = EventDalService.CreateEvent(
                                request.UserId,
                                Event.EventAdresse.Id,
                                Event.Categorie.Id,
                                Convert.ToInt32(Event.DateEvenement.ToString("ddMMYYhhmmss")),
                                Convert.ToInt32(DateTime.Now.ToString("ddMMYYhhmmss")),
                                Convert.ToInt32(Event.DateModification.ToString("ddMMYYhhmmss")),
                                Convert.ToInt32(Event.DateFinInscription.ToString("ddMMYYhhmmss")),
                                Event.TitreEvenement.ToString(),
                                Event.DescriptionEvenement.ToString(),
                                Event.MinimumParticipant,
                                Event.MaximumParticipant,
                                Event.Statut,
                                Event.Price,
                                Event.Premium,
                                Convert.ToInt32(Event.DateMiseEnAvant.ToString("ddMMYYhhmmss")),
                                1 // A voir pour changer
                                );

            return result.ToEvenementDao();
        }

        public EvenementDao GetLieuId(decimal latitude, decimal longitude)
        {

            var result = LieuEventDalService.GetLieuId(latitude, longitude);
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

        #endregion
        
        #region IDisposable
        void IDisposable.Dispose ()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
