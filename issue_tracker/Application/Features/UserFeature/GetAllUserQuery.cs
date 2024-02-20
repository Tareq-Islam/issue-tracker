
using Application.Abstractions.Services;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.UserFeature
{
    public class GetAllUserQuery : IQuery<IApiResult>
    {
        internal class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, IApiResult>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly ICurrentUserService _currentUserService;          

            public GetAllUserQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
            {
                _unitOfWork = unitOfWork;
                _currentUserService = currentUserService;             
            }

            public async Task<IApiResult> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
            {               

                var user = _unitOfWork.User.Queryable.AsQueryable();      

                var data = await user.ToListAsync();

                return ApiResult<dynamic>.Success(data);
            }
        }
    }
}
