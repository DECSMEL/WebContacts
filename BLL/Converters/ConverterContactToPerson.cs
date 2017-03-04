using BLL.ViewModel;
using IDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Converters
{
    class ConverterContactToPerson
    {
        public static Person ForNewSave(ContactEditM contact)
        {
            Person person = new Person();
            person.PersonId = contact.PersonId;
            person.Email = contact.Email;
            person.Password = contact.Password;
            person.FirstName = contact.FirstName;
            person.LastName = contact.LastName;
            if (contact.Phones != null)
            {
                person.Phones = ConvertListPhoneVMtoPhones(contact.Phones);
                foreach (Phone ph in person.Phones)
                {
                    ph.PersonId = person.PersonId;
                    ph.Person = person;
                }
            }
            if (contact.Photo != null)
            {
                person.Photo = ConvertPhotoVMtoPhoto(contact.Photo);
                person.Photo.Person = person;
            }
            
            return person;

        }

        private static IList<Phone> ConvertListPhoneVMtoPhones(IList<PhoneVM> vmPhones)
        {
            IList<Phone> newPhones = new List<Phone>();
            foreach (PhoneVM ph in vmPhones)
            {
                newPhones.Add(ConvertPhoneVMtoPhone(ph));
            }
            return newPhones;
        }

        private static Phone ConvertPhoneVMtoPhone(PhoneVM vmPhone)
        {
            return new Phone()
            {
                PhoneId = vmPhone.PhoneId,
                Type = (PhoneType)vmPhone.Type,
                Number = vmPhone.Number,
            };
        }

        private static Photo ConvertPhotoVMtoPhoto(PhotoVM vmPhoto)
        {
            Photo photo = new Photo()
            {
                PhotoId = vmPhoto.PhotoId,
                IsPrivate = vmPhoto.IsPrivate,
                ImageMimeType = vmPhoto.ImageMimeType,
                ImageData = vmPhoto.ImageData
            };
            return photo;
        }
    }
}
