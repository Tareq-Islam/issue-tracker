
using Microsoft.EntityFrameworkCore;

namespace Application.Features.RoleFeature;

public class GetRoleListQuery : IQuery<IApiResult>
{
    internal class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, IApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetRoleListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IApiResult> Handle(GetRoleListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.Role.Queryable.ToListAsync();

            return ApiResult<dynamic>.Success(roles);
        }
    }
}
