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
        [HttpGet]
        public ActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public ActionResult Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                bool result = ContactSevice.PasswordCheck(model.Email, model.Password);
                if (result)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, false);
                    int id = ContactSevice.GetIdByEmail(model.Email);
                    Response.SetCookie(new HttpCookie("user", id.ToString()));
                }
                else
                {
                    ModelState.AddModelError("", ResourceUI.FailLogin);
                    return View("Login");
                }
            }
            else
            {
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
            var contact = new ContactEditM();
            return View("Register", contact);

        }

        [HttpPost]
        public ActionResult Register(ContactEditM model)
        {
            if (ModelState.IsValid)
            {
                ContactSevice.Create(model);
                FormsAuthentication.SetAuthCookie(model.Email, false);
                var userCookie = new HttpCookie("user", ContactSevice.GetIdByEmail(model.Email).ToString());
                Response.SetCookie(userCookie);
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
            if (ContactSevice.EmailIsUsed(email))
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

        [Authorize]
        public ActionResult All()
        {
            ListResult<ContactVM> contacts = ContactSevice.GetAllContacts();
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
            ListResult<ContactVM> contacts = ContactSevice.FindByLastName(lastName);
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
            OneResult<ContactVM> contact = ContactSevice.GetContactDetails(id);
            if (contact.IsOk)
            {
                return View("Details", contact.Data);
            }
            else
            {
                ViewBag.Message = contact.Message;
                return View("Error");
            };
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditMyProfile()
        {
            string id = Request.Cookies["user"].Value;
            if (id != null)
            {
                return View("Edit", ContactSevice.GetContactForEdit(Int32.Parse(id)).Data);
            }
            else
            {
                ViewBag.Message = "Login please";
                return View("Error");
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditMyProfile(ContactEditM model)
        {
            if (ModelState.IsValid)
            {
                ContactSevice.Edit(model);
            }
            else
            {
                ViewBag.Message = ResourceUI.EditFail;
                return View("Error");
            }
            return RedirectToAction("All");
        }

    }
}
