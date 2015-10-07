using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneBook.ViewModels.RegisterViewModel;
using PhoneBook.Repository;
using PhoneBook.Services;
using PhoneBook.Entity;

namespace PhoneBook.Controllers
{
    public class RegisterController : Controller
    {
        UserRepository userRepository = new UserRepository();
        RegisterControllerRegisterVM model = new RegisterControllerRegisterVM();

        public ActionResult RegisterForm()
        {
            return View(model);
        }

        [HttpPost]
        public ActionResult RegisterForm(RegisterControllerRegisterVM model)
        {
            TryUpdateModel(model);
            if (ModelState.IsValid)
            {
                User user = new User();
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.UserName = model.UserName;
                user.Password = model.Password;
                userRepository.Save(user);
                Session["User"] = user;
                AuthenticationService.AuthenticateUser(user.UserName,user.Password);
                return RedirectToAction("ListContact","Contact");
            }
            return View(model);
        }
    }
}