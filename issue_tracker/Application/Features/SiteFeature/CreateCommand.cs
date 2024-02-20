using Application.Abstractions.Services;
using Domain.Entities;

namespace Application.Features.SiteFeature;

public class CreateCommand : IQuery<IApiResult>
{ 
    public int? Id { get; set; }
    public string SiteName { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    internal class CreateCommandHandler : IRequestHandler<CreateCommand, IApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        public CreateCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<IApiResult> Handle(CreateCommand request, CancellationToken cancellationToken)
        {

            var item = new Site
            {

                SiteName = request.SiteName,
                Description = request.Description
            };

            await _unitOfWork.Site.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return ApiResult.Success("Item created successfully.");
        }
    }
}
