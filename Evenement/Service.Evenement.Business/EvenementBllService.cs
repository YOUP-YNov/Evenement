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
    }
}
