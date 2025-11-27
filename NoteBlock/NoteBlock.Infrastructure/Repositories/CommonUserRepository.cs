using Microsoft.EntityFrameworkCore;
using NoteBlock.Domain.Models;
using NoteBlock.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NoteBlock.Infrastructure.Repositories
{
    public class CommonUserRepository : Repository<CommonUser>, ICommonUserRepository
    {
        public CommonUserRepository(NoteBLockDbContext dbContext) : base(dbContext)
        {
        }

        public Task<CommonUser> FindByEmailAsync(string Email)
        {
            return _dbContext.CommonUsers.SingleOrDefaultAsync(c => c.Email == Email);

        }

        public Task<CommonUser> FindByIdWithDependenciesAsync(int id)
        {
            return _dbContext.CommonUsers
                .Include(x => x.Notes)
                .Include(x => x.Reminders)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<CommonUser> FindByNameAsync(string Name)
        {
            return _dbContext.CommonUsers.SingleOrDefaultAsync(c => c.Name == Name);
        }

        public async Task<CommonUser> FindOrCreateAsync(CommonUser e)
        {
           var entity = await FindByNameAsync(e.Name);
            if(entity == null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }
    }
}
