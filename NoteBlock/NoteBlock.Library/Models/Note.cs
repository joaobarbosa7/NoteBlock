using NoteBlock.Domain.Models;
using NoteBlock.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace NoteBlock.Domain.Models
{
    public class Note : Entity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public bool IsFavorite { get; set; }
        public bool IsFiled { get; set; }

        public int CommonUserId { get; set; }
        public CommonUser CommonUser { get; set; }

        public List<Attachment> Attachment { get; set; }
        public List<NoteCategory> NoteCategories { get; set; }

        public Note() { 
        
            CreationDate = DateTime.Now;
            LastModificationDate = DateTime.Now;
            IsFiled = false;
            IsFavorite = false;
        }
    }

}
