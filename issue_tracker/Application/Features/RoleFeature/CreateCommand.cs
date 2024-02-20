using Application.Abstractions.Services;
using Domain.Entities;

namespace Application.Features.RoleFeature;

public class CreateCommand : IQuery<IApiResult>
{ 
    public int? Id { get; set; }
    public string RoleName { get; set; } = string.Empty;
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
               RoleName = request.RoleName,
               Id = (int)request.Id
            };

            await _unitOfWork.Role.AddAsync(role);
            await _unitOfWork.SaveChangesAsync();

            return ApiResult.Success("Role created successfully.");
        }
    }
}
