using Application.Abstractions.Services;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserFeature
{
    public class UpdateCommand : ICommand<IApiResult>
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public string MobileNumber { get; set; }          

        internal class UpdateCommandHandler : IRequestHandler<UpdateCommand, IApiResult>
        {
            private readonly ICurrentUserService _currentUserService;
            private readonly IUnitOfWork _unitOfWork;          
            private readonly IJwtService _jwtService;        
            public UpdateCommandHandler(ICurrentUserService currentUserService, IUnitOfWork unitOfWork,IJwtService jwtService)
            {
                _currentUserService = currentUserService;
                _unitOfWork = unitOfWork;              
                _jwtService = jwtService;              
            }

            public async Task<IApiResult> Handle(UpdateCommand request, CancellationToken cancellationToken)
            {
                var user = await _unitOfWork.User.FindAsync(request.UserId, cancellationToken);

                if (user is null)
                    return ApiResult.Fail("User has not been found");

                // update user
                user.UserName = request.UserName;
                user.UserEmail = request.UserEmail;
                _unitOfWork.User.Update(user);
                await _unitOfWork.SaveChangesAsync();

                return ApiResult.Success("User has been successfully updated");
            }
        }
    }

    public class UpdateCommandValidator : AbstractValidator<UpdateCommand>
    {
        public UpdateCommandValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .WithMessage("User not found");

            RuleFor(x => x.UserName)
                .MaximumLength(100)
                .NotEmpty();

            RuleFor(x => x.UserEmail)
                .MaximumLength(50)
                .EmailAddress()
                .When(x => !string.IsNullOrEmpty(x.UserEmail));
          
        }
    }
}
