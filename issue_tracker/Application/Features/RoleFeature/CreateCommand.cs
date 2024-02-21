using Application.Abstractions.Services;
using Domain.Entities;
using Domain.Enums;

namespace Application.Features.RoleFeature;

public class CreateCommand : IQuery<IApiResult>
{    
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public int RoleType { get; set; }
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

            var role = new Role
            {
               RoleName = request.Name,
               RoleType = request.RoleType,
               Description = request.Description
            };

            await _unitOfWork.Role.AddAsync(role);
            await _unitOfWork.SaveChangesAsync();

            return ApiResult.Success("Role created successfully.");
        }
    }
}
