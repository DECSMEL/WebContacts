using BLL.ResultsModel;
using BLL.Services;
using BLL.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace WebContacts.Controllers
{
    public class ContactController : Controller
    {
        private ContactService contactService = new ContactService();
        private QuicklistService quicklistService = new QuicklistService();


        #region Login and Registration
        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.Login = ResourceUI.LoginInvitation;
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                bool result = contactService.PasswordCheck(model.Email, model.Password);
                if (result)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                }
                else
                {
                    ViewBag.Login = ResourceUI.FailLogin;
                    return View("Login");
                }
            }
            else
            {
                ViewBag.Login = ResourceUI.LoginError;
                return View("Login");
            }
            return RedirectToAction("All");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Register()
        {
            var contact = new ContactEditM() { Photo = new PhotoVM()};
            return View("Register", contact);

        }

        [HttpPost]
        public ActionResult Register(ContactEditM model, BirthDayVM day = null, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    model.Photo.ImageMimeType = image.ContentType;
                    model.Photo.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(model.Photo.ImageData, 0, image.ContentLength);
                }
                if (day != null)
                {
                    model.BirthDay = day;
                }

                contactService.Create(model);
                FormsAuthentication.SetAuthCookie(model.Email, false);
            }
            else
            {
                ViewBag.Message = ResourceUI.RegisterFail;
                return View("Error");
            }
            return RedirectToAction("All");
        }

        public ActionResult VerifyEmail(string email)
        {
            if (contactService.EmailIsUsed(email))
            {
                return Json($"Email {email} is already in use.", JsonRequestBehavior.AllowGet);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNewPhone()
        {
            var phone = new PhoneVM();
            return PartialView("~/Views/Shared/EditorTemplates/PhoneVM.cshtml", phone);
        }

        public ActionResult AddBirthDay()
        {
            var day = new BirthDayVM();
            return PartialView("~/Views/Shared/EditorTemplates/BirthDayVM.cshtml", day);
        }

        #endregion

        #region Information about another contacts 

        [Authorize]
        public ActionResult All()
        {
            ListResult<ContactVM> contacts = contactService.GetAllContacts();
            if (contacts.IsOk)
            {
                return View("All", contacts.ListData);
            }
            else
            {
                ViewBag.Message = contacts.Message;
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult FindByLastName(string lastName)
        {
            ListResult<ContactVM> contacts = contactService.FindByLastName(lastName);
            if (contacts.IsOk)
            {
                return View("All", contacts.ListData);
            }
            else
            {
                ViewBag.Message = contacts.Message;
                return View("Error");
            }
        }

        [Authorize]
        public ActionResult Details(int id)
        {
            OneResult<ContactVM> contact = contactService.GetContactDetails(id);
            if (contact.IsOk)
            {
                int listId = contactService.GetIdByEmail(User.Identity.Name);
                ViewBag.InFavor = quicklistService.IsInQuicklist(listId, id);
                return View("Details", contact.Data);
            }
            else
            {
                ViewBag.Message = contact.Message;
                return View("Error");
            };
        }

        #endregion
        
        #region Work with Profile

        [Authorize]
        [HttpGet]
        public ActionResult EditProfile()
        {
            int id = contactService.GetIdByEmail(User.Identity.Name);
            if (id != 0)
            {                
                OneResult<ContactEditM> contact = contactService.GetContactForEdit(id);
                if (contact.IsOk)
                {
                    contact.Data.Password = "test1234";
                    contact.Data.PasswordValidation = "test1234";
                    return View("EditProfile", contact.Data);
                }
                else
                {
                    ViewBag.Message = contact.Message;
                    return View("Error");
                };
            }
            else
            {
                ViewBag.Message = ResourceUI.LoginError;
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditProfile(ContactEditM model, BirthDayVM day = null, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid && model.Email.Equals(User.Identity.Name))
            {               
                if (image != null)
                {
                    model.Photo.ImageMimeType = image.ContentType;
                    model.Photo.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(model.Photo.ImageData, 0, image.ContentLength);
                    contactService.Edit(model, true);
                }
                else
                {
                    contactService.Edit(model, false);
                }
                TempData["Info"] = ResourceUI.EditSuccess; 
                return RedirectToAction("All");
            }
            else
            {
                ViewBag.Message = ResourceUI.EditFail;
                return View("Error");
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteMyProfile()
        {
            ViewBag.Message = ResourceUI.DeleteConfirm;
            return View("DeleteProfile");
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteMyProfile(DeleteVM model)
        {
            string userEmail = User.Identity.Name;
            if (contactService.PasswordCheck(userEmail, model.Password))
            {
                int id = contactService.GetIdByEmail(userEmail);
                contactService.Delete(id);
                TempData["Info"] = ResourceUI.DeleteSuccess;
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Message = ResourceUI.DeleteNotConfirm;
                return View("Delete");
            }
        }

        #endregion

        #region Work with Quicklist

        [Authorize]
        public ActionResult GetFavoriteContacts()
        {
            int listId = contactService.GetIdByEmail(User.Identity.Name);
            if (listId != 0)
            {
                ListResult<ContactVM> contacts = quicklistService.GetAllFavoriteContacts(listId);
                if (contacts.IsOk)
                {
                    return View("All", contacts.ListData);
                }
                else
                {
                    ViewBag.Message = contacts.Message;
                    return View("Error");
                }
            }
            else
            {
                ViewBag.Message = ResourceUI.LoginError;
                return View("Error");
            }
        }

        [Authorize]
        public ActionResult AddToFavorite(int contactId)
        {
            int listId = contactService.GetIdByEmail(User.Identity.Name);
            if (listId != 0)
            {
                TempData["Info"] = ResourceUI.QuickAddSuccess;
                quicklistService.AddToQuicklist(listId, contactId);
            }
            else
            {
                ViewBag.Message = ResourceUI.LoginError;
                return View("Error");
            }
            return Redirect(Request.UrlReferrer.ToString());
        }

        [Authorize]
        public ActionResult RemoveFromFavorite(int contactId)
        {
            int listId = contactService.GetIdByEmail(User.Identity.Name);
            if (listId != 0)
            {
                quicklistService.RemoveFromQuicklist(listId, contactId);
                TempData["Info"] = ResourceUI.QuickRemSuccess;
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                ViewBag.Message = ResourceUI.LoginError;
                return View("Error");
            }
        }
        #endregion

    }
}
