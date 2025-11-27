using NoteBlock.Domain.SeedWork;
using System.Collections.Generic;

namespace NoteBlock.Domain.Models
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public List<NoteCategory> NoteCategories { get; set; }
        public List<ReminderCategory> ReminderCategories { get; set; }
    }
}
