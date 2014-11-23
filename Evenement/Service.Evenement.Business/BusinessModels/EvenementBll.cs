namespace Service.Evenement.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Service.Evenement.Business.BusinessModels;

    /// <summary>
    /// Model d'accès au données représentant un évenement
    /// </summary>
    public class EvenementBll
    {
        /// <summary>
        /// Assigne ou récupère l'id de l'évenement.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Assigne ou récupère l'id de l'organisateur de l'évenement.
        /// </summary>
        public long OrganisateurId { get; set; }

        /// <summary>
        /// Assigne ou récupère le pseudo de l'organisateur
        /// </summary>
        public string OrganisateurPseudo { get; set; }

        /// <summary>
        /// Assigne ou récupère la photo de l'organisateur
        /// </summary>
        public string OrganisateurImageUrl { get; set; }

        /// <summary>
        /// Assigne ou récupère la liste des HashTag associé à l'évenement
        /// </summary>
        public IEnumerable<StringBuilder> HashTag { get; set; }

        /// <summary>
        /// Assigne ou récupère la liste des Images associé à l'évenement
        /// </summary>
        public IEnumerable<EventImageBll> Galleries { get; set; }

        /// <summary>
        /// Assigne ou récupère l'adresse de l'évenement
        /// </summary>
        public EventLocationBll EventAdresse { get; set; }

        /// <summary>
        /// Assigne ou récupère la catégorie de l'évenement
        /// </summary>
        public EvenementCategorieBll Categorie { get; set; }
        
        /// <summary>
        /// Assigne ou récupère la Date à laquelle l'évenement a lieu.
        /// </summary>
        public DateTime DateEvenement { get; set; }

        /// <summary>
        /// Assigne ou récupère la Date à laquelle l'évenement a lieu.
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Assigne ou récupère la Date à laquelle l'évenement a était crée.
        /// </summary>
        public DateTime DateModification { get; set; }

        /// <summary>
        /// Assigne ou récupère la Date à partir de laquelle les utilisateurs ne peuvent plus
        /// s'incrirent.
        /// </summary>
        public DateTime DateFinInscription { get; set; }

        /// <summary>
        /// Assigne ou récupère le titre de l'évenement
        /// </summary>
        public StringBuilder TitreEvenement { get; set; }

        /// <summary>
        /// Assigne ou récupère la description de l'évenement
        /// </summary>
        public StringBuilder DescriptionEvenement { get; set; }

        /// <summary>
        /// Récupère le nombre de participants enregistrés à l'évènement
        /// </summary>
        public double NbParticipant { get; set; }
        ///// <summary>
        ///// Assigne ou récupère la liste des participants de l'evenementss
        ///// </summary>
        public IEnumerable<EvenementSubscriberBll> Participants { get; set; }

        /// <summary>
        /// Assigne ou récupère le nombre de participant maximum à l'évenement
        /// </summary>
        public int MaximumParticipant { get; set; }

        /// <summary>
        /// Assigne ou récupère le nombre de participant minimum à l'évenement
        /// </summary>
        public int MinimumParticipant { get; set; }

        /// <summary>
        /// Assigne ou récupère l'état de l'évenement
        /// </summary>
        public EventStateBll EtatEvenement { get; set; }

        /// <summary>
        /// Assigne ou récupère le price a payé pour participé à l'évenement
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Assigne ou récupère l'attestation que l'évenement est premium
        /// </summary>
        public bool Premium { get; set; }

        /// <summary>
        /// Assigne ou récupère la date de mise en avant de l'évenement
        /// </summary>
        public DateTime DateMiseEnAvant { get; set; }

        /// <summary>
        /// Assigne ou récupère le statut d'un evenement
        /// </summary>
        public string Statut { get; set; }

          /// <summary>
        /// Assigne ou récupère l'id d'un topic
        /// </summary>
        public int Topic_id { get; set; }

        /// <summary>
        /// Assigne ou récupère la liste des participants à l'évènements
        /// </summary>
        public IEnumerable<EvenementSubscriberBll> Subscribers { get; set; }

        /// <summary>
        /// assigne ou récupère le nombre de participants
        /// </summary>
        public int ParticipantNumbers { get; set; }
    

        public bool evenementUpdateIsValid()
        {
            if (this.Id != null && this.OrganisateurId != null && this.Categorie != null
                && this.TitreEvenement != null && this.DescriptionEvenement != null && this.Price != null)
            {
                if (this.Categorie.Id != null)
                {
                    if (this.MaximumParticipant>this.MinimumParticipant)
                    {
                        if (this.DateEvenement>=this.DateFinInscription)
                        {
                            return true;
                        }

                    }
                }
            }
            return false;
        }
    }
}
