using Application.Abstractions.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Application.Features.AuthFeature;

public class GetAuthPayloadQuery : IQuery<IApiResult>
{
    internal class GetAuthPayloadQueryHandler : IRequestHandler<GetAuthPayloadQuery, IApiResult>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUnitOfWork _unitOfWork;

        public GetAuthPayloadQueryHandler(ICurrentUserService currentUserService, IUnitOfWork unitOfWork)
        {
            _currentUserService = currentUserService;
            _unitOfWork = unitOfWork;
        }

        public async Task<IApiResult> Handle(GetAuthPayloadQuery request, CancellationToken cancellationToken)
        {
            var currentUser = await _unitOfWork.User.Queryable
                .Where(x => x.Id == _currentUserService.UserId).Select(x => new
                {
                    x.UserName,
                    x.RoleId,
                    x.Id,
                    x.Role.RoleName,
                    x.Role.RoleType
                })
                .FirstOrDefaultAsync();

            bool isInvalidUser = currentUser is null;
            if (isInvalidUser)
            {
                return ApiResult.Unauthorized();
            }

            var response = new
            {
                token = string.Empty,
                Payload = new
                {
                    rid = currentUser.RoleId,
                    currentUser.UserName,
                    uid = currentUser.Id,
                    currentUser.RoleName,
                    currentUser.RoleType
                }
            };
            return ApiResult<dynamic>.Success(response);
        }
    }
}
