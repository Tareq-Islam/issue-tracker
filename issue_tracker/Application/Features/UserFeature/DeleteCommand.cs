using Application.Abstractions.Services;

namespace Application.Features.UserFeature
{
    public class DeleteCommand : ICommand<IApiResult>
    {
        public int UserId { get; set; }      

        internal class DeleteCustomerUserCommandHandler : IRequestHandler<DeleteCommand, IApiResult>
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly IUnitOfWork _unitOfWork;        
            public DeleteCustomerUserCommandHandler(ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
            {
                _currentUserService = currentUserService;
                _unitOfWork = unitOfWork;             
            }

            public async Task<IApiResult> Handle(DeleteCommand request, CancellationToken cancellationToken)
            {
                var user = await _unitOfWork.User.FindAsync(request.UserId, cancellationToken);
                if (user is null)
                    return ApiResult.Fail("User has not been found");


                _unitOfWork.User.SoftDelete(user);
                await _unitOfWork.SaveChangesAsync();

                return ApiResult.Success("User has been deleted");
            }
        }
    }
}
