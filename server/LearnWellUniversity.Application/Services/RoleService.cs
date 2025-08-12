using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Models.Dtos.Auths;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.Domain.Entities.Auths;
using MapsterMapper;
using Microsoft.VisualBasic;

namespace LearnWellUniversity.Application.Services
{
    public class RoleService(IUnitOfWork unitOfWork, IMapper mapper) : ApplicationCrudService<Role, RoleDto, int, RoleCreateRequest, RoleUpdateRequest>(unitOfWork, mapper), IRoleService
    {
        public override async Task<int> AddAsync(RoleCreateRequest request)
        {
			try
			{
                await unitOfWork.BeginTransactionAsync();

                var roleId =  await base.AddAsync(request);

                await ResourcesAssingToRole(roleId, request.ResourceIds);

                await unitOfWork.CommitTransactionAsync();

                return roleId;
            }
			catch (Exception e)
			{
                await unitOfWork.RollbackTransactionAsync();

                throw;
			}
        }


        private async Task ResourcesAssingToRole(int roleId, int[]? resourceIds)
        {

            if (resourceIds != null && resourceIds.Any())
            {
                var roleResources = resourceIds.Select(resourceId => new RoleResource
                {
                    RoleId = roleId,
                    ResourceId = resourceId
                }).ToList();

                await unitOfWork.Repository<RoleResource>().BulkInsertOrUpdateAsync(roleResources);
            }

        }
    }
}
