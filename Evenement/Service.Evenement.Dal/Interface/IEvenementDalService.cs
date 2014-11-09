using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal.Dao;
using Service.Evenement.Dal.Dao.Request;
using Service.Evenement.Dal.Dal.EventDalServiceTableAdapters;

namespace Service.Evenement.Dal.Interface
{
    public interface IEvenementDalService : IDisposable
    {
        IEnumerable<EvenementCategorieDao> GetAllCategorie ( EvenementDalRequest request );
        IEnumerable<EventImageDao> GetImageByEventId ( EvenementDalRequest request );
        IEnumerable<EventImageDao> CreateImage ( EvenementDalRequest request, EventImageDao image );
        IEnumerable<EvenementDao> GetLieuEvenementByVille ( EvenementDalRequest request );
        IEnumerable<EvenementDao> GetLieuEvenementByCP ( EvenementDalRequest request );
        IEnumerable<EvenementDao> GetLieuEvenementById ( EvenementDalRequest request );
        IEnumerable<EvenementDao> CreateLieuEvenement ( EvenementDalRequest request, EventLocationDao location );
        IEnumerable<EvenementDao> GetAllEvenement ();
        IEnumerable<EvenementDao> GetEvenementByCPAndCategorie ( EvenementDalRequest request );
        IEnumerable<EvenementDao> GetEvenementByCP ( EvenementDalRequest request );
        IEnumerable<EvenementDao> UpdateEvenement ( EvenementDao Event );
        IEnumerable<EvenementDao> UpdateStateEvenement ( EvenementDao Event );
        IEnumerable<EvenementDao> CreateEvenement ( EvenementDalRequest request, EvenementDao Event );


    }
}
