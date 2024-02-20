using Application.Abstractions.Repositories.Base;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositries.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IssueTrackerApplicationDbContext _dbContext;
        public IRepository<User, int> User { get; }        
        public IRepository<Role, int> Role { get; }
        public IRepository<Category, int> Category { get; }
        public IRepository<CauseFinding, int> CauseFinding { get; }
        public IRepository<Site, int> Site { get; }
        public IRepository<Assignee, int> Assignees { get;  }
        public IRepository<Comment, int> Comment { get; }
        public IRepository<Issue, int> Issue { get;  }
        public IRepository<IssueSolutionTagMapping, int> IssueSolutionTagMapping { get; }
        public IRepository<SolutionTag, int> SolutionTag { get;  }
        public IRepository<Vendor, int> Vendor { get;  }

        public UnitOfWork(IssueTrackerApplicationDbContext dbContext, IRepository<User, int> user, IRepository<Role, int> role, IRepository<Category, int> category, IRepository<CauseFinding, int> causeFinding, IRepository<Site, int> site, IRepository<Assignee, int> assignees, IRepository<Comment, int> comment, IRepository<Issue, int> issue, IRepository<IssueSolutionTagMapping, int> issueSolutionTagMapping, IRepository<SolutionTag, int> solutionTag, IRepository<Vendor, int> vendor)
        {
            _dbContext = dbContext;
            User = user;
            Role = role;
            Category = category;
            CauseFinding = causeFinding;
            Site = site;
            Assignees = assignees;
            Comment = comment;
            Issue = issue;
            IssueSolutionTagMapping = issueSolutionTagMapping;
            SolutionTag = solutionTag;
            Vendor = vendor;
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
