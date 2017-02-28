using BLL.Converters;
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
        private static IPersonDAO personDao = Unity.Container.Resolve<IPersonDAO>();

        public static bool PasswordCheck(string email, string password)
        {
            return personDao.PasswordCheck(email, password);
        }

        public static bool EmailIsUsed(string email)
        {
            return personDao.EmailIsUsed(email);
        }

        public static int GetIdByEmail(string email)
        {
            return personDao.GetByEmail(email).PersonId;
        }

        public static ListResult<ContactVM> GetAllContacts()
        {
            ListResult<ContactVM> contacts = new ListResult<ContactVM>();
            try
            {
                var persons = personDao.GetAll();
                if (persons != null)
                {
                    foreach (Person pers in persons)
                    {
                        ContactVM contact = ConverterPersonToContact.ForAllView(pers);
                        contacts.ListData.Add(contact);
                    }
                    contacts.IsOk = true;
                    contacts.Message = Resource.GetAllSuccess;
                } else
                {
                    contacts.IsOk = false;
                    contacts.Message = Resource.GetAllNone;
                }               
            }
            catch (Exception)
            {
                //TO DO logging
                contacts.IsOk = false;
                contacts.Message = Resource.GetAllContactFail;
            }
            return contacts;
        }

        public static OneResult<ContactVM> GetContactDetails(int id)
        {
            OneResult<ContactVM> contact = new OneResult<ContactVM>();
            try
            {
                Person person = personDao.GetById(id);
                if (person != null)
                {
                    contact.Data = ConverterPersonToContact.ForDetailView(person);
                    contact.IsOk = true;
                    contact.Message = Resource.GetDetailsSuccess;
                }
            }
            catch (Exception)
            {
                //TO DO logging
                contact.IsOk = false;
                contact.Message = Resource.GetDetailsFail;
            }
            return contact;
        }

        public static OneResult<ContactEditM> GetContactForEdit(int id)
        {
            OneResult<ContactEditM> contact = new OneResult<ContactEditM>();
            try
            {
                Person person = personDao.GetById(id);
                if (person != null)
                {
                    contact.Data = ConverterPersonToContact.ForEditView(person);
                    contact.IsOk = true;
                    contact.Message = Resource.GetDetailsSuccess;
                }
            }
            catch (Exception)
            {
                //TO DO logging
                contact.IsOk = false;
                contact.Message = Resource.GetDetailsFail;
            }
            return contact;

        }

        public static ListResult<ContactVM> FindByLastName(string lastName)
        {
            ListResult<ContactVM> contacts = new ListResult<ContactVM>();
            try
            {
                var persons = personDao.FindByLastName(lastName);
                if (persons != null)
                {
                    foreach (Person pers in persons)
                    {
                        ContactVM contact = ConverterPersonToContact.ForAllView(pers);
                        contacts.ListData.Add(contact);
                    }
                    contacts.IsOk = true;
                    contacts.Message = Resource.FindContactSuccess;
                }
                else
                {
                    contacts.IsOk = false;
                    contacts.Message = Resource.FindNone;
                }
            }
            catch (Exception ex)
            {
                //TO DO logging
                contacts.IsOk = false;
                contacts.Message = Resource.FindContactFail;
            }
            return contacts;
        }

        public static bool Create(ContactEditM contact)
        {
            try
            {
                Person person = ConverterContactToPerson.ForNewSave(contact);
                personDao.Create(person);
                return true;
            }
            catch (Exception ex)
            {
                //TO DO logging
                return false;
            }
        }

        public static bool Edit(ContactEditM contact)
        {
            try
            {
                Person person = ConverterContactToPerson.ForNewSave(contact);
                personDao.Update(person);
                return true;
            }
            catch (Exception ex)
            {
                //TO DO logging
                return false;
            }
        }

    }

}
