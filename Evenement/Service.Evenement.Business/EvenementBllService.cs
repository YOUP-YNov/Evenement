using AutoMapper;
using Service.Evenement.Dal.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal;
using Service.Evenement.Dal.Dao.Request;
using Service.Evenement.Business.BusinessModels;

namespace Service.Evenement.Business
{
    public class EvenementBllService
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

        public EvenementBllService ()
        {
            
        }

        public void PutEvenement(EvenementBll evenementBll)
        {
            EvenementDao daoEvent = Mapper.Map<EvenementBll, EvenementDao>(evenementBll);

            EvenementDalService.UpdateEvenement(daoEvent);
        }

        public IEnumerable<EvenementBll> GetEvenements(DateTime? date_search, int max_result, long categorie, string text_search, int max_id, string orderby, bool? premium)
        {

            IEnumerable<Dal.Dao.EvenementDao> tmp = EvenementDalService.GetAllEvenement();
            
            if (date_search != null)
                tmp.Where(e => e.DateEvenement == date_search  && e.Id >= (long)max_id);

            if (categorie != -1)
                tmp.Where(e => e.Categorie.Id == (long)categorie);

            if (text_search != null)
                tmp = tmp.Where(e => e.TitreEvenement.ToString().Contains(text_search));

            if (premium != null)
                tmp = tmp.Where(e => e.Premium == premium);

            if (orderby != null)
                switch (orderby)
                {
                    case "Id": tmp = tmp.OrderBy(e => e.Id); break;
                    case "OrganisateurId": tmp = tmp.OrderBy(e => e.OrganisateurId); break;
                    case "Categorie": tmp = tmp.OrderBy(e => e.Categorie); break;
                    case "DateEvenement": tmp = tmp.OrderBy(e => e.DateEvenement); break;
                    case "TitreEvenement": tmp = tmp.OrderBy(e => e.TitreEvenement); break;
                    case "Price": tmp = tmp.OrderBy(e => e.Price); break;
                    default: break;
                }
            List<EvenementBll> ret = new List<EvenementBll>();
            

            foreach (var item in tmp)
            {
                Mapper.CreateMap<EventLocationDao, EventLocationBll>();
                Mapper.CreateMap<EvenementCategorieDao, EvenementCategorieBll>();
                Mapper.CreateMap<EvenementDao, EvenementBll>();
                ret.Add(Mapper.Map<EvenementDao, EvenementBll>(item));
            }

            return ret;
        }
        

        public EvenementBll GetEvenementById(long id)
        {
           
            EvenementDalRequest request = new EvenementDalRequest();
            request.EvenementId = id;
            var evt = _evenementDalService.getEvenementId(request);
            EvenementBll evtBLL = Mapper.Map<EvenementDao, EvenementBll>(evt);

            return evtBLL;
        }

        /// <summary>
        /// retourne la liste des événements d'un profil 
        /// </summary>
        /// <param name="id_profil">id du profil</param>
        /// <returns>liste d'événements</returns>
        public IEnumerable<EvenementBll> GetByProfil(int id_profil)
        {
            // pour l'instant les event dont le profil est organisateur (api profil pour gerer les event ou le profil est inscrit)
            IEnumerable<EvenementDao> daoEventList = _evenementDalService.GetEvenementByProfil((long)id_profil);
            IEnumerable<EvenementBll> bllEventList = null;
            foreach (EvenementDao e in daoEventList)
            {
                bllEventList.ToList().Add(Mapper.Map<EvenementDao, EvenementBll>(e));
            }           
            return bllEventList;
        }

        /// <summary>
        /// Retourne la liste des événements signalés
        /// </summary>
        /// <returns>Liste d'événements</returns>
        public IEnumerable<EvenementBll> GetReportedEvents()
        {
            IEnumerable<Dal.Dao.EvenementDao> tmp = EvenementDalService.GetAllEvenement();
            IEnumerable<EvenementBll> events = from e in tmp
                                                       where e.EtatEvenement.Nom == Dal.Dao.EventStateEnum.Signaler
                                                       select Mapper.Map<EvenementDao, EvenementBll>(e);
            return events;
        }

        /// <summary>
        /// Retourne l'état d'un événement
        /// </summary>
        /// <param name="id">id de l'événement</param>
        /// <returns>état de l'événement</returns>
        public EventStateBll GetEventState(int id)
        {
            return GetEvenementById(id).EtatEvenement;
        }
        /// <summary>
        /// Permet de modifier l'état d'un événement 
        /// </summary>
        /// <param name="id">id de l'événement</param>
        /// <returns>état de l'événement</returns>
        public void ModifyEventState(int id, EventStateBll state)
        {
            EvenementDalRequest request = new EvenementDalRequest();
            request.EvenementId = id;
            EvenementDao eventDao = EvenementDalService.getEvenementId(request);

            switch (state.Nom)
        {
                case EventStateEnum.Annuler:
                    eventDao.EtatEvenement = new EventStateDao(Dal.Dao.EventStateEnum.Annuler);
                    break;
                case EventStateEnum.AValider:
                    eventDao.EtatEvenement = new EventStateDao(Dal.Dao.EventStateEnum.AValider);
                    break;
                case EventStateEnum.Desactiver:
                    eventDao.EtatEvenement = new EventStateDao(Dal.Dao.EventStateEnum.Desactiver);
                    break;
                case EventStateEnum.Reussi:
                    eventDao.EtatEvenement = new EventStateDao(Dal.Dao.EventStateEnum.Reussi);
                    break;
                case EventStateEnum.Signaler:
                    eventDao.EtatEvenement = new EventStateDao(Dal.Dao.EventStateEnum.Signaler);
                    break;
                case EventStateEnum.Valide:
                    eventDao.EtatEvenement = new EventStateDao(Dal.Dao.EventStateEnum.Valide);
                    break;
                default:
                    break;
            }

            EvenementDalService.UpdateStateEvenement(eventDao);
        }

        private long locationExistOrCreate(EventLocationBll loc)
        {
            if (loc != null)
            {
                //Penser a set l id
                Mapper.CreateMap<EventLocationBll, EventLocationDao>();

                EventLocationDao locDAO = Mapper.Map<EventLocationBll, EventLocationDao>(loc);
                IEnumerable<EvenementDao> e = EvenementDalService.CreateLieuEvenement(new EvenementDalRequest(), locDAO);
                if (e != null)
                {
                    EvenementDao evenement = e.First();
                    
                    return evenement.EventAdresse.Id;
                }
            } 
            return 0;
        }

        /// <summary>
        /// Permet de désactiver un événement
        /// </summary>
        /// <param name="eventId">id de l'événement à désactiver</param>
        public void DeactivateEvent(int eventId)
        {
            EvenementDao eventDao = new EvenementDao();
            eventDao.Id = eventId;
            eventDao.EtatEvenement = new EventStateDao(Service.Evenement.Dal.Dao.EventStateEnum.Desactiver);
            eventDao.DateModification = DateTime.Now;

            EvenementDalService.UpdateStateEvenement(eventDao);
        }

        /// <summary>
        /// Permet de récupèrer la liste de toutes les inscriptions d'un utilisateur
        /// </summary>
        /// <param name="UserId">Id de l'utilisateur</param>
        /// <returns>Liste des evenements souscrit par l'utilisateur</returns>
        public IEnumerable<EvenementSubscriberBll> GetSubscriptionByUser ( int UserId )
        {
            IEnumerable<EvenementSubscriberBll> result = null;
            EvenementDalRequest request = new EvenementDalRequest()
            {
                UserId = UserId
            };

            IEnumerable<EvenementSubcriberDao> daoResult = EvenementDalService.GetSubscriptionByUser(request);

            if ( daoResult != null )
            {
                result = Mapper.Map<IEnumerable<EvenementSubcriberDao>, IEnumerable<EvenementSubscriberBll>>(daoResult);
            }
            return result;
        }

        /// <summary>
        /// Permet à un utilisateurs de s'incrire ou de se désincrire à un évènement
        /// </summary>
        /// <param name="UserId">Id de l'utilisateur</param>
        /// <param name="EvenementId">Id de l'evenement</param>
        /// <returns>L'inscription de l'utilisateur à l'évènement</returns>
        public EvenementSubscriberBll SubscribeEvenement ( int UserId, int EvenementId )
        {
            EvenementDalRequest request = new EvenementDalRequest()
            {
                UserId = UserId,
                EvenementId = EvenementId
            };

            var daoResult = EvenementDalService.SubscribeEvenement(request).FirstOrDefault();

            if ( daoResult == null )
                return null;

            EvenementSubscriberBll result = Mapper.Map<EvenementSubcriberDao, EvenementSubscriberBll>(daoResult);

            return result;
        }

        /// <summary>
        /// Permet de récupèrer la liste des personnes inscrites à un évènement
        /// </summary>
        /// <param name="EvenementId">Id de l'évènement</param>
        /// <returns>Liste des inscriptions relative à cette évènement</returns>
        public IEnumerable<EvenementSubscriberBll> GetSubscribersByEvent ( int EvenementId )
        {
            EvenementDalRequest request = new EvenementDalRequest()
            {
                EvenementId = EvenementId
            };

            var daoResult = EvenementDalService.GetSubscribersByEvent(request);

            if ( daoResult == null )
                return null;

            IEnumerable<EvenementSubscriberBll> result = Mapper.Map<IEnumerable<EvenementSubcriberDao>, IEnumerable<EvenementSubscriberBll>>(daoResult);

            return result;
        }
    }
}
