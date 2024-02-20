using Application.Abstractions.Services;
using Domain.Entities;

namespace Application.Features.CategoryFeature;

public class CreateCommand : IQuery<IApiResult>
{ 
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    internal class CreateCommandHandler : IRequestHandler<CreateCommand, IApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CreateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IApiResult> Handle(CreateCommand request, CancellationToken cancellationToken)
        {           

            var item = new Category
            {
               Name = request.Name,
               Description = request.Description
            };

            await _unitOfWork.Category.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return ApiResult.Success("Item created successfully.");
        }
    }
}
