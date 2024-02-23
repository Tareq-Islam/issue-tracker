using Application.Abstractions.Services;
using Domain.Entities;

namespace Application.Features.IssueFeature;

public class CreateCommand : IQuery<IApiResult>
{    
    public int CategoryId { get; set; }
    public int SiteId { get; set; }
    public int VendorId { get; set; }
    public int Status { get; set; }
    public int Priority { get; set; }
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
            using var transaction = await _unitOfWork.BeginTransactionAsync(cancellationToken);
            try
            {
                var vendor = _unitOfWork.Vendor.Queryable.Where(x => x.Id == request.VendorId).FirstOrDefault();

                var item = new Issue
                {
                    CategoryId = request.CategoryId,
                    SiteId = request.SiteId,
                    VendorId = request.VendorId,
                    Status = request.Status,
                    PriorityStatus = request.Priority,
                };

                await _unitOfWork.Issue.AddAsync(item);
                await _unitOfWork.SaveChangesAsync();

                var comments = new Comment
                {
                    Comment1 = request.Comment,
                    Subject = request.Subject,
                };

                var assignee = new Assignee
                {
                    UserId = _currentUserService.UserId,
                    UserType = 1,
                };

                var assignee2 = new Assignee
                {
                    UserId = vendor.Users.FirstOrDefault().Id,
                    UserType = 2,
                };

                await transaction.CommitAsync(cancellationToken);

                return ApiResult.Success($"Issue is successfully created. Issue ID #{item.Id}");
            }
            catch (Exception)
            {
                await transaction.RollbackAsync(cancellationToken);
                return ApiResult.Fail("Issue has not been created");
            }
           
        }
    }
}
