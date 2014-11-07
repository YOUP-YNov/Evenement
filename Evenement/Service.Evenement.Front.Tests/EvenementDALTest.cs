using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Evenement.Dal;
using System.Collections.Generic;
using Service.Evenement.Dal.Dao;
using Moq;
using Service.Evenement.Dal.Interface;
using Service.Evenement.Dal.Dao.Request;
using Service.Evenement.Business;

namespace Service.Evenement.Front.Tests
{
    [TestClass]
    public class EvenementDALTest
    {
        [TestMethod]
        public void TestGetAllEvenement()
        {
            EvenementDalService serv = new EvenementDalService();
            List<EvenementDao> test = new List<EvenementDao>(serv.GetAllEvenement());

            Assert.IsNotNull(test);
            Assert.AreEqual(101, test.Count);
        }

        //[TestMethod]
        //public void TestMock()
        //{
        //    var mock = new Mock<IEvenementDalService>();
        //    EvenementDalRequest request = new EvenementDalRequest();
        //    request.UserId = 1;
        //    List<EvenementCategorieDao> categories = new List<EvenementCategorieDao>();
        //    EvenementCategorieDao category = new EvenementCategorieDao()
        //    {
        //        Libelle = new System.Text.StringBuilder("Catégorie de Test")
        //    };

        //    categories.Add(category);

        //    mock.Setup(x => x.GetAllCategorie(request)).Returns(categories);

        //    CategorieBllService service = new CategorieBllService();
        //    service.EvenementDalService = mock.Object;
        //    var test = service.GetCategories();
        //}

        [TestMethod]
        public void TestGetAllEvenementBusiness()
        {
            EvenementBllService serv = new EvenementBllService();
            List<EvenementBll> test = new List<EvenementBll>(serv.GetEvenements(null, 10, -1, null, 10, null, null));

            Assert.IsNotNull(test);
            Assert.AreEqual(10, test.Count);
        }
    }
}
