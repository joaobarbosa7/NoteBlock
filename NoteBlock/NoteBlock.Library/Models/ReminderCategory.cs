namespace NoteBlock.Domain.Models
{
    public class ReminderCategory
    {
        public int ReminderId { get; set; }
        public Reminder Reminder { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
