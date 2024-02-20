using FluentValidation;

namespace Application.Features.SiteFeature
{
    public class UpdateCommand : ICommand<IApiResult>
    {
        public int Id { get; set; }
        public string SiteName { get; set; }
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
                var item = await _unitOfWork.Site.FindAsync(request.Id, cancellationToken);

                if (item is null)
                    return ApiResult.Fail("Item has not been found");

             
                item.SiteName = request.SiteName;
                item.Description = request.Description;
                _unitOfWork.Site.Update(item);
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
