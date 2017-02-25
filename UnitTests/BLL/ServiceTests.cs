using Microsoft.VisualStudio.TestTools.UnitTesting;
using IDAL.Entities;
using BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Tests
{
    [TestClass()]
    public class ServiceTests
    {
        [TestMethod()]
        public void getPersonByIdTest()
        {
            Service serv = new Service();
            Person pers = serv.getPersonById(19);
            Assert.AreEqual(pers.LastName, "Che");
        }
    }
}