
using Microsoft.EntityFrameworkCore;

namespace Application.Features.CauseFindingsFeature;

public class GetListQuery : IQuery<IApiResult>
{
    internal class GetListQueryHandler : IRequestHandler<GetListQuery, IApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IApiResult> Handle(GetListQuery request, CancellationToken cancellationToken)
        {
            var roles = await _unitOfWork.CauseFinding.Queryable          
                .Select(x => new
                {
                    x.Id,
                    x.Name,
                    x.Description
                }).ToListAsync();

            return ApiResult<dynamic>.Success(roles);
        }
    }
}
