using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using Service.Evenement.Dal.Interface;
using Service.Evenement.Dal.Dao.Request;
using Service.Evenement.Dal.Dao;
using System.Text;

namespace Service.Evenement.Front.Tests.DAL
{
    [TestClass]
    public class UnitTestEvenementDAL
    {
        [TestMethod]
        public void TestMethod1()
        {
            //var stubEventsProvider = MockRepository.GenerateStub<IEvenementDalService>();

            //EvenementDalRequest request = new EvenementDalRequest();
            //request.UserId = 1;

            //EvenementDao evenement = new EvenementDao();
            //evenement.Categorie = new EvenementCategorieDao() { Id = 2 };
            //evenement.CreateDate = DateTime.Now;
            //evenement.DateEvenement = DateTime.Now;
            //evenement.DateFinInscription = DateTime.Now;
            //evenement.DateMiseEnAvant = DateTime.Now;
            //evenement.DateModification = DateTime.Now;
            //evenement.DescriptionEvenement = new System.Text.StringBuilder("description");
            //evenement.EtatEvenement = new EventStateDao() { Id = 11 };
            //evenement.MaximumParticipant = 10;
            //evenement.MinimumParticipant = 8;
            //evenement.OrganisateurId = 1;
            //evenement.Premium = false;
            //evenement.Price = 0;
            //evenement.TitreEvenement = new System.Text.StringBuilder("titre");

            //var result = stubEventsProvider.CreateEvenement(request, evenement);

            //EvenementDalRequest request = new EvenementDalRequest();

            //EventLocationDao location = new EventLocationDao();

            //location.Adresse = new StringBuilder("89 quai des chartrons");
            //location.CodePostale = new StringBuilder("33300");
            //location.Latitude = 50;
            //location.Longitude = 42;
            //location.Nom = new StringBuilder("Ingésup");
            //location.Pays = new StringBuilder("France");
            //location.Ville = new StringBuilder("Bordeaux");

            //var result = stubEventsProvider.CreateLieuEvenement(request, location);
        }
    }
}
