using FluentValidation;

namespace Application.Features.SolutionTagFeature
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
                var item = await _unitOfWork.SolutionTag.FindAsync(request.Id, cancellationToken);

                if (item is null)
                    return ApiResult.Fail("Item has not been found");

             
                item.Name = request.Name;
                item.Description = request.Description;
                _unitOfWork.SolutionTag.Update(item);
                await _unitOfWork.SaveChangesAsync();

                return ApiResult.Success("Item has been successfully updated");
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
