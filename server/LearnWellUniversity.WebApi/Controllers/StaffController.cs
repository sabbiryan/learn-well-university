using LearnWellUniversity.Application.Common;
using LearnWellUniversity.Application.Common.Paginations;
using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Dtos;
using LearnWellUniversity.Application.Requestes;
using LearnWellUniversity.WebApi.Controllers.Bases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnWellUniversity.WebApi.Controllers
{
    public class StaffController(IStaffService staffService) : CrudController<StaffDto, int, StaffCreateRequest, StaffUpdateRequest> (staffService)
    {       

    }


    
}
