using TBC.PersonReference.Application;
using TBC.PersonReference.Core.Interfaces;
using TBC.PersonReference.Infrastructure.Data.Repositories;

namespace TBC.PersonReference.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IPersonRepository PersonRepository => new PersonRepository(_context);

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
