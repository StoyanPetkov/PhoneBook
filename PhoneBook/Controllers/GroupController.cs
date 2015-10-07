using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PhoneBook.Entities;
using PhoneBook.Entity;
using PhoneBook.Repositories;
using PhoneBook.Repository;
using PhoneBook.ViewModels.GroupViewModel;
using PhoneBook.Services;

namespace PhoneBook.Controllers
{
    public class GroupController : Controller
    {

        GroupControllerGroupVM model = new GroupControllerGroupVM();

        public ActionResult ListGroups(int parentId)
        {

            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Default");
            }
            else
            {
                ContactRepository ContactRepository = new ContactRepository();
                GroupRepository GroupRepository = new GroupRepository();
                model.UserId = AuthenticationService.LoggedUser.Id;
                model.GroupList = GroupRepository.GetAll(filter: u => u.UserID == parentId);
            }
            return View(model);
        }

        public ActionResult EditGroup(int id)
        {
            if (Session["User"] == null)
            {
                return RedirectToAction("Login", "Default");
            }
            else
            {
                ContactRepository ContactRepository = new ContactRepository();
                GroupRepository GroupRepository = new GroupRepository();
                User user = (User)Session["User"];
                List<Contact> contactList = ContactRepository.GetAll(filter: u => u.UserId == user.Id);
                List<SelectListItem> List = new List<SelectListItem>();

                if (id > 0)
                {
                    Group group = new Group();
                    group = GroupRepository.GetByID(id);
                    model.groupName = group.GroupName;

                    foreach (var item in contactList)
                    {
                        SelectListItem one = null;
                        if (group.Contacts.Any(c => c.Id == item.Id))
                        {
                            one = new SelectListItem { Text = item.FullName, Value = item.Id.ToString(), Selected = true };
                        }
                        else
                        {
                            one = new SelectListItem() { Text = item.FullName, Value = item.Id.ToString(), Selected = false };
                        }
                        List.Add(one);
                    }
                    model.ContactList = List;
                    model.GroupId = id;
                }
                if (id == 0)
                {
                    foreach (var item in contactList)
                    {
                        SelectListItem one = new SelectListItem() { Text = item.FullName, Value = item.Id.ToString() };
                        List.Add(one);
                    }
                    model.ContactList = List;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditGroup(GroupControllerGroupVM model, FormCollection formCollection, int id)
        {
            UnitOfWork Uow = new UnitOfWork();
            ContactRepository contactRepo = new ContactRepository(Uow);
            GroupRepository groupRepo = new GroupRepository(Uow);
            User user = (User)Session["User"];
            List<Contact> contactList = contactRepo.GetAll(filter: u => u.UserId == user.Id);
            List<SelectListItem> List = new List<SelectListItem>();
            Group group = new Group();
            TryUpdateModel(model);
            if (!this.ModelState.IsValid)
            {
                foreach (var item in contactList)
                {
                    SelectListItem one = new SelectListItem() { Text = item.FullName, Value = item.Id.ToString() };
                    List.Add(one);
                }
                model.ContactList = List;
                return View(model);
            }
            else
            {
                group.UserID = user.Id;
                if (id > 0)
                {
                    group = groupRepo.GetByID(id);
                    group.GroupName = model.groupName;
                    var value = formCollection["ContactId"];
                    if (value != null)
                    {
                        string[] arrValue = formCollection["ContactId"].ToString().Split(',');

                        List<Contact> contacts = new List<Contact>();
                        contacts = group.Contacts.ToList();

                        foreach (var item in contacts)
                        {
                            if (!arrValue.Contains(item.Id.ToString()))
                            {
                                group.Contacts.Remove(contactRepo.GetByID(Convert.ToInt32(item.Id)));
                            }
                        }
                        foreach (var item in arrValue)
                        {
                            List<Contact> cont = contactRepo.GetAll(filter: c => c.UserId == user.Id);
                            if (cont.Any(c => c.Id == Convert.ToInt32(item)))
                            {
                                group.Contacts.Add(contactRepo.GetByID(Convert.ToInt32(item)));
                            }
                        }

                    }
                }
                else
                {
                    group.GroupName = model.groupName;
                    group.Contacts = new List<Contact>();
                    var value = formCollection["ContactId"];
                    if (value != null)
                    {
                        string[] arrValue = formCollection["ContactId"].ToString().Split(',');
                        foreach (var item in arrValue)
                        {
                            group.Contacts.Add(contactRepo.GetByID(Convert.ToInt32(item)));
                        }
                    }
                }

                groupRepo.Save(group);
                Uow.Commit();
                model.GroupId = group.Id;
                return RedirectToAction("ListGroups", "Group", new { @parentId = group.UserID });
            }
        }

        public ActionResult Details(int id)
        {
            GroupRepository groupRepo = new GroupRepository();
            Group group = groupRepo.GetByID(id);
            model.Contacts = group.Contacts.ToList();
            model.GroupId = id;
            return View(model);
        }

        public ActionResult Delete(int id)
        {
            GroupRepository groupRepo = new GroupRepository();
            Group group = groupRepo.GetByID(id);
            group.Contacts.Clear();
            groupRepo.Save(group);
            groupRepo.Delete(group);
            return RedirectToAction("ListGroups",new {@parentId = AuthenticationService.LoggedUser.Id });
        }
    }
}