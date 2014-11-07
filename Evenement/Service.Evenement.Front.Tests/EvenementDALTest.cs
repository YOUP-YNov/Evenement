using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service.Evenement.Dal;
using System.Collections.Generic;
using Service.Evenement.Dal.Dao;

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
    }
}
