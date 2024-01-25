using Application.Abstractions.Repositories.Base;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositries.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IssueTrackerApplicationDbContext _dbContext;
        public IRepository<User, int> User { get; }        
        public IRepository<Role, int> Role { get; }                
        public UnitOfWork(IssueTrackerApplicationDbContext dbContext, IRepository<User, int> user, IRepository<Role, int> role)
        {
            _dbContext = dbContext;
            User = user;
            Role = role;            
        }

        private bool disposed;

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            disposed = true;
        }
    }
}
