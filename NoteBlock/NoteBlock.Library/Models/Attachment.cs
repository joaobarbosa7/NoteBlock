using NoteBlock.Domain.SeedWork;
using NoteBlock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteBlock.Domain.Models
{
    public class Attachment : Entity
    {
        public string Name { get; set; }
        public string Path { get; set; }

        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
