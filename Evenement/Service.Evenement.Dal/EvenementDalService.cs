using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal.Interface;
using Service.Evenement.Dal.Dal.EventDalServiceTableAdapters;
using Service.Evenement.Dal.Dao;
using Service.Evenement.Dal.Mappeur;
using Events = Service.Evenement.Dal.Dao.Evenement;
using Service.Evenement.Dal.Dal;

namespace Service.Evenement.Dal
{
    public class EvenementDalService : IEvenementDalService
    {
        private EvenementTableAdapter _eventDalService;

        private LieuEvenementTableAdapter _lieuDalService;
        

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
        
        
        public IEnumerable<Events> GetLocationByCityName()
        {
            var test = LieuEventDalService.GetEventLieuByVille("Menai Bridge");

            var result = test.ToEvenementDao();

            return result;
        }

        public IEnumerable<Events> GetLocationByCP()
        {

            return null;
        }

        public IEnumerable<Events> GetAllEvenement()
        {
            var test = EventDalService.GetAllEvent();

            var result = test.ToEvenementDao();
            return result;
        }

        void IDisposable.Dispose ()
        {
            throw new NotImplementedException();
        }
    }
}
