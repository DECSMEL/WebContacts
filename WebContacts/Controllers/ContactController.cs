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

                    int id = contactService.GetIdByEmail(model.Email);
                    Response.SetCookie(new HttpCookie("user", id.ToString()));
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
        public ActionResult Register(ContactEditM model, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    model.Photo.ImageMimeType = image.ContentType;
                    model.Photo.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(model.Photo.ImageData, 0, image.ContentLength);
                }

                contactService.Create(model);
                FormsAuthentication.SetAuthCookie(model.Email, false);
                var userCookie = new HttpCookie("user", contactService.GetIdByEmail(model.Email).ToString());
                Response.SetCookie(userCookie);
            }
            else
            {
                ViewBag.Message = ResourceUI.RegisterFail;
                return View("~/Views/Error");
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
                return View("~/Views/Error");
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
                return View("~/Views/Error");
            }
        }


        [Authorize]
        public ActionResult Details(int id)
        {
            OneResult<ContactVM> contact = contactService.GetContactDetails(id);
            if (contact.IsOk)
            {
                return View("Details", contact.Data);
            }
            else
            {
                ViewBag.Message = contact.Message;
                return View("~/Views/Error");
            };
        }

        #endregion
        
        #region Work with Profile

        [Authorize]
        [HttpGet]
        public ActionResult EditProfile()
        {
            string id = Request.Cookies["user"].Value;
            if (id != null)
            {
                OneResult<ContactEditM> contact = contactService.GetContactForEdit(Int32.Parse(id));
                if (contact.IsOk)
                {
                    contact.Data.Password = "test1234";
                    contact.Data.PasswordValidation = "test1234";
                    return View("EditProfile", contact.Data);
                }
                else
                {
                    ViewBag.Message = contact.Message;
                    return View("~/Views/Error");
                };
            }
            else
            {
                ViewBag.Message = ResourceUI.LoginInvitation;
                return View("~/Views/Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditProfile(ContactEditM model, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
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
            }
            else
            {
                ViewBag.Message = ResourceUI.EditFail;
                return View("~/Views/Error");
            }
            return RedirectToAction("All");
        }

        [Authorize]
        [HttpGet]
        public ActionResult DeleteMyProfile()
        {
            string id = Request.Cookies["user"].Value;
            if (id != null)
            {
                ViewBag.Message = ResourceUI.DeleteConfirm;
                return View("DeleteProfile");
            }
            else
            {
                ViewBag.Message = ResourceUI.LoginError;
                return View("~/Views/Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteMyProfile(DeleteVM model)
        {
            string idStr = Request.Cookies["user"].Value;
            if (idStr != null)
            {
                int id = Int32.Parse(idStr);
                if (contactService.PasswordCheck(id, model.Password))
                {
                    contactService.Delete(id);
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.Message = ResourceUI.DeleteNotConfirm;
                    return View("Delete");
                }
            }
            else
            {
                ViewBag.Message = ResourceUI.LoginError;
                return View("~/Views/Error");
            }
        }

        #endregion
      

    }
}
