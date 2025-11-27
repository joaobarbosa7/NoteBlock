using NoteBlock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteBlock.Domain.Models
{
    public class CommonUser : User
    {
        

        public List<Reminder> Reminders { get; }
        public List<Note> Notes { get; }
    }
}
