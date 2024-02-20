using Application.Abstractions.Services;

namespace Application.Features.CauseFindingsFeature
{
    public class DeleteCommand : ICommand<IApiResult>
    {
        public int Id { get; set; }      

        internal class DeleteCustomerUserCommandHandler : IRequestHandler<DeleteCommand, IApiResult>
        {           
            private readonly IUnitOfWork _unitOfWork;        
            public DeleteCustomerUserCommandHandler(IUnitOfWork unitOfWork)
            {              
                _unitOfWork = unitOfWork;             
            }

            public async Task<IApiResult> Handle(DeleteCommand request, CancellationToken cancellationToken)
            {
                var item = await _unitOfWork.CauseFinding.FindAsync(request.Id, cancellationToken);
                if (item is null)
                    return ApiResult.Fail("Item has not been found");


                _unitOfWork.CauseFinding.SoftDelete(item);
                await _unitOfWork.SaveChangesAsync();

                return ApiResult.Success("Item has been deleted");
            }
        }
    }
}
