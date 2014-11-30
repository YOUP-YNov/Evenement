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
using SubcriptionUsersTable = Service.Evenement.Dal.Dal.EventDalService.SubscriptionEventDataTable;
using SubcriptionUsersRow = Service.Evenement.Dal.Dal.EventDalService.SubscriptionEventRow;
using EvenementStateEnumDao = Service.Evenement.Dal.Dao.EventStateEnum;
using Service.Evenement.Dal.Dao;

namespace Service.Evenement.Dal.Mappeur
{
    /// <summary>
    /// Classe statique qui assure la mapping entre les Objets fournit par le dataset et les objects
    /// DAO
    /// </summary>
    internal static class EvenementMappeur
    {
        #region Evenement

        /// <summary>
        /// Transforme une EvenementTable en IEnumerable d'EvenementDao
        /// </summary>
        /// <param name="EventTable">Data retourner par le Dataset</param>
        /// <returns>Liste des objets de type Dal</returns>
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

        /// <summary>
        /// Transforme une EvenementRow en EvenementDao
        /// </summary>
        /// <param name="EvenementRow">Représente une ligne renvoyer par le dataset</param>
        /// <returns>Evenement Dao</returns>
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
            result.Topic_id = EvenementRow.IsTopic_idNull() ? 0 : (int)EvenementRow.Topic_id;
            return result;
        }

        /// <summary>
        /// Tranforme une EvenementRow en EvenementStateDao ( représentation objet de l'état de l'évènement )
        /// </summary>
        /// <param name="param">Représente une ligne evenement renvoyer par le dataset </param>
        /// <returns>L'état de l'évenement</returns>
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

        /// <summary>
        /// Tranforme une EvenementRow en EvenementLocationDao ( représentation objet de la localisation de l'évènement )
        /// </summary>
        /// <param name="param">Représente une ligne evenement renvoyer par le dataset</param>
        /// <returns>La localisation de l'évènement</returns>
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

        /// <summary>
        /// Tranforme une LieuEvenementTable en EvenementDao ( Objet global EvenementDao qui contient une Localisation )
        /// </summary>
        /// <param name="EventTable">Représente une table de lieu renvoyer par le dataset</param>
        /// <returns>Liste de lieu</returns>
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

        /// <summary>
        /// Tranforme une LieuEvenementRow en EvenementDao ( Objet global EvenementDao qui contient une Localisation )
        /// </summary>
        /// <param name="EvenementRow">Représente une ligne de lieu renvoyer par le dataset</param>
        /// <returns>Renvoie un evenement contenant une localisation</returns>
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

        /// <summary>
        /// Tranforme une LieuEvenementRow en EvenementLocationDao ( Objet représentant la Localisation )
        /// </summary>
        /// <param name="param">Représente une ligne de lieu renvoyer par le dataset</param>
        /// <returns>Objet EvenementLocationDao</returns>
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

        /// <summary>
        /// Tranforme une ImageTable en IEnumerable<EvenementImageDao> ( Objet représentant les images )
        /// </summary>
        /// <param name="ImageTable">Représente une table d'image renvoyer par le dataset</param>
        /// <returns>Renvoie une liste d'images</returns>
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

        /// <summary>
        /// Tranforme une ImageRow en EvenementImageDao ( Objet représentant les images )
        /// </summary>
        /// <param name="ImageRow">Représente une ligne d'image renvoyer par le dataset</param>
        /// <returns>Renvoie une image</returns>
        internal static EvenementImageDao ToEvenementImageDao ( this ImageRow ImageRow )
        {
            if ( ImageRow == null )
                return null;

            EvenementImageDao result = new EvenementImageDao();

            result.Id = ImageRow.EvenementPhoto_id;
            result.Url = new StringBuilder(ImageRow.Adresse);
            result.EvenementId = ImageRow.Evenement_id;

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

        #region SubcriptionUser

        internal static IEnumerable<EvenementSubcriberDao> ToSubscriberDao ( this SubcriptionUsersTable SubcribersTable )
        {
            if ( SubcribersTable == null && SubcribersTable.Rows == null )
                return null;

            List<EvenementSubcriberDao> result = new List<EvenementSubcriberDao>();

            foreach ( SubcriptionUsersRow EventRow in SubcribersTable )
            {
                EvenementSubcriberDao daoResult = EventRow.ToSubscriberDao();
                if ( daoResult != null )
                    result.Add(daoResult);
            }

            return result;
        }

        internal static EvenementSubcriberDao ToSubscriberDao ( this SubcriptionUsersRow SubcriptionUser )
        {
            if ( SubcriptionUser == null )
                return null;

            EvenementSubcriberDao result = new EvenementSubcriberDao();

            result.DateAnnulation = SubcriptionUser.IsDateAnnulationNull() ? new DateTime() : SubcriptionUser.DateAnnulation;
            result.DateInscription = SubcriptionUser.DateInscription;
            result.Annulation = SubcriptionUser.Annulation;
            result.ParticipationId = SubcriptionUser.Participe_id;
            result.EvenementId = SubcriptionUser.Evenement_id;
            result.UtilisateurId = SubcriptionUser.Utilisateur_id;

            return result;
        }

        #endregion
    }
}
