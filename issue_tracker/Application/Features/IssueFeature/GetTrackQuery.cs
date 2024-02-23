using Application.Abstractions.Services;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;


namespace Application.Features.IssueFeature
{
    public class GetTrackQuery : IQuery<IApiResult>
    {
        public int Id { get; set; }
        public class GetTrackQueryHandler : IRequestHandler<GetTrackQuery, IApiResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;

            public GetTrackQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
            {
                _unitOfWork = unitOfWork;
                _currentUserService = currentUserService;
            }

            public async Task<IApiResult> Handle(GetTrackQuery request, CancellationToken cancellationToken)
            {

                var item = _unitOfWork.Issue.Queryable.Where(x => x.IsDeleted == 0 && x.Id == request.Id).Select(x => new
                {
                    x.Id,
                    CategoryName = x.Category.Name,
                    x.CategoryId,
                    x.VendorId,
                    VendorName = x.Vendor.Name,
                    x.SiteId,
                    x.Site.SiteName,
                    x.PriorityStatus,
                    x.Status,
                    Assignees = x.Assignees.Select(y => new
                    {
                        y.Id,
                        y.UserId,
                        y.User.UserName,
                        assigneeType = y.UserType
                    }).ToList(),
                    Comments = x.Comments.Select(y => new
                    {
                        y.Assignee.User.UserName,
                        y.Subject,
                        y.AssigneeId,
                        y.CommentText,
                        y.CreationTime
                    }).ToList(),
                    causes = x.IssueCauseFindingsMappings.ToList(),
                    solutions = x.IssueSolutionTagMappings.ToList(),
                }).AsQueryable();

                var data = await item.FirstOrDefault<dynamic>();

                return ApiResult<dynamic>.Success(data);
            }
        }
    }

}
