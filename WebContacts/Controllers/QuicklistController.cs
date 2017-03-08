using BLL.ResultsModel;
using BLL.Services;
using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebContacts.Controllers
{
    public class QuicklistController : Controller
    {
        private QuicklistService quicklistService = new QuicklistService();

        #region Work with Quicklist

        [Authorize]
        public ActionResult GetFavoriteContacts()
        {
            string id = Request.Cookies["user"].Value;
            if (id != null)
            {
                int contactId = Int32.Parse(id);
                ListResult<ContactVM> contacts = quicklistService.GetAllFavoriteContacts(contactId);
                if (contacts.IsOk)
                {
                    return View("Favorite", contacts.ListData);
                }
                else
                {
                    ViewBag.Message = contacts.Message;
                    return View("~/Views/Error");
                }
            }
            else
            {
                ViewBag.Message = ResourceUI.LoginInvitation;
                return View("~/Views/Error");
            }
        }

        [Authorize]
        public ActionResult AddToFavorite(int contactId)
        {
            string id = Request.Cookies["user"].Value;
            if (id != null)
            {
                int listId = Int32.Parse(id);
                quicklistService.AddToQuicklist(listId, contactId);
            }
            else
            {
                ViewBag.Message = ResourceUI.LoginInvitation;
                return View("~/Views/Error");
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [Authorize]
        public ActionResult RemoveFromFavorite(int contactId)
        {
            string id = Request.Cookies["user"].Value;
            if (id != null)
            {
                int listId = Int32.Parse(id);
                quicklistService.RemoveFromQuicklist(listId, contactId);
            }
            else
            {
                ViewBag.Message = ResourceUI.LoginInvitation;
                return View("~/Views/Error");
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
        #endregion

    }
}