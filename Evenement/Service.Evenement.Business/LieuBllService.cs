using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal;
using Service.Evenement.Dal.Dao.Request;
using AutoMapper;
using Service.Evenement.Dal.Dao;

namespace Service.Evenement.Business
{
    public class LieuBllService
    {
        private EvenementDalService _evenementDalService;

        public EvenementDalService EvenementDalService
        {
            get
            {
                if (_evenementDalService == null)
                    _evenementDalService = new EvenementDalService();
                return _evenementDalService;
            }
            set
            {
                _evenementDalService = value;
            }
        }

        public void PostLieu(EventLocationBll location)
        {
             Mapper.CreateMap<EventLocationBll, EventLocationDao>();
             EvenementDalService.CreateLieuEvenement(new EvenementDalRequest(), Mapper.Map<EventLocationBll, EventLocationDao>(location));
        }

    }
}
