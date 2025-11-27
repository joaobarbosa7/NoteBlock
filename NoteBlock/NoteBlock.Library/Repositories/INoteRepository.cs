using NoteBlock.Domain.SeedWork;
using NoteBlock.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteBlock.Domain.Repositories
{
    public interface INoteRepository : IRepository<Note>
    {
        Task<Note> FindByTitleAsync(string Title, int UserId);
        Task<Note> FindByTitleStartedWithAsync(string Title, int UserId);
        Task<Note> FindAllFiledByAsync(int UserId);
        Task<Note> FindAllNotFiledAsync(int UserId);
        Task<Note> FindByIdWithDependenciesAsync(int id);
        Task<List<Note>> FindAllWithDependenciesAsync();

    }
}
