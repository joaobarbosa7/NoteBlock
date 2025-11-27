using Microsoft.EntityFrameworkCore;
using NoteBlock.Domain.Models;
using NoteBlock.Domain.Repositories;
using NoteBlock.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(NoteBLockDbContext dbContext) : base(dbContext)
        {
        }


        public Task<List<Category>> FindAllByNameStartedWithAsync(string name, int UserId)
        {
            return _dbContext.Categories.Where(x => x.Id == UserId && x.Name.StartsWith(name)).OrderBy(x => x.Name).ToListAsync();

        }



        public Task<List<Category>> FindAllWithDependenciesAsync()
        {
            return _dbContext.Categories.Include(x => x.NoteCategories).ToListAsync();
        }

   

        public Task<Category> FindByNameAsync(string name, int UserId)
        {   
            // Começar aqui
            return _dbContext.Categories.SingleOrDefaultAsync(c => c.Id == UserId && c.Name == name);

        }

        public async Task<Category> FindOrCreateAsync(Category e)
        {
            var entity = await FindByNameAsync(e.Name,e.Id);
            if (entity == null) { Create(e);entity = e;} return entity;
        }
    }
}
