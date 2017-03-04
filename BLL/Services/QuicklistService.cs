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
    public class QuicklistService
    {
        private IQuicklistDAO quicklistDao = Unity.Container.Resolve<IQuicklistDAO>();

        public ListResult<ContactVM> GetAllFavoriteContacts(int contactId)
        {
            ListResult<ContactVM> contacts = new ListResult<ContactVM>();
            try
            {
                var quicklist = quicklistDao.GetById(contactId);
                if(quicklist != null && quicklist.Persons != null)
                {
                    foreach (Person pers in quicklist.Persons)
                    {
                        ContactVM contact = ConverterPersonToContact.ForAllView(pers);
                        contacts.ListData.Add(contact);
                    }
                    contacts.IsOk = true;
                    contacts.Message = Resource.GetAllSuccess;
                }
                else
                {
                    contacts.IsOk = true;
                    contacts.Message = Resource.QuicklistEmpty;
                }
            }
            catch (Exception)
            {
                //TO DO logging
                contacts.IsOk = false;
                contacts.Message = Resource.QuicklistError;
            }
            return contacts;
        }

        public bool AddToQuicklist(int listId, int contactId)
        {
            try
            {
                var quicklist = quicklistDao.GetById(listId);
                if (quicklist == null)
                {
                    quicklistDao.Create(listId);
                    quicklistDao.AddToQuicklist(listId, contactId);
                }
                else
                {
                    quicklistDao.AddToQuicklist(listId, contactId);
                }
            }
            catch(Exception ex)
            {
                //TODO logging
                return false;
            }
            return true;
        }

        public bool RemoveFromQuicklist(int listId, int contactId)
        {
            try
            {
                var quicklist = quicklistDao.GetById(listId);
                if (quicklist != null)
                {
                    quicklistDao.RemoveFromQuicklist(listId, contactId);
                }                
     
            }
            catch (Exception)
            {
                //TODO logging
                return false;
            }
            return true;
        }

    }
}
