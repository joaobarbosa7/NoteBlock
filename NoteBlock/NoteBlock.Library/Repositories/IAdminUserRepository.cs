using NoteBlock.Domain.Models;
using NoteBlock.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Domain.Repositories
{
    public interface IAdminUserRepository : IRepository<AdminUser>
    {
        Task<AdminUser> FindByNameAsync(string Name);
        Task<AdminUser> FindByEmailAsync(string Email);
        Task<AdminUser> FindByEmployeeNumberAsync(string EmployeeNumber);

    }
}
