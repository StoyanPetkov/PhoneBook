using PhoneBook.Entities;
using PhoneBook.Entity;
using PhoneBook.Repositories;
using PhoneBook.Repository;
using PhoneBook.Services;
using PhoneBook.ViewModels.NoteViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhoneBook.Controllers
{
    public class NoteController : Controller
    {
        NoteRepository NoteRepository = new NoteRepository();
        ContactRepository ContactRepository = new ContactRepository();
        NoteControllerNoteVM model = new NoteControllerNoteVM();

        public ActionResult ListNotes(int parentId)
        {
            if (AuthenticationService.LoggedUser == null)
            {
                return RedirectToAction("Login", "Default");
            }
            else
            {
                if (parentId > 0)
                {
                    Contact contact = new Contact();
                    contact = ContactRepository.GetByID(parentId);
                    model.ContactName = contact.FullName;
                    model.ParentContactId = parentId;
                    model.NoteList = NoteRepository.GetAll(filter: c => c.ContactId == parentId);
                }
            }
            return View(model);
        }

        public ActionResult EditNote(int id)
        {
            if (AuthenticationService.LoggedUser == null)
            {
                return RedirectToAction("Login", "Default");
            }
            else
            {
                model = new NoteControllerNoteVM();
                TryUpdateModel(model);
                Note note = new Note();
                if (model.Id > 0)
                {
                    note = NoteRepository.GetByID(model.Id);
                    model.description = note.Description;
                    model.note = note.Text;
                }
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult EditNote(NoteControllerNoteVM model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            model = new NoteControllerNoteVM();
            TryUpdateModel(model);
            Note note = new Note();
            if (model.Id > 0)
            {
                note = NoteRepository.GetByID(model.Id);
                note.Description = model.description;
                note.Text = model.note;
                note.ContactId = model.ParentContactId;
            }
            else
            {
                note.Description = model.description;
                note.Text = model.note;
                note.ContactId = model.ParentContactId;
            }
            
            NoteRepository.Save(note);

            return RedirectToAction("ListNotes", "Note", new { @parentId = model.ParentContactId });
        }

        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                Note note = new Note();
                note = NoteRepository.GetByID(id);
                NoteRepository.Delete(note);
            }
            model = new NoteControllerNoteVM();
            TryUpdateModel(model);
            return RedirectToAction("ListNotes", "Note", new { @parentId = model.ParentContactId });
        }
    }
}
