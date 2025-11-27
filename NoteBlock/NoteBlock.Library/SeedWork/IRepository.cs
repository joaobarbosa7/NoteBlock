using System.Collections.Generic;
using System.Threading.Tasks;

namespace NoteBlock.Domain.SeedWork
{
    public interface IRepository<T> where T : Entity
    {
        void Create(T e);
        void Update(T e);
        void Delete(T e);
        Task<T> FindByIdAsync(int id);
        Task<List<T>> FindAllAsync();
    }
}
