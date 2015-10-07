using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneBook.Repository;
using PhoneBook.ViewModels.ContactViewModel;
using PhoneBook.Services;
using PhoneBook.Entity;
using System.IO;
using System.Text;
using PhoneBook.ViewModels.GroupViewModel;

namespace PhoneBook.Controllers

{
    public class ContactController : Controller
    {
        ContactControllerContactVM model = null;
        ContactControllerEditContactVM EditModel = null;
        ContactRepository contactRepository = new ContactRepository();
        PhoneRepository phoneRepository = new PhoneRepository();

        public ActionResult ListContact()
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Default");
            }
            else
            {
                contactRepository = new ContactRepository();
                User user = (User)Session["User"];
                model = new ContactControllerContactVM();
                TryUpdateModel(model);
                model.ContactList = contactRepository.GetAll(filter: x => x.UserId == user.Id);

                if (ModelState.IsValid)
                {
                    model.ContactList = model.LookingFor == null ? model.FullNameIsChecked == true ? model.ContactList.OrderBy(c => c.FullName).ToList() : model.ContactList.OrderBy(c => c.Email).ToList() :
                    model.FullNameIsChecked == true ? (model.ContactList = contactRepository.GetAll(filter: x => x.FullName.Contains(model.LookingFor))).OrderBy(c => c.FullName).ToList() :
                                                      (model.ContactList = contactRepository.GetAll(filter: x => x.Email.Contains(model.LookingFor))).OrderBy(c => c.Email).ToList();
                }
            }
            return View(model);
        }

        public ActionResult EditContactForm(int id)
        {
            if (AuthenticationService.LoggedUser == null)
            {
                return RedirectToAction("Login", "Default");
            }
            else
            {
                EditModel = new ContactControllerEditContactVM();
                EditModel.UserId = AuthenticationService.LoggedUser.Id;
                if (id != 0)
                {
                    Contact contact = new Contact();
                    contact = contactRepository.GetByID(id);
                    EditModel.FullName = contact.FullName;
                    EditModel.Id = id;
                    EditModel.Email = contact.Email;
                    EditModel.ImageLocation = contact.ImageLocation;
                    EditModel.BirthDay = contact.BirthDay;
                }
                return View(EditModel);
            }
        }

        [HttpPost]
        public ActionResult EditContactForm(int Id, ContactControllerEditContactVM fileModel)
        {
            if (AuthenticationService.LoggedUser == null)
            {
                return RedirectToAction("Login", "Default");
            }

            TryUpdateModel(fileModel);

            if (ModelState.IsValid)
            {
                string directory = null;
                string userDirectory = null;
                string fileLocation = null;
                StringBuilder trailingPath = null;
                string newDirectory = null;
                string oldDirectory = null;
                Contact contact = new Contact();
                if (Id > 0)
                {
                    contact = contactRepository.GetByID(Id);
                    contact.FullName = fileModel.FullName;
                    contact.Email = fileModel.Email;
                    if (fileModel.BirthDay <= new DateTime(1 / 1 / 1753))
                    { fileModel.BirthDay = DateTime.Now; }
                    contact.BirthDay = fileModel.BirthDay;

                    if (fileModel.file != null)
                    {
                        if (contact.ImageLocation != null)
                        {
                            int index = contact.ImageLocation.LastIndexOf(@"/");
                            string str = contact.ImageLocation.Substring(index + 1);
                            oldDirectory = Path.Combine(directory + userDirectory + @"\" + str);
                        }
                        directory = Server.MapPath(@"~/images/");
                        userDirectory = AuthenticationService.LoggedUser.UserName;
                        trailingPath = new StringBuilder(Path.GetExtension(fileModel.file.FileName));
                        trailingPath.Insert(0, Id);
                        fileLocation = Path.Combine(directory, userDirectory, trailingPath.ToString());
                        if (!Directory.Exists(directory + userDirectory))
                        {
                            Directory.CreateDirectory(directory + userDirectory);
                        }
                        fileModel.file.SaveAs(fileLocation);

                        newDirectory = @"/images/" + userDirectory + "/" + trailingPath;

                        if (contact.ImageLocation == null)
                        {
                            contact.ImageLocation = newDirectory;
                        }

                        if (contact.ImageLocation != newDirectory)
                        {
                            System.IO.File.Delete(oldDirectory);
                            contact.ImageLocation = newDirectory;
                        }
                    }
                    contactRepository.Save(contact);
                    return RedirectToAction("ListContact", "Contact");
                }

                if (Id <= 0)
                {
                    contact.UserId = AuthenticationService.LoggedUser.Id;
                    contact.FullName = fileModel.FullName;
                    contact.Email = fileModel.Email;
                    if (fileModel.BirthDay <= new DateTime(1 / 1 / 1753))
                    { fileModel.BirthDay = DateTime.Now; }
                    contact.BirthDay = fileModel.BirthDay;
                    contactRepository.Save(contact);
                    if (fileModel.file != null)
                    {
                        directory = Server.MapPath(@"~/images/");
                        userDirectory = AuthenticationService.LoggedUser.UserName;
                        trailingPath = new StringBuilder(Path.GetExtension(fileModel.file.FileName));
                        trailingPath.Insert(0, contact.Id);
                        fileLocation = Path.Combine(directory, userDirectory, trailingPath.ToString());
                        if (!Directory.Exists(directory + userDirectory))
                        {
                            Directory.CreateDirectory(directory + userDirectory);
                        }
                        fileModel.file.SaveAs(fileLocation);
                        newDirectory = @"/images/" + userDirectory + "/" + trailingPath;
                        contact.ImageLocation = newDirectory;
                        contactRepository.Save(contact);
                    }
                    return RedirectToAction("ListContact", "Contact");
                }
            }
            return View(fileModel);
        }

        public ActionResult Delete(int id)
        {
            Contact contact = contactRepository.GetByID(id);
            if (contact == null)
            {
                HttpNotFound();
            }
            else
            {
                contactRepository.Delete(contact);

                return RedirectToAction("ListContact", "Contact");
            }
            return View();
        }
    }
}