using Application.Abstractions.Services;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.IssueFeature;

public class CreateCommand : IQuery<IApiResult>
{    
    public int CategoryId { get; set; }
    public int SiteId { get; set; }
    public int VendorId { get; set; }
    public PriorityStatusEnum Priority { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;
    internal class CreateCommandHandler : IRequestHandler<CreateCommand, IApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        public CreateCommandHandler(IUnitOfWork unitOfWork,  ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<IApiResult> Handle(CreateCommand request, CancellationToken cancellationToken)
        {
            var vendor = await _unitOfWork.Vendor.FindAsync(request.VendorId);            
            if (vendor == null)
            {
                return ApiResult.Fail("Invalid vendor.");
            }

            var category = await _unitOfWork.Category.FindAsync(request.CategoryId);
            if (category == null)
            {
                return ApiResult.Fail("Invalid category.");
            }

            var site = await _unitOfWork.Site.FindAsync(request.SiteId);
            if (site == null)
            {
                return ApiResult.Fail("Invalid site.");
            }

            var issue = new Issue
            {
                CategoryId = request.CategoryId,
                VendorId = request.VendorId,
                SiteId = request.SiteId,
                PriorityStatus = (int)request.Priority,
                Status = (int)IssueStatusEnum.Open,
            };

            var assignees = new List<Assignee>();

            //add creator User
            assignees.Add(new Assignee
            {
                UserId = _currentUserService.UserId,
                Issue = issue,
                UserType = (int)IssueAssigneUserType.IssueCreator
            });

            var vendorAssignee = await _unitOfWork.Vendor.Queryable
                .Where(x => x.Id == request.VendorId)
                .Select(x => x.Users.FirstOrDefault())
                .FirstOrDefaultAsync();

            if (vendorAssignee != null)
            {
                // add Vendor user
                assignees.Add(new Assignee
                {
                    UserId = vendorAssignee.Id,
                    Issue = issue,
                    UserType = (int)IssueAssigneUserType.IssueEditor
                });
            }

            var issueComment = new Comment
            {
                Issue = issue,
                Assignee = assignees.Where(x => x.UserId == _currentUserService.UserId).FirstOrDefault(),
                Subject = request.Subject,
                CommentText = request.Comment
            };

            // Transaction
            using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                await _unitOfWork.Issue.AddAsync(issue);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.Assignees.AddRangeAsync(assignees);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await _unitOfWork.Comment.AddAsync(issueComment);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                await transaction.CommitAsync(cancellationToken);
                return ApiResult.Success($"Issue is successfully created. Issue ID #{issue.Id}");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                return ApiResult.Fail("Issue has not been created");
            }

        }
    }
}
