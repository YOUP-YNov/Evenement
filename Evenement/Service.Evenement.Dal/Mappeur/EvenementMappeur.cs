using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EvenementRow = Service.Evenement.Dal.Dal.EventDalService.EvenementRow;
using EvenementTable = Service.Evenement.Dal.Dal.EventDalService.EvenementDataTable;
using LieuEvenementRow = Service.Evenement.Dal.Dal.EventDalService.LieuEvenementRow;
using LieuEvenementTable = Service.Evenement.Dal.Dal.EventDalService.LieuEvenementDataTable;
using ImageRow = Service.Evenement.Dal.Dal.EventDalService.ImageRow;
using ImageTable = Service.Evenement.Dal.Dal.EventDalService.ImageDataTable;
using CategorieRow = Service.Evenement.Dal.Dal.EventDalService.CategorieRow;
using CategorieTable = Service.Evenement.Dal.Dal.EventDalService.CategorieDataTable;
using EvenementDao = Service.Evenement.Dal.Dao.EvenementDao;
using EvenementImageDao = Service.Evenement.Dal.Dao.EventImageDao;
using EvenementLocationDao = Service.Evenement.Dal.Dao.EventLocationDao;
using EvenementStateDao = Service.Evenement.Dal.Dao.EventStateDao;
using EvenementStateEnumDao = Service.Evenement.Dal.Dao.EventStateEnum;
using Service.Evenement.Dal.Dao;

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
            result.Categorie = new EvenementCategorieDao();
            result.Categorie = EvenementRow.ToEvenementCategorieDao();
            result.CreateDate = EvenementRow.DateCreation;
            result.DateEvenement = EvenementRow.DateEvenement;
            result.DateFinInscription = EvenementRow.DateFinInscription;
            result.DateMiseEnAvant = EvenementRow.IsDateMiseEnAvantNull() ? new DateTime() : EvenementRow.DateMiseEnAvant;
            result.DateModification = EvenementRow.IsDateModificationNull() ? new DateTime() : EvenementRow.DateModification;
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

        #region Image

        internal static IEnumerable<EvenementImageDao> ToImageDao ( this ImageTable ImageTable )
        {
            if ( ImageTable == null && ImageTable.Rows == null )
                return null;

            List<EvenementImageDao> result = new List<EvenementImageDao>();

            foreach ( ImageRow EventRow in ImageTable )
            {
                EvenementImageDao daoResult = EventRow.ToEvenementImageDao();
                if ( daoResult != null )
                    result.Add(daoResult);
            }

            return result;
        }

        internal static EvenementImageDao ToEvenementImageDao ( this ImageRow ImageRow )
        {
            if ( ImageRow == null )
                return null;

            EvenementImageDao result = new EvenementImageDao();

            result.Id = ImageRow.EvenementPhoto_id;
            result.Url = ImageRow.Adresse.ToString();

            return result;
        }

        #endregion

        #region Categorie

        internal static EvenementCategorieDao ToEvenementCategorieDao ( this EvenementRow CategorieRow )
        {
            if ( CategorieRow == null )
                return null;

            EvenementCategorieDao result = new EvenementCategorieDao();

            result.Id = CategorieRow.Categorie_id;
            result.Libelle = new StringBuilder(CategorieRow.Libelle);

            return result;
        }

        internal static IEnumerable<EvenementCategorieDao> ToCategorieDao ( this CategorieTable CategorieTable )
        {
            if ( CategorieTable == null && CategorieTable.Rows == null )
                return null;

            List<EvenementCategorieDao> result = new List<EvenementCategorieDao>();

            foreach ( CategorieRow EventRow in CategorieTable )
            {
                EvenementCategorieDao daoResult = EventRow.ToEvenementCategorieDao();
                if ( daoResult != null )
                    result.Add(daoResult);
            }

            return result;
        }

        internal static EvenementCategorieDao ToEvenementCategorieDao ( this CategorieRow CategorieRow )
        {
            if ( CategorieRow == null )
                return null;

            EvenementCategorieDao result = new EvenementCategorieDao();

            result.Id = CategorieRow.Categorie_id;
            result.Libelle = new StringBuilder(CategorieRow.Libelle);

            return result;
        }

        #endregion
    }
}
