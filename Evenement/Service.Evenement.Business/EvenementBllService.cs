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
        /// <summary>
        /// Exemple pour montrer comment on transforme une objet DAO en objet BLL avec AutoMapper
        /// A supprimer !
        /// </summary>
        /// <param name="daoEvent"></param>
        public void ExampleAutoMapper(EvenementDao daoEvent)
        {
            Mapper.CreateMap<EvenementDao, EvenementBll>();
            EvenementBll bllEvent = Mapper.Map<EvenementDao, EvenementBll>(daoEvent);
        }

        private EvenementDalService evenementDalService;

        public EvenementBllService()
        {
            evenementDalService = new EvenementDalService();
        }


        public IEnumerable<EvenementBll> GetEvenements(DateTime? date_search, int max_result = 10, int categorie = -1, string text_search = null, int max_id = -1, string orderby = null)
        {
            Mapper.CreateMap<EvenementDao, EvenementBll>();

            IEnumerable<Dal.Dao.EvenementDao> tmp = evenementDalService.GetAllEvenement();
            
            if (date_search != null)
                tmp.Where(e => e.DateEvenement == date_search  && e.Id >= (long)max_id);

            if (categorie != -1)
                tmp.Where(e => e.Categorie.Id == (long)categorie);

            if (text_search != null)
                tmp = tmp.Where(e => e.TitreEvenement.ToString().Contains(text_search));

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
            IEnumerable<EvenementDao> daoEventList = evenementDalService.GetAllEvenement().Where(e => e.OrganisateurId==id_profil);
            IEnumerable<EvenementBll> bllEventList = null;
            foreach (EvenementDao e in daoEventList)
            {
                bllEventList.ToList().Add(Mapper.Map<EvenementDao, EvenementBll>(e));
            }           
            return bllEventList;
        }

        /// <summary>
        /// Permet de désactiver un événement
        /// </summary>
        /// <param name="eventId">id de l'événement à désactiver</param>
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

            evenementDalService.UpdateStateEvenement(eventDao);
        }
    }
}
