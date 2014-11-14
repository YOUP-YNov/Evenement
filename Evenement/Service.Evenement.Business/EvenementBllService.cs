using AutoMapper;
using Service.Evenement.Dal.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal;
using Service.Evenement.Dal.Dao.Request;
using Service.Evenement.Business.Response;
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

        public ResponseObject CreateEvenement(EvenementBll evenementBll)
        {
            ResponseObject response = new ResponseObject();
            if (evenementBll.EventAdresse.IsValid() && evenementBll.evenementUpdateIsValid())
            {
            EvenementDao daoEvent = Mapper.Map<EvenementBll, EvenementDao>(evenementBll);
                IEnumerable<EvenementDao> result = EvenementDalService.CreateEvenement(new EvenementDalRequest(), daoEvent);
                if (result.Count() > 0)
                {
                        response.State = ResponseState.Created;
                }
                else
                {
                        response.State = ResponseState.NotModified;
                }
            }
            else
            {
                response.State = ResponseState.BadRequest;
            }
            return response;
        }


        public ResponseObject PutEvenement(EvenementBll evenementBll)
        {

            EvenementBll evenement = (EvenementBll)(this.GetEvenementById(evenementBll.Id)).Value;
            ResponseObject response = new ResponseObject();
            if (evenement != null)
            {
                if (evenementBll.EventAdresse.IsValid() && evenementBll.evenementUpdateIsValid())
                {
                    EvenementDao daoEvent = Mapper.Map<EvenementBll, EvenementDao>(evenementBll);
                    IEnumerable<EvenementDao> result = EvenementDalService.UpdateEvenement(daoEvent);
                    if (result.Count() > 0)
                    {
                        response.State = ResponseState.Ok;
        }
                    else
                    {
                        response.State = ResponseState.NotModified;
                    }
                }
                else
                {
                    response.State = ResponseState.BadRequest;
                }
            }
            else
            {
                response.State = ResponseState.NotFound;
            }
            return response;
        }
        /// <summary>
        /// retourne la liste des évènements en fonction d'une date, d'une catégorie, de son statut, de son nom.
        /// </summary>
        /// <param name="date_search"></param>
        /// <param name="max_result"></param>
        /// <param name="categorie"></param>
        /// <param name="max_id"></param>
        /// <param name="premium"></param>
        /// <param name="text_search"></param>
        /// <param name="orderby"></param>
        /// <returns>liste d'évènements</returns>
        public ResponseObject GetEvenements(DateTime? date_search = null, int max_result = 10, long? categorie = -1, long? max_id = null, bool? premium = null, string text_search = null, string orderby = null)
        {

            IEnumerable<Dal.Dao.EvenementDao> tmp = EvenementDalService.GetAllEvenement(date_search,premium, max_result, categorie, max_id,text_search ,orderby);
            IEnumerable<EvenementBll> bllEvent = Mapper.Map < IEnumerable<EvenementDao>, IEnumerable<EvenementBll>>(tmp);
            ResponseObject response = new ResponseObject();
            if (bllEvent != null)
            {
                if(bllEvent.Count()>0)
                {
                    response.State = ResponseState.Ok;
                    response.Value = bllEvent;
                }
                else
                {
                    response.State = ResponseState.NoContent;
                }
            }
            else
            {
                response.State = ResponseState.NotFound;
            }
            return response;
        }
        
        /// <summary>
        /// retourne un évènement par rapport à son ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>évènement</returns>
        public ResponseObject GetEvenementById(long id)
        {
            ResponseObject response = new ResponseObject();
            if(id == null)
            {
                response.State = ResponseState.BadRequest;
            }
            else
        {
            EvenementDalRequest request = new EvenementDalRequest();
            request.EvenementId = id;
                var evt = EvenementDalService.getEvenementId(request);
            EvenementBll evtBLL = Mapper.Map<EvenementDao, EvenementBll>(evt);
                if (evtBLL != null)
                {
                    response.State = ResponseState.Ok;
                    response.Value = evtBLL;
                }
                else
                {
                    response.State = ResponseState.NoContent;
                }
            }


            return response;
        }
        /// <summary>
        /// retourne la liste des évènements d'un département
        /// </summary>
        /// <param name="dept"></param>
        /// <returns>liste d'évènements</returns>
        public ResponseObject GetEvenementByDept( int dept )
        {
            ResponseObject response = new ResponseObject();
            if (dept == default(int))
                response.State = ResponseState.BadRequest;
            else
            {
                var evt = EvenementDalService.GetEvenementByDept(dept);
                IEnumerable<EvenementBll> evtBLL = Mapper.Map<IEnumerable<EvenementDao>, IEnumerable<EvenementBll>>(evt);
                if (evtBLL != null)
                {
                    response.State = ResponseState.Ok;
                    response.Value = evtBLL;
                }
                else response.State = ResponseState.NoContent;
            }
            return response;
        }

        /// <summary>
        /// retourne la liste des événements d'un profil 
        /// </summary>
        /// <param name="id_profil">id du profil</param>
        /// <returns>liste d'événements</returns>
        public ResponseObject GetByProfil(long id_profil)
        {
            // pour l'instant les event dont le profil est organisateur (api profil pour gerer les event ou le profil est inscrit)
             ResponseObject response = new ResponseObject();
             if (id_profil == default(long))
             {
                 response.State = ResponseState.BadRequest;
             }
             else
             {
                 IEnumerable<EvenementDao> daoEventList = EvenementDalService.GetEvenementByProfil(id_profil);
                 if (daoEventList != null)
                 {
                     if (daoEventList.Count() > 0)
                     {
                         response.State = ResponseState.Ok;
                         response.Value = Mapper.Map<IEnumerable<EvenementDao>,IEnumerable<EvenementBll>> (daoEventList);
                     }
                     else
                     {
                         response.State = ResponseState.NoContent;
                     }
                 }
                 else
                 {

                     response.State = ResponseState.NotFound;
                 }
             }

             return response;
        }

        /// <summary>
        /// Retourne la liste des événements signalés
        /// </summary>
        /// <returns>Liste d'événements</returns>
        public ResponseObject GetReportedEvents()
        {
            IEnumerable<EvenementDao> daoEventList = EvenementDalService.GetEvenementByState(new EventStateDao(Dal.Dao.EventStateEnum.Signaler));
            ResponseObject response = new ResponseObject();
           
            if (daoEventList != null)
            {
                if (daoEventList.Count() > 0)
                {
                    response.State = ResponseState.Ok;
                    response.Value = Mapper.Map<IEnumerable<EvenementDao>, IEnumerable<EvenementBll>>(daoEventList);
                }
                else
                {
                    response.State = ResponseState.NoContent;
                }
            }
            else
            {

                response.State = ResponseState.NotFound;
            }
            

            return response;
            //IEnumerable<Dal.Dao.EvenementDao> tmp = EvenementDalService.GetAllEvenement();
            //IEnumerable<EvenementBll> events = from e in tmp
                                                      // where e.EtatEvenement.Nom == Dal.Dao.EventStateEnum.Signaler
                                                       //select Mapper.Map<EvenementDao, EvenementBll>(e);
            //return null;
        }

        /// <summary>
        /// Retourne l'état d'un événement
        /// </summary>
        /// <param name="id">id de l'événement</param>
        /// <returns>état de l'événement</returns>
        public EventStateBll GetEventState(int id)
        {
            //return GetEvenementById(id).EtatEvenement;
            return null;
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
