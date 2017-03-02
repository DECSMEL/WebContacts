using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFDAL;
using IDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL.Tests
{
    [TestClass()]
    public class EFRersonDAOTests
    {
        private Person person1 = new Person()
        {
            FirstName = "Alex",
            LastName = "Che",
            Email = "decs@gmail.com",
            Password = "test1",
            Photo = new Photo() { ImageMimeType = "jpeg" },

            Phones = new List<Phone>()
            {
                new Phone() { Type = PhoneType.Work, Number = "1111"},
                new Phone() { Type = PhoneType.Home, Number = "4444"},
            },
            Quicklist = new List<Quicklist>()
            {
                new Quicklist() { Name = "Default1"}
            }
        };

        private Person person2 = new Person()
        {
            FirstName = "Angry",
            LastName = "Bird",
            Email = "alex@gmail.com",
            Password = "test2",
            Photo = new Photo() { ImageMimeType = "png" },
            Phones = new List<Phone>()
            {
                new Phone() { Type = PhoneType.Work, Number = "2222"},
                new Phone() { Type = PhoneType.Home, Number = "5555"},
            },
            Quicklist = new List<Quicklist>()
            {
                new Quicklist() { Name = "Default2"}
            }
        };

        private RersonDAO dao = new RersonDAO();


        [TestMethod()]
        public void CreateTest()
        {
            dao.Create(person1);
            dao.Create(person2);
            var pers1 = dao.FindByLastName("Che").FirstOrDefault();
            var pers2 = dao.FindByLastName("Bird").FirstOrDefault();
            Assert.IsNotNull(pers1);
            Assert.IsNotNull(pers2);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            int pers1Id = dao.FindByLastName("Che").First().PersonId;
            Person pers = dao.GetById(pers1Id);
            pers.FirstName = "New1";
            pers.Phones.First().Number = "0000";
            pers.Photo = null;
            pers.Phones.Add(new Phone() { Number = "0000", PersonId = pers.PersonId });
            dao.Update(pers);
            Person newPers = dao.GetById(pers1Id);
            Assert.AreEqual(pers.FirstName, newPers.FirstName);
            Assert.AreEqual(pers.Phones.First().Number, newPers.Phones.First().Number);
        }

        [TestMethod()]
        public void DeleteTest()
        {
            int pers1Id = dao.FindByLastName("Che").First().PersonId;
            int pers2Id = dao.FindByLastName("Bird").First().PersonId;
            dao.Delete(pers1Id);
            dao.Delete(pers2Id);
            var pers1 = dao.FindByLastName("Che").FirstOrDefault();
            var pers2 = dao.FindByLastName("Bird").FirstOrDefault();
            Assert.IsNull(pers1);
            Assert.IsNull(pers2);
        }
    }
}
