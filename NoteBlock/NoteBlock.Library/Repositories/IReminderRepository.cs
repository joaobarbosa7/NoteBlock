using NoteBlock.Domain.Models;
using NoteBlock.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Domain.Repositories
{
    public interface IReminderRepository : IRepository<Reminder>
    {
        Task<Reminder> FindByTitleAsync(string Title, int UserId);
        Task<Reminder> FindByTitleStartedWithAsync(string Title, int UserId);
        Task<Reminder> FindAllExpiredByAsync(int UserId);
        Task<Reminder> FindAllNotExpiredAsync(int UserId);
        Task<Reminder> FindByIdWithDependenciesAsync(int UserId, int ReminderID);
        Task<List<Reminder>> FindAllWithDependenciesAsync();

    }
}
