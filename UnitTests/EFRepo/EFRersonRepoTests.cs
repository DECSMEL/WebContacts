using Microsoft.VisualStudio.TestTools.UnitTesting;
using EFDAL.EFRepo;
using IDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFDAL.EFRepo.Tests
{
    [TestClass()]
    public class EFRersonRepoTests
    {
        private Person person1 = new Person()
        {
            FirstName = "1A",
            LastName = "1B",
            Email = "d2@gmail.com",
            Password = "test",
            Photo = new Photo() { ImageMimeType = "jpeg"},
            Phones = new List<Phone>()
            {
                new Phone() { Type = PhoneType.Work, Number = "1111"},
                new Phone() { Type = PhoneType.Home, Number = "4444"},
            },            
        };

        private Person person2 = new Person()
        {
            FirstName = "1AA",
            LastName = "1BB",
            Email = "dd2@gmail.com",
            Password = "test",
            Photo = new Photo() { ImageMimeType = "png" },
            Phones = new List<Phone>()
            {
                new Phone() { Type = PhoneType.Work, Number = "2222"},
                new Phone() { Type = PhoneType.Home, Number = "5555"},
            },
        };

        private EFRersonRepo repo = new EFRersonRepo();


        [TestMethod()]
        public void GetAllTest()
        {

        }

        [TestMethod()]
        public void GetByIdTest()
        {
            

        }

        [TestMethod()]
        public void FindTest()
        {

        }

        [TestMethod()]
        public void CreateTest()
        {
            repo.Create(person1);
            repo.Create(person2);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            
        }


        [TestMethod()]
        public void DeleteTest()
        {
            repo.Delete(person1.Email);
            repo.Delete(person2.Email);
        }
    }
}