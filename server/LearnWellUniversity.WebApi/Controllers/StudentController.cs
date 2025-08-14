using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Models.Common;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.WebApi.Controllers.Bases;
using Microsoft.AspNetCore.Mvc;

namespace LearnWellUniversity.WebApi.Controllers
{
    public class StudentController(IStudentService studentService) : 
        CrudController<StudentDto, int, StudentCreateRequest, StudentUpdateRequest>(studentService)
    {


        [HttpGet("ClassesFriends/{studentId}")]
        public async Task<ApiResponse<List<StudentClassessFriendListDto>>> GetClassesFriendsAsync([FromRoute] int studentId)
        {
            var result = await studentService.GetClassesFriendsAsync(studentId);

            return new ApiResponse<List<StudentClassessFriendListDto>>(result);
        }

    }
}
