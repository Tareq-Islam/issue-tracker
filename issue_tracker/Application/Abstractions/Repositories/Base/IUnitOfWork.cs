using Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Abstractions.Repositories.Base;

public interface IUnitOfWork : IDisposable, ITransientService
{    
    IRepository<Role, int> Role { get; }    
    IRepository<User, int> User { get; }
 IRepository<Category, int> Category { get; }
  IRepository<CauseFinding, int> CauseFinding { get; }
 IRepository<Site, int> Site { get; }
 IRepository<Assignee, int> Assignees { get; }
IRepository<Comment, int> Comment { get; }
 IRepository<Issue, int> Issue { get; }
IRepository<IssueSolutionTagMapping, int> IssueSolutionTagMapping { get; }
IRepository<SolutionTag, int> SolutionTag { get; }
IRepository<Vendor, int> Vendor { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
}
