using Application.Abstractions.Services;
using Domain.Entities;

namespace Application.Features.IssueFeature;

public class CreateCommand : IQuery<IApiResult>
{    
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string Contact { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
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

            var role = new Vendor
            {
               Name = request.Name,
                Address = request.Address,
                Contact = request.Contact,
                Phone = request.Phone
            };

            await _unitOfWork.Vendor.AddAsync(role);
            await _unitOfWork.SaveChangesAsync();

            return ApiResult.Success("Item created successfully.");
        }
    }
}
