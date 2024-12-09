using TBC.PersonReference.Core.Interfaces;

namespace TBC.PersonReference.Application
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        Task CommitAsync();
    }
}
