using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Abstractions.Repositories.Base;

public interface IUnitOfWork : IDisposable, ITransientService
{    
    IRepository<Role, int> Role { get; }    
    IRepository<User, int> User { get; }    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
