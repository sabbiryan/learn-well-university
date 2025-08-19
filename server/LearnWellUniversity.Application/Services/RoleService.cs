using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Models.Dtos.Auths;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.Application.Services.Bases;
using LearnWellUniversity.Domain.Entities.Auths;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace LearnWellUniversity.Application.Services
{
    public class RoleService : ApplicationCrudService<Role, RoleDto, int, RoleCreateRequest, RoleUpdateRequest>, IRoleService
    {
        private readonly ILogger<RoleService> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RoleService> logger) : base(unitOfWork, mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public override async Task<int> AddAsync(RoleCreateRequest request)
        {
			try
			{
                var role = _mapper.Map<Role>(request);

                await _unitOfWork.ExecuteInTransactionAsync(async () =>
                {                    
                    await _unitOfWork.Repository<Role>().AddAsync(role);
                    
                    await _unitOfWork.SaveChangesAsync();

                    await ResourcesAssingToRole(role.Id, request.ResourceIds);

                });

                return role.Id;
            }
			catch (Exception e)
			{
                _logger.LogError(e, "Error occurred while adding role");

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

                await _unitOfWork.Repository<RoleResource>().BulkInsertOrUpdateAsync(roleResources);
            }

        }
    }
}
