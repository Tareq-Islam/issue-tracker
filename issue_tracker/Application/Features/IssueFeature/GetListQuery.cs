using Application.Abstractions.Services;
using Microsoft.EntityFrameworkCore;


namespace Application.Features.IssueFeature
{
    public class GetListQuery : IQuery<IApiResult>
    {
        internal class GetListQueryHandler : IRequestHandler<GetListQuery, IApiResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;

            public GetListQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
            {
                _unitOfWork = unitOfWork;
                _currentUserService = currentUserService;
            }

            public async Task<IApiResult> Handle(GetListQuery request, CancellationToken cancellationToken)
            {

                var item = _unitOfWork.Issue.Queryable.Where(x => x.IsDeleted == 0).Select(x => new
                {
                    x.Id,
                    CategoryName = x.Category.Name,
                    x.CategoryId,
                    x.VendorId,
                    VendorName = x.Vendor.Name,
                    x.SiteId,
                    x.Site.SiteName,
                    x.PriorityStatus,
                    x.Status
                }).AsQueryable();

                var data = await item.ToListAsync();

                return ApiResult<dynamic>.Success(data);
            }
        }
    }

}
