using NoteBlock.Domain.Models;
using NoteBlock.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Domain.Repositories
{
    public interface ICommonUserRepository : IRepository<CommonUser>
    {
        Task<CommonUser> FindByNameAsync(string Name);
        Task<CommonUser> FindByEmailAsync(string Email);
        Task<CommonUser> FindByIdWithDependenciesAsync(int id);
    }
}
