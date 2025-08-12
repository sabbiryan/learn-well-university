using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.WebApi.Controllers.Bases;

namespace LearnWellUniversity.WebApi.Controllers
{
    public class StaffController(IStaffService staffService) : CrudController<StaffDto, int, StaffCreateRequest, StaffUpdateRequest> (staffService)
    {       

    }


    
}
