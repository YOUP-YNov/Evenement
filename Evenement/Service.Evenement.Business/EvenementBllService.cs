using AutoMapper;
using Service.Evenement.Dal.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal;

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

            foreach (var item in tmp)
            {
                ret.Add(Mapper.Map<EvenementDao, EvenementBll>(item));
            }

            return ret;
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
            IEnumerable<EvenementDao> daoEventList = EvenementDalService.GetAllEvenement().Where(e => e.OrganisateurId == id_profil);
            IEnumerable<EvenementBll> bllEventList = null;
            foreach (EvenementDao e in daoEventList)
            {
                bllEventList.ToList().Add(Mapper.Map<EvenementDao, EvenementBll>(e));
            }           
            return bllEventList;
        }

        public void DeactivateEvent(int eventId)
        {
            EvenementDao eventDao = new EvenementDao();
            eventDao.Id = eventId;
            eventDao.EtatEvenement = new EventStateDao()
            {
                Id = 16,
                Nom = Dal.Dao.EventStateEnum.Desactiver
            };
            eventDao.DateModification = DateTime.Now;

            EvenementDalService.UpdateStateEvenement(eventDao);
        }
    }
}
