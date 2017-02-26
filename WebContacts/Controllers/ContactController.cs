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
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
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


        public ActionResult All()
        {
            ListResult<ContactVM> contacts = ContactSevice.GetAllContacts();
            if (contacts.IsOk)
            {
                return View("All", contacts.ListData);
            }
            else
            {
                return View("Error", contacts.Message);
            }
        }

        public ActionResult Details(int id)
        {
            OneResult<ContactVM> contact = ContactSevice.GetContactDetails(id);
            if (contact.IsOk)
            {
                return View("Details", contact.Data);
            }
            else
            {
                return View("Error", contact.Message);
            };
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View("Register");

        }

        [HttpPost]
        public ActionResult Register(ContactEditM model)
        {

            ContactSevice.Create(model);

            return RedirectToAction("All");
        }

    }
}
