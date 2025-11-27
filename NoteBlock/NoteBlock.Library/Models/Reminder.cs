using NoteBlock.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace NoteBlock.Domain.Models
{
    public class Reminder : Entity
    {
        public string Title { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime LastModificationDate { get; set; }
        public DateTime NotificationDate { get; set; }
        public bool IsExpired { get; set; }

        public int CommonUserId { get; set; }
        public CommonUser CommonUser { get; set; }

        public List<ReminderCategory> ReminderCategories { get; set; }

        public Reminder() { 
        
            CreationDate = DateTime.Now;
            LastModificationDate = DateTime.Now;
            IsExpired = false;
        
        }
    }
}
