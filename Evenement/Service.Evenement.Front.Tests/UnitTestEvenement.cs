using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;
using Service.Evenement.Dal;


namespace Service.Evenement.Front.Tests
{
    [TestClass]
    public class UnitTestEvenement
    {
        [TestMethod]
        public void TestGetEvenements ()
        {
            EvenementDalService test = new EvenementDalService();

            List<Evenement.Dal.Dao.EvenementDao> list = new List<Dal.Dao.EvenementDao>(test.GetAllEvenement());

            Assert.AreEqual(101, list.Count);
        }
    }
}
