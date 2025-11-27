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
    public class AdminUserRepository : Repository<AdminUser>, IAdminUserRepository
    {
        public AdminUserRepository(NoteBLockDbContext dbContext) : base(dbContext)
        {
        }

        public Task<AdminUser> FindByEmailAsync(string Email)
        {
            return _dbContext.AdminUsers.SingleOrDefaultAsync(c => c.Email == Email);

        }

        public Task<AdminUser> FindByEmployeeNumberAsync(string EmployeeNumber)
        {
            return _dbContext.AdminUsers.SingleOrDefaultAsync(c => c.EmployeeNumber == EmployeeNumber);

        }

        public Task<AdminUser> FindByNameAsync(string Name)
        {
            return _dbContext.AdminUsers.SingleOrDefaultAsync(c => c.Name == Name);
        }

        public async Task<AdminUser> FindOrCreateAsync(AdminUser e)
        {
            var entity = await FindByNameAsync(e.Name);
            if (entity == null)
            {
                Create(e);
                entity = e;
            }
            return entity;
        }
    }
}
