using Application.Abstractions.Services;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.IssueFeature
{
    public class GetIssueStatusCountQuery : IQuery<IApiResult>
    {
        internal class GetIssueStatusCountQueryHandler : IRequestHandler<GetIssueStatusCountQuery, IApiResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUser;
            public GetIssueStatusCountQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUser)
            {
                _unitOfWork = unitOfWork;
                _currentUser = currentUser;
            }

            public async Task<IApiResult> Handle(GetIssueStatusCountQuery request, CancellationToken cancellationToken)
            {
                var items = _unitOfWork.Issue.Queryable.Where(x => x.IsDeleted == 0).AsQueryable();
                var data = await items.ToListAsync();
                int open = data.Where(x => x.Status == 1).Count();
                int closed = data.Where(x => x.Status == 2).Count();
                int suspend = data.Where(x => x.Status == 3).Count();

                return ApiResult<dynamic>.Success(new { open, closed, suspend });
            }
        }
    }   
}
