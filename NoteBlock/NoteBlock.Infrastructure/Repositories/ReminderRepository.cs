using Microsoft.EntityFrameworkCore;
using NoteBlock.Domain.Models;
using NoteBlock.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Infrastructure.Repositories
{
    public class ReminderRepository : Repository<Reminder>, IReminderRepository
    {
        public ReminderRepository(NoteBLockDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Reminder> FindAllExpiredByAsync(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<Reminder> FindAllNotExpiredAsync(int UserId)
        {
            throw new NotImplementedException();
        }

        public Task<Reminder> FindByIdWithDependenciesAsync(int UserId, int ReminderID)
        {
            throw new NotImplementedException();
        }

        public Task<Reminder> FindByTitleAsync(string Title, int UserId)
        {
            return _dbContext.Reminders.SingleOrDefaultAsync(r => r.Title == Title);
        }

        public Task<Reminder> FindByTitleStartedWithAsync(string Title, int UserId)
        {
            throw new NotImplementedException();
        }

        public async Task<Reminder> FindOrCreateAsync(Reminder e, int UserId)
        {
            var entity = await FindByTitleAsync(e.Title, UserId);
            if (entity==null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }

        public Task<List<Reminder>> FindAllWithDependenciesAsync()
        {
            return _dbContext.Reminders
                .Include(x => x.CommonUser)
                .Include(x => x.ReminderCategories)
                .ThenInclude(x => x.Category)
                .ToListAsync();
        }
    }
}
