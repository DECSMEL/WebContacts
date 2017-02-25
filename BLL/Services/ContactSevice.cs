using BLL.ResultsModel;
using BLL.ViewModel;
using IDAL;
using IDAL.Entities;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ContactSevice
    {
        private static IPersonDAO dao = Unity.Container.Resolve<IPersonDAO>();
        public static ListResult<ContactVM> GetAllContacts()
        {
            ListResult<ContactVM> contacts = new ListResult<ContactVM>();
            try
            {
                var persons = dao.GetAll();
                foreach (Person pers in persons)
                {
                    contacts.ListData.Add(ConvertPersonToContactVM(pers));
                }
            }
            catch (Exception ex)
            {
                contacts.IsOk = false;
                contacts.Message = ex.Message;
            }
            contacts.IsOk = true;
            contacts.Message = Resource.GetAllSuccess;
            return contacts;
        }


        private static ContactVM ConvertPersonToContactVM(Person person)
        {
            return new ContactVM(person.PersonId, person.FirstName, person.LastName,
                                                ConvertPhotoToPhotoVM(person.Photo),
                                                ConvertListPhoneToVM(person.Phones));              
        }

        private static PhotoVM ConvertPhotoToPhotoVM(Photo photo)
        {
            return new PhotoVM()
            {
                PhotoId = photo.PhotoId,
                IsPrivate = photo.IsPrivate,
                ImageMimeType = photo.ImageMimeType,
                ImageData = photo.ImageData
            };
        }

        private static PhoneVM ConvertPhoneToPhoneVM(Phone phone)
        {
            return new PhoneVM()
            {
                PhoneId = phone.PhoneId,
                Type = phone.Type,
                Number = phone.Number,
                PersonId = phone.PersonId                
            };
        }

        private static List<PhoneVM> ConvertListPhoneToVM(ICollection<Phone> phones)
        {
            List<PhoneVM> newPhones = new List<PhoneVM>();
            foreach (Phone ph in phones)
            {
                newPhones.Add(ConvertPhoneToPhoneVM(ph));
            }
            return newPhones;
        }
    }
}
