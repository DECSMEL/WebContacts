using BLL.ViewModel;
using IDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Converters
{
    class ConverterPersonToContact
    {
        public static ContactVM ForAllView(Person person)
        {
            return new ContactVM(person.PersonId, person.FirstName, person.LastName, person.Email,
                                                ConvertListPhoneToVM(person.Phones));
        }

        public static ContactVM ForDetailView(Person person)
        {
            return new ContactVM(person.PersonId, person.FirstName, person.LastName, person.Email,
                                                ConvertListPhoneToVM(person.Phones),
                                                ConvertPhotoToPhotoVM(person.Photo));
        }

        public static ContactEditM ForEditView(Person person)
        {
            ContactEditM contact = new ContactEditM();
            contact.PersonId = person.PersonId;
            contact.Email = person.Email;
            contact.FirstName = person.FirstName;
            contact.LastName = person.LastName;
            contact.Phones = ConvertListPhoneToVM(person.Phones);
            contact.Photo = ConvertPhotoToEditPhotoVM(person.Photo);
            return contact;   
        }

        private static IList<PhoneVM> ConvertListPhoneToVM(IList<Phone> phones)
        {
            IList<PhoneVM> newPhones = new List<PhoneVM>();
            foreach (Phone ph in phones)
            {
                newPhones.Add(ConvertPhoneToPhoneVM(ph));
            }
            return newPhones;
        }

        private static PhoneVM ConvertPhoneToPhoneVM(Phone phone)
        {
            return new PhoneVM()
            {
                PhoneId = phone.PhoneId,
                Type = (PhoneTypeVM)phone.Type,
                Number = phone.Number,
                PersonId = phone.PersonId
            };
        }

        private static PhotoVM ConvertPhotoToPhotoVM(Photo photo)
        {
            if (photo != null)
            {
                PhotoVM photoView = new PhotoVM()
                {
                    PhotoId = photo.PhotoId,
                    IsPrivate = photo.IsPrivate,
                    ImageMimeType = photo.ImageMimeType,
                };
                if (!photoView.IsPrivate) photoView.ImageData = photo.ImageData;

                return photoView;
            }
            return null;
        }

        private static PhotoVM ConvertPhotoToEditPhotoVM(Photo photo)
        {
            if (photo != null)
            {
                return new PhotoVM()
                {
                    PhotoId = photo.PhotoId,
                    IsPrivate = photo.IsPrivate,
                    ImageMimeType = photo.ImageMimeType,
                    ImageData = photo.ImageData
                };                 
            }
            return null;
        }
    }
}
