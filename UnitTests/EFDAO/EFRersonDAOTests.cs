using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFDAL.EFDAO;
using IDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL.EFRepo.Tests
{
    [TestClass()]
    public class EFRersonDAOTests
    {
        private Person person1 = new Person()
        {
            FirstName = "Alex",
            LastName = "Che",
            Email = "d2@gmail.com",
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
            Email = "dd2@gmail.com",
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

        private EFRersonDAO dao = new EFRersonDAO();


        [TestMethod()]
        public void DAOtest()
        {
            dao.Create(person1);
            dao.Create(person2);

            int pers1Id = dao.Find("Che").First().PersonId;

            Person pers = dao.GetById(pers1Id);

            pers.FirstName = "New1";
            pers.Phones.First().Number = "0000";
            pers.Quicklist.First().Name = "Frends";
            pers.Photo.ImageMimeType = "text";
            dao.Update(pers);

            int pers2Id = dao.Find("Bird").First().PersonId;
            dao.Delete(pers1Id);
            dao.Delete(pers2Id);
     
        }

    }
}