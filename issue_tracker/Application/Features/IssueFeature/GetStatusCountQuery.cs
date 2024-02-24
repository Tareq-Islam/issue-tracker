using Application.Abstractions.Services;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.IssueFeature
{
    public class GetIssueStatusCountQuery : IQuery<IApiResult>
    {
        internal class GetIssueStatusCountQueryHandler : IRequestHandler<GetIssueStatusCountQuery, IApiResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            public GetIssueStatusCountQueryHandler(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            public async Task<IApiResult> Handle(GetIssueStatusCountQuery request, CancellationToken cancellationToken)
            {
                var items = _unitOfWork.Issue.Queryable.Where(x => x.IsDeleted == 0).AsQueryable();
                var data = await items.ToListAsync();
                int open = data.Where(x => x.Status == (int)IssueStatusEnum.Open).Count();
                int closed = data.Where(x => x.Status == (int)IssueStatusEnum.Close).Count();

                return ApiResult<dynamic>.Success(new { open, closed });
            }
        }
    }   
}
