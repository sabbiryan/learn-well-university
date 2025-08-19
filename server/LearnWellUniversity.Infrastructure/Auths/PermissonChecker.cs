using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Domain.Entities.Auths;

namespace LearnWellUniversity.Infrastructure.Auths
{
    public class PermissonChecker: IPermissionChecker
    {
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;

        public PermissonChecker(IUserContext userContext, IUnitOfWork unitOfWork)
        {
            _userContext = userContext;
            _unitOfWork = unitOfWork;
        }


        public async Task<bool> HasPermissionAsync(string permissionCode)
        {
            var userRoles = await _unitOfWork.Repository<UserRole>().FilterAsync(ur => ur.UserId == _userContext.GetTypedFromValue<int>(_userContext.UserId));

            var rolesIds = userRoles.Select(x=> x.RoleId).ToList();

            var permissions = await _unitOfWork.Repository<RoleResource>()
                .FilterAsync(rr => rolesIds.Contains(rr.RoleId) && rr.Resource.Name == permissionCode, rr => rr.Resource);

            return permissions.Any(rr => rr.Resource.Name.Equals(permissionCode));
        }
    }
}
