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
        private static IQuicklistDAO quicklistDao = Unity.Container.Resolve<IQuicklistDAO>();

        public static ListResult<ContactVM> GetAllFavoriteContacts()
        {
            ListResult<ContactVM> contacts = new ListResult<ContactVM>();
            try
            {
                if (true)
                {
                  
                    contacts.IsOk = true;
                    contacts.Message = Resource.GetAllSuccess;
                }
                else
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
    }
}
