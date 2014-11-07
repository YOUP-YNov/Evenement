using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Evenement.Dal;
using System.Collections.Generic;
using Service.Evenement.Dal.Dao;
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

        [TestMethod]
        public void TestGetAllEvenementBusiness()
        {
            EvenementBllService serv = new EvenementBllService();
            List<EvenementBll> test = new List<EvenementBll>(serv.GetEvenements(null,10,-1,null,10,null,null));

            Assert.IsNotNull(test);
            Assert.AreEqual(10, test.Count);
        }
    }
}
