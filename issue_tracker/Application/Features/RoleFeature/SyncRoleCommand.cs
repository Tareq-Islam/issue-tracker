
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace Application.Features.RoleFeature;

public class SyncRoleCommand : ICommand<IApiResult>
{
    internal class SyncRoleCommandHandler : IRequestHandler<SyncRoleCommand, IApiResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        public SyncRoleCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IApiResult> Handle(SyncRoleCommand request, CancellationToken cancellationToken)
        {
            List<RoleDto> roleList = Enum.GetValues(typeof(RoleEnum))
            .Cast<RoleEnum>()
            .Select(role =>
            {
                var field = typeof(RoleEnum).GetField(role.ToString());
                var descriptionAttribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
                string description = descriptionAttribute != null ? descriptionAttribute.Description : role.ToString();

                return new RoleDto
                {
                    Id = (int)role,
                    Name = role.ToString().Replace("_"," "),
                    Description = description
                };
            })
            .ToList();

            var existingRole = await _unitOfWork.Role.Queryable.ToListAsync();

            var appDeclareRoleIds = roleList.Select(x => x.Id).ToList();
            var existingDbRoleIds = existingRole.Select(x => x.Id).ToList();

            var newDeclareRoleId = appDeclareRoleIds.Except(existingDbRoleIds).ToList();

            var newRoles = roleList.Where(x => newDeclareRoleId.Any(n => n == x.Id)).ToList();
            var updateExistingRoles = roleList.Where(x => existingDbRoleIds.Any(n => n == x.Id)).ToList();

            var message = "Sync role successfully. There is no new role.";
            if (newRoles.Any())
            {
                var newInsertableRole = new List<Role>();
                foreach (var item in newRoles)
                {
                    newInsertableRole.Add(new Role()
                    {
                        Id = item.Id,
                        RoleName = item.Name,
                        Description = item.Description
                    });
                }
                await _unitOfWork.Role.AddRangeAsync(newInsertableRole);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                message = $@"Sync role successfully. {newRoles.Count} new role found.";
            }

            if (updateExistingRoles.Any())
            {
                var updateableRole = new List<Role>();
                foreach (var item in updateExistingRoles)
                {
                    var exitingItem = existingRole.Where( x => x.Id == item.Id).FirstOrDefault();
                    exitingItem.RoleName = item.Name;
                    exitingItem.Description = item.Description;
                    updateableRole.Add(exitingItem);
                }
                _unitOfWork.Role.UpdateRange(updateableRole);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return ApiResult.Success(message);
        }
    }

    internal class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
