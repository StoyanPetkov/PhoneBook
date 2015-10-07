using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneBook.Entity;
using PhoneBook.Repository;
using PhoneBook.Services;
using PhoneBook.ViewModels.ContactViewModel;
using PhoneBook.ViewModels.PhoneViewModel;

namespace PhoneBook.Controllers
{
    public class PhoneController : Controller
    {
        ContactRepository contactRepository = new ContactRepository();
        PhoneRepository phoneRepository = new PhoneRepository();
        PhoneControllerPhoneVM model = null;

        public ActionResult ListPhones(int parentId)
        {
            if (AuthenticationService.LoggedUser == null)
            {
                return RedirectToAction("Login", "Default");
            }
            else
            {
                if (parentId > 0)
                {
                    model = new PhoneControllerPhoneVM();
                    Contact contact = contactRepository.GetByID(parentId);
                    model.ContactName = contact.FullName;
                    model.ParentContactId = parentId;
                    model.PhoneList = phoneRepository.GetAll(filter: x => x.ContactId == parentId);
                }
            }
            return View(model);
        }

        public ActionResult EditPhone(int id)
        {
            if (AuthenticationService.LoggedUser == null)
            {
                return RedirectToAction("Login", "Default");
            }
            else
            {
                model = new PhoneControllerPhoneVM();
                TryUpdateModel(model);
                Phone phone = new Phone();
                if (model.Id != 0)
                {
                    phone = phoneRepository.GetByID(model.Id);
                    model.phone = phone.PhoneNumber;
                }
                if (model.Id > 0 && ModelState.IsValid)
                {

                    phone.PhoneNumber = model.phone;
                    phoneRepository.Save(phone);
                    return RedirectToAction("ListContact", "Contact", new { @parentId = phone.ContactId });
                }
                return View(model);
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult EditPhone(PhoneControllerPhoneVM model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            model = new PhoneControllerPhoneVM();
            TryUpdateModel(model);
            Phone phone = new Phone();

            if (model.Id > 0)
            {
                phone = phoneRepository.GetByID(model.Id);
            }
            phone.PhoneNumber = model.phone;
            phone.ContactId = model.ParentContactId;
            phoneRepository.Save(phone);

            return RedirectToAction("ListPhones", "Phone", new { @parentId = model.ParentContactId });
        }
        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                Phone phone = new Phone();
                phone = phoneRepository.GetByID(id);
                phoneRepository.Delete(phone);
            }
            model = new PhoneControllerPhoneVM();
            TryUpdateModel(model);
            return RedirectToAction("ListPhones", "Phone", new { @parentId = model.ParentContactId });
        }
    }
}