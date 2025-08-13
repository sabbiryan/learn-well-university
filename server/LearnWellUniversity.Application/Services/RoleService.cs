using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Models.Dtos.Auths;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.Domain.Entities.Auths;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace LearnWellUniversity.Application.Services
{
    public class RoleService(IUnitOfWork unitOfWork, 
        IMapper mapper,
        ILogger<RoleService> logger) : ApplicationCrudService<Role, RoleDto, int, RoleCreateRequest, RoleUpdateRequest>(unitOfWork, mapper), IRoleService
    {
        public override async Task<int> AddAsync(RoleCreateRequest request)
        {
			try
			{
                var role = mapper.Map<Role>(request);

                await unitOfWork.ExecuteInTransactionAsync(async () =>
                {                    
                    await unitOfWork.Repository<Role>().AddAsync(role);
                    
                    await unitOfWork.SaveChangesAsync();

                    await ResourcesAssingToRole(role.Id, request.ResourceIds);

                });

                return role.Id;
            }
			catch (Exception e)
			{
                logger.LogError(e, "Error occurred while adding role");

                throw;
			}
        }


        private async Task ResourcesAssingToRole(int roleId, int[]? resourceIds)
        {

            if (resourceIds != null && resourceIds.Length != 0)
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
