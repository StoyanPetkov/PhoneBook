using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneBook.Services;
using PhoneBook.Entity;
using PhoneBook.Repository;
using PhoneBook.ViewModels.DefaultViewModel;
using System.Net;

namespace PhoneBook.Controllers
{
    public class DefaultController : Controller
    {
        DefaultControllerDefaultLoginVM model = new DefaultControllerDefaultLoginVM();
        UserRepository userRepository = new UserRepository();

        public ActionResult Login()
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(DefaultControllerDefaultLoginVM model)
        {
            TryUpdateModel(model);
            if (ModelState.IsValid)
            {
                AuthenticationService.AuthenticateUser(model.Username, model.Password);

                User user = AuthenticationService.LoggedUser;
                if (user != null)
                {
                    Session["User"] = user;
                    return RedirectToAction("ListContact", "Contact");
                }
            }
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}