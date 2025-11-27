using NoteBlock.Domain.Repositories;
using System;
using System.Threading.Tasks;

namespace NoteBlock.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        IReminderRepository ReminderRepository { get; }
        INoteRepository NoteRepository { get; }
        ICommonUserRepository CommonUserRepository { get; }
        IAdminUserRepository AdminUserRepository { get; }

        ICategoryRepository CategoryRepository { get; }

        Task SaveAsync();
    }
}
