namespace Service.Evenement.ExpositionAPI.Models
{
    using Service.Evenement.Business;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Model d'accès au données représentant un évenement
    /// </summary>
    public class EvenementFront
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
        public IEnumerable<String> HashTag { get; set; }

        /// <summary>
        /// Assigne ou récupère la liste des Images associé à l'évenement
        /// </summary>
        public IEnumerable<EventImageFront> Galleries { get; set; }

        /// <summary>
        /// Assigne ou récupère l'adresse de l'évenement
        /// </summary>
        public EventLocationFront EventAdresse { get; set; }

        /// <summary>
        /// Assigne ou récupère la catégorie de l'évenement
        /// </summary>
        public EvenementCategorieFront Categorie { get; set; }
        
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
        public String TitreEvenement { get; set; }

        /// <summary>
        /// Assigne ou récupère la description de l'évenement
        /// </summary>
        public String DescriptionEvenement { get; set; }

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
        public EventStateFront EtatEvenement { get; set; }

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
        /// Assigne ou récupère la visibilité de l'évènement
        /// </summary>
        public bool Public { get; set; }

        /// <summary>
        /// Assigne ou récupère la visibilité de l'évènement
        /// </summary>
        public bool  Payant { get; set; }

        /// <summary>
        /// Assigne ou récupère l'id d'un topic
        /// </summary>
        public int Topic_id { get; set; }

        public EvenementFront(long organisateur, DateTime end_inscription, DateTime date_event, 
            List<String> keys_words, List<object> friends, 
            int total_people, string description, string title,
            object location, bool? prenium, bool? payant, 
            bool? isPublic, List<Stream> lstPicture = null)
        {
            OrganisateurId = organisateur;
            DateEvenement = date_event;
            DateFinInscription = end_inscription;
            IEnumerable<String> hashTag = new List<String>();
            keys_words.ForEach(t => hashTag.ToList().Add(t));
            HashTag = hashTag;
            /* La liste des amis n'est pas encore géré */
            MaximumParticipant = total_people;
            DescriptionEvenement = description;
            TitreEvenement = title;

            /* gestion de l'adresse à prévoir */
            EventAdresse = new EventLocationFront();
            Premium = prenium ?? false;

            // par défaut un évènementr est public
            Public = isPublic ?? true;

            // par défaut un évènement n'est pas payant.
            Payant = payant ?? false;

            // gerer le flux des images
            List<EventImageFront> test = new List<EventImageFront>();
            Galleries = test;
            

        }

        //Constructeur vide
        public EvenementFront()
        {
        }
    }
}
