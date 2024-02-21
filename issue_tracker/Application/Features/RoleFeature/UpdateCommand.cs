using FluentValidation;

namespace Application.Features.RoleFeature
{
    public class UpdateCommand : ICommand<IApiResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }     

        internal class UpdateCommandHandler : IRequestHandler<UpdateCommand, IApiResult>
        {       
            private readonly IUnitOfWork _unitOfWork;          
            public UpdateCommandHandler(IUnitOfWork unitOfWork)
            {              
                _unitOfWork = unitOfWork;              
            }

            public async Task<IApiResult> Handle(UpdateCommand request, CancellationToken cancellationToken)
            {
                var item = await _unitOfWork.Role.FindAsync(request.Id, cancellationToken);

                if (item is null)
                    return ApiResult.Fail("Role has not been found");

             
                item.RoleName = request.Name;
                item.Description = request.Description;
                _unitOfWork.Role.Update(item);
                await _unitOfWork.SaveChangesAsync();

                return ApiResult.Success("Role has been successfully updated");
            }
        }
    }

    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Id not found");            
          
        }
    }
}
