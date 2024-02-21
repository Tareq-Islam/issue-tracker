
using Application.Abstractions.Services;
using Domain.Entities;
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

                var user = _unitOfWork.User.Queryable.Where(x => x.IsDeleted == 0).Select(x => new {
                    x.Id,
                    x.Role.RoleName,
                    x.Role.RoleType,
                    VendorName =  x.Vendor.Name,
                    x.VendorId,
                    x.UserName,
                    x.RoleId,
                    x.LoginName,
                    x.UserEmail,
                    x.UserMobileNumber,
                }).AsQueryable();      

                var data = await user.ToListAsync();

                return ApiResult<dynamic>.Success(data);
            }
        }
    }
}
