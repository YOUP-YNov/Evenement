using AutoMapper;
using Service.Evenement.Dal.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Business.BusinessModels;

namespace Service.Evenement.Business
{
    public class InitBllAutoMappeur
    {
        /// <summary>
        /// Initilisation des Mapping entre la couche Dal et la couche business
        /// </summary>
        public static void initialisation()
        {
            Mapper.CreateMap<EvenementCategorieDao, EvenementCategorieBll>();
            Mapper.CreateMap<EvenementCategorieBll, EvenementCategorieDao>();

            Mapper.CreateMap<EvenementDao, EvenementBll>();
            Mapper.CreateMap<EvenementBll, EvenementDao>();

            Mapper.CreateMap<EventLocationDao, EventLocationBll>();
            Mapper.CreateMap<EventLocationBll, EventLocationDao>();

            Mapper.CreateMap<EventStateDao, EventStateBll>();
            Mapper.CreateMap<EventStateBll, EventStateDao>();

            Mapper.CreateMap<EventImageDao, EventImageBll>();
            Mapper.CreateMap<EventImageBll, EventImageDao>();

            Mapper.CreateMap<EvenementSubcriberDao, EvenementSubscriberBll>();
            Mapper.CreateMap<EvenementSubscriberBll, EvenementSubcriberDao>();
        }
    }
}
