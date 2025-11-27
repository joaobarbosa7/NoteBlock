using NoteBlock.Domain;
using NoteBlock.Domain.Models;
using NoteBlock.Domain.Repositories;
using NoteBlock.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private NoteBLockDbContext _dbContext;

        public UnitOfWork()
        {
            _dbContext = new NoteBLockDbContext();
            _dbContext.Database.EnsureCreated();
        }
        public IReminderRepository ReminderRepository => new ReminderRepository(_dbContext);

        public INoteRepository NoteRepository =>  new NoteRepository(_dbContext);

        public ICommonUserRepository CommonUserRepository =>  new CommonUserRepository(_dbContext);

        public IAdminUserRepository AdminUserRepository =>  new AdminUserRepository(_dbContext);

        public ICategoryRepository CategoryRepository =>  new CategoryRepository(_dbContext);

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
