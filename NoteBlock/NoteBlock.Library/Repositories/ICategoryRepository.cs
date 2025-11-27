using NoteBlock.Domain.Models;
using NoteBlock.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Domain.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<Category> FindByNameAsync(string name,int UserId);
        Task<List<Category>> FindAllByNameStartedWithAsync(string name,int UserId);
        Task<List<Category>> FindAllWithDependenciesAsync(); 
    }
}
