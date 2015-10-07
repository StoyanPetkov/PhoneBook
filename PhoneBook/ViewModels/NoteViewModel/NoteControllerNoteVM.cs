using PhoneBook.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PhoneBook.ViewModels.NoteViewModel
{
    public class NoteControllerNoteVM
    {
        public string description { get; set; }
        public string note { get; set; }
        public List<Note> NoteList { get; set; }
        public string ContactName { get; set; }
        public int ParentContactId { get; set; }
        public int Id { get; set; }
    }
}