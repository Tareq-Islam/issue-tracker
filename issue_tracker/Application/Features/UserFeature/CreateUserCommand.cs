using Application.Abstractions.Services;
using Domain.Entities;

namespace Application.Features.UserFeature;

public class CreateUserCommand : IQuery<IApiResult>
{
    public string UserName { get; set; } = string.Empty;
    public string UserEmail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string MobileNumber { get; set; } = string.Empty;
    public int RoleId { get; set; }
    public int? VendorId { get; set; }
    public string LoginName { get; set; } = string.Empty;
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtService _jwtService;
        public CreateUserCommandHandler(IUnitOfWork unitOfWork, IJwtService jwtService)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        public async Task<IApiResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var isLoginNameExist = await _unitOfWork.User.IsExsitAsync(x => x.LoginName.ToLower() == request.LoginName.ToLower().Trim());
                if (isLoginNameExist) return ApiResult.Fail("Login name is already exist.");

            }
            catch (Exception ex)
            {

                throw;
            }
          
            // Generate Password Hash & Salt
            _jwtService.CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                UserName = request.UserName,
                UserEmail = request.UserEmail.Trim(),
                LoginName = request.LoginName.Trim(),
                UserMobileNumber = request.MobileNumber,
                VendorId = request.VendorId,               
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = request.RoleId
            };

            await _unitOfWork.User.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return ApiResult.Success("User created successfully.");
        }
    }
}
