using LearnWellUniversity.Application.Contracts.Bases;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Contracts
{
    public interface IStudentClassEntrollmentService: IApplicationService
    {
        Task<StudentClassDto> EnrollAsync(StudentClassRequest request);
        Task<StudentClassDto> UnenrollAsync(StudentClassRequest request);
        Task<List<StudentClassDto>> GetEnrolledClassesAsync(int studentId);
        Task<List<StudentClassDto>> GetEnrolledStudentsAsync(int classId);
    }
}
