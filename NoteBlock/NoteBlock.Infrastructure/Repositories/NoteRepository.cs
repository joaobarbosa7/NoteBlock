using Microsoft.EntityFrameworkCore;
using NoteBlock.Domain.Models;
using NoteBlock.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Infrastructure.Repositories
{
    public class NoteRepository : Repository<Note>, INoteRepository
    {
        public NoteRepository(NoteBLockDbContext dbContext) : base(dbContext)
        {
        }

        public Task<Note> FindAllFiledByAsync(int Id)
        {
            return _dbContext.Notes.SingleOrDefaultAsync(x => x.IsFiled == true && x.Id == Id);
            
        }

        public Task<Note> FindAllNotFiledAsync(int Id)
        {
            return _dbContext.Notes.SingleOrDefaultAsync(x => x.IsFiled == false && x.Id == Id);

        }

        public Task<List<Note>> FindAllWithDependenciesAsync()
        {
            return _dbContext.Notes
                .Include(x => x.CommonUser)
                .Include(x => x.NoteCategories)
                .ThenInclude(x => x.Category)
                .ToListAsync();
        }

        public Task<Note> FindByIdWithDependenciesAsync(int id)
        {
            return _dbContext.Notes
                .Include(x => x.CommonUser)
                .FirstOrDefaultAsync(x => x.Id == id);

        }


        public Task<Note> FindByTitleAsync(string Title, int Id)
        {
            return _dbContext.Notes.SingleOrDefaultAsync(x => x.Title == Title && x.Id == Id);

        }

        public Task<Note> FindByTitleStartedWithAsync(string Title, int Id)
        {
            return _dbContext.Notes.Where(x => x.Id == Id && x.Title.StartsWith(Title)).SingleOrDefaultAsync();
        }

        public async Task<Note> FindOrCreateAsync(Note e)
        {
            var entity = await FindByTitleAsync(e.Title,e.Id);
            if (entity == null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }
    }
}
