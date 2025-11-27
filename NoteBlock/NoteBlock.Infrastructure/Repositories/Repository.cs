using Microsoft.EntityFrameworkCore;
using NoteBlock.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Infrastructure.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity

    {
        protected readonly NoteBLockDbContext _dbContext;

        protected Repository(NoteBLockDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(T e)
        {
            _dbContext.Add(e);
        }

        public void Delete(T e)
        {
            _dbContext.Remove(e);
        }

        public Task<List<T>> FindAllAsync()
        {
            return _dbContext.Set<T>().ToListAsync();
        }

        public Task<T> FindByIdAsync(int id)
        {
            return _dbContext.Set<T>().SingleOrDefaultAsync(x => x.Id == id);
        }

        public void Update(T e)
        {
            _dbContext.Update(e);
        }
    }
}
