using AutoMapper;
using Service.Evenement.Dal.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal;
using Service.Evenement.Dal.Dao.Request;

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
            Mapper.CreateMap<EvenementDao, EvenementBll>();
        }

        public void PutEvenement(EvenementBll evenementBll)
        {
            Mapper.CreateMap<EvenementBll, EvenementDao>();
            EvenementDao daoEvent = Mapper.Map<EvenementBll, EvenementDao>(evenementBll);

            EvenementDalService.UpdateEvenement(daoEvent);
        }

        public IEnumerable<EvenementBll> GetEvenements(DateTime? date_search, int max_result, int categorie, string text_search, int max_id, string orderby, bool? premium)
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
            Mapper.CreateMap<EvenementDao,EvenementBll >();
            Mapper.CreateMap<EventLocationDao, EventLocationBll>();
            Mapper.CreateMap<EvenementCategorieDao,EvenementCategorieBll>();
            Mapper.CreateMap<EventStateDao, EventStateBll>();

            foreach (var item in tmp)
            {
                ret.Add(Mapper.Map<EvenementDao, EvenementBll>(item));
            }

            return ret;
        }
        

        public EvenementBll GetEvenementById(long id)
        {
            Mapper.CreateMap<EvenementBll, EvenementDao>();
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
            Mapper.CreateMap<EvenementDao, EvenementBll>();
            
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
            Mapper.CreateMap<EvenementDao, EvenementBll>();
            Mapper.CreateMap<EventLocationDao, EventLocationBll>();
            Mapper.CreateMap<EvenementCategorieDao, EvenementCategorieBll>();
            Mapper.CreateMap<EventStateDao, EventStateBll>();

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
    }
}
