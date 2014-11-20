﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Service.Evenement.Dal.Dao;
using Service.Evenement.Dal.Dao.Request;
using Service.Evenement.Dal.Dal.EventDalServiceTableAdapters;

namespace Service.Evenement.Dal.Interface
{
    /// <summary>
    /// Interface représentant le Service Dal Evenement
    /// </summary>
    public interface IEvenementDalService : IDisposable
    {
        IEnumerable<EvenementCategorieDao> GetAllCategorie ( EvenementDalRequest request );
        IEnumerable<EventImageDao> GetImageByEventId ( EvenementDalRequest request );
        IEnumerable<EventImageDao> CreateImage ( EventImageDao image );
        int DeleteImage ( EvenementDalRequest request );
        IEnumerable<EvenementDao> GetLieuEvenementByVille ( EvenementDalRequest request );
        IEnumerable<EvenementDao> GetLieuEvenementByCP ( EvenementDalRequest request );
        IEnumerable<EvenementDao> GetLieuEvenementById ( EvenementDalRequest request );
        IEnumerable<EvenementDao> CreateLieuEvenement ( EvenementDalRequest request, EventLocationDao location );
        IEnumerable<EvenementDao> GetAllEvenement ( DateTime? date_search, bool? premium, int max_result, long? categorie, long? max_id, string orderby = null, string text_search = null, DateTime? startRange = null, DateTime? endRange = null );
        IEnumerable<EvenementDao> GetEvenementByDept ( int dept );
        IEnumerable<EvenementDao> GetEvenementByProfil ( long id_profil );
        IEnumerable<EvenementDao> GetEvenementByState ( EventStateDao state );
        IEnumerable<EvenementDao> GetEvenementByCPAndCategorie ( EvenementDalRequest request );
        IEnumerable<EvenementDao> GetEvenementByCP ( EvenementDalRequest request );
        IEnumerable<EvenementDao> UpdateEvenement ( EvenementDao Event );
        IEnumerable<EvenementDao> UpdateStateEvenement ( EvenementDao Event );
        IEnumerable<EvenementDao> CreateEvenement ( EvenementDalRequest request, EvenementDao Event );
        EvenementDao GetLieuId ( decimal latitude, decimal longitude );
        IEnumerable<EvenementSubcriberDao> SubscribeEvenement ( EvenementDalRequest request );
        IEnumerable<EvenementSubcriberDao> GetSubscribersByEvent ( EvenementDalRequest request );
        IEnumerable<EvenementSubcriberDao> GetSubscriptionByUser ( EvenementDalRequest request );
        
    }
}
