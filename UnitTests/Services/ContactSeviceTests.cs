using Microsoft.VisualStudio.TestTools.UnitTesting;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services.Tests
{
    [TestClass()]
    public class ContactSeviceTests
    {
        [TestMethod()]
        public void GetAllContactsTest()
        {
            var persons = ContactSevice.GetAllContacts();

        }
    }
}