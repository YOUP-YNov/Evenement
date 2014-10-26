﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvenementRow = Service.Evenement.Dal.Dal.EventDalService.EvenementRow;
using EvenementTable = Service.Evenement.Dal.Dal.EventDalService.EvenementDataTable;
using LieuEvenementRow = Service.Evenement.Dal.Dal.EventDalService.LieuEvenementRow;
using LieuEvenementTable = Service.Evenement.Dal.Dal.EventDalService.LieuEvenementDataTable;
using EvenementDao = Service.Evenement.Dal.Dao.Evenement;
using EvenementImageDao = Service.Evenement.Dal.Dao.EventImage;
using EvenementLocationDao = Service.Evenement.Dal.Dao.EventLocation;
using EvenementStateDao = Service.Evenement.Dal.Dao.EventState;
using EvenementStateEnumDao = Service.Evenement.Dal.Dao.EventStateEnum;

namespace Service.Evenement.Dal.Mappeur
{
    internal static class EvenementMappeur
    {
        #region Evenement
        internal static IEnumerable<EvenementDao> ToEvenementDao(this EvenementTable EventTable)
        {
            if ( EventTable == null && EventTable.Rows == null )
                return null;

            List<EvenementDao> result = new List<EvenementDao>();

            foreach ( EvenementRow EventRow in EventTable )
            {
                EvenementDao daoResult = EventRow.ToEvenementDao();
                if ( daoResult != null )
                    result.Add(daoResult);
            }

            return result;
        }

        internal static EvenementDao ToEvenementDao ( this EvenementRow EvenementRow )
        {
            if ( EvenementRow == null )
                return null;

            EvenementDao result = new EvenementDao();
            result.EtatEvenement = new EvenementStateDao();
            result.EventAdresse = new EvenementLocationDao();
            result.Galleries = new List<EvenementImageDao>();

            result.CategorieId = EvenementRow.Categorie_id;
            result.Categorie = new StringBuilder(EvenementRow.Libelle);
            result.CreateDate = EvenementRow.DateCreation;
            result.DateEvenement = EvenementRow.DateEvenement;
            result.DateFinInscription = EvenementRow.DateFinInscription;
            result.DateMiseEnAvant = EvenementRow.DateMiseEnAvant;
            result.DateModification = EvenementRow.DateModification;
            result.DescriptionEvenement = EvenementRow.IsDescriptionEvenementNull() ? new StringBuilder() : new StringBuilder(EvenementRow.DescriptionEvenement);
            result.EtatEvenement = EvenementRow.ToEvenementStateDao();
            result.EventAdresse = EvenementRow.ToEvenementAdresseDao();
            result.Id = EvenementRow.Evenement_id;
            result.MaximumParticipant = EvenementRow.IsMaximumParticipantNull() ? 0 : EvenementRow.MaximumParticipant;
            result.MinimumParticipant = EvenementRow.IsMinimumParticipantNull() ? 0 : EvenementRow.MinimumParticipant;
            result.OrganisateurId = EvenementRow.Utilisateur_id;
            result.Premium = EvenementRow.Premium;
            result.Price = EvenementRow.Prix;
            result.TitreEvenement = new StringBuilder(EvenementRow.TitreEvenement);
            result.Statut = EvenementRow.Statut;
            return result;
        }

        internal static EvenementStateDao ToEvenementStateDao ( this EvenementRow param )
        {
            EvenementStateDao result = new EvenementStateDao();

            if ( param == null )
                return result;
            result.Id = param.Etat_id;
            EvenementStateEnumDao outputEnum;
            result.Nom = Enum.TryParse<EvenementStateEnumDao>(param.StateName, true, out outputEnum) ? outputEnum : EvenementStateEnumDao.Unspecified;

            return result;
        }

        internal static EvenementLocationDao ToEvenementAdresseDao ( this EvenementRow param )
        {
            EvenementLocationDao result = new EvenementLocationDao();

            if ( param == null )
                return result;

            result.Adresse = new StringBuilder(param.Adresse);
            result.CodePostale = param.IsCodePostaleNull() ? new StringBuilder() : new StringBuilder(param.CodePostale);
            result.Id = param.LieuEvenement_id;
            result.Latitude = param.IsLatitudeNull() ? 0 : param.Latitude;
            result.Longitude = param.IsLongitudeNull() ? 0 : param.Longitude;
            result.Nom = param.IsNomNull() ? new StringBuilder() : new StringBuilder(param.Nom);
            result.Pays = new StringBuilder(param.Pays);
            result.Ville = new StringBuilder(param.Ville);

            return result;
        }
        #endregion

        #region EventLieu

        internal static IEnumerable<EvenementDao> ToEvenementDao ( this LieuEvenementTable EventTable )
        {
            if ( EventTable == null && EventTable.Rows == null )
                return null;

            List<EvenementDao> result = new List<EvenementDao>();

            foreach ( LieuEvenementRow EventRow in EventTable )
            {
                EvenementDao daoResult = EventRow.ToEvenementDao();
                if ( daoResult != null )
                    result.Add(daoResult);
            }

            return result;
        }

        internal static EvenementDao ToEvenementDao ( this LieuEvenementRow EvenementRow )
        {
            if ( EvenementRow == null )
                return null;

            EvenementDao result = new EvenementDao();
            result.EtatEvenement = new EvenementStateDao();
            result.EventAdresse = new EvenementLocationDao();
            result.Galleries = new List<EvenementImageDao>();

            result.EventAdresse = EvenementRow.ToEvenementAdresseDao();

            return result;
        }
        internal static EvenementLocationDao ToEvenementAdresseDao ( this LieuEvenementRow param )
        {
            EvenementLocationDao result = new EvenementLocationDao();

            if ( param == null )
                return result;

            result.Adresse = new StringBuilder(param.Adresse);
            result.CodePostale = param.IsCodePostaleNull() ? new StringBuilder() : new StringBuilder(param.CodePostale);
            result.Id = param.LieuEvenement_id;
            result.Latitude = param.IsLatitudeNull() ? 0 : param.Latitude;
            result.Longitude = param.IsLongitudeNull() ? 0 : param.Longitude;
            result.Nom = param.IsNomNull() ? new StringBuilder() : new StringBuilder(param.Nom);
            result.Pays = new StringBuilder(param.Pays);
            result.Ville = new StringBuilder(param.Ville);

            return result;
        }

        #endregion
    }
}
