using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.Domain.Entities;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Services
{
    public class StudentClassEnrollmentService(IUnitOfWork unitOfWork, 
        IMapper mapper) : IStudentClassEntrollmentService
    {
        public async Task<StudentClassDto> EnrollAsync(StudentClassRequest request)
        {
            var studentClass = await unitOfWork.Repository<StudentClass>()
                .FindAsync(sc => sc.StudentId == request.StudentId && sc.ClassId == request.ClassId);

            if (studentClass != null)
            {
                throw new InvalidOperationException("Already enrolled in this class.");
            }

            studentClass = mapper.Map<StudentClass>(request);

            await unitOfWork.Repository<StudentClass>().AddAsync(studentClass);

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<StudentClassDto>(studentClass);
        }

        public async Task<StudentClassDto> UnenrollAsync(StudentClassRequest request)
        {
            var existingEnrollment = await unitOfWork.Repository<StudentClass>()
                .FindAsync(sc => sc.StudentId == request.StudentId && sc.ClassId == request.ClassId);

            if (existingEnrollment == null)
            {
                throw new InvalidOperationException("Enrollment not found.");
            }

            unitOfWork.Repository<StudentClass>().Remove(existingEnrollment);

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<StudentClassDto>(existingEnrollment);
        }

        public async Task<List<StudentClassDto>> GetEnrolledClassesAsync(int studentId)
        {
            var studentClasses = await unitOfWork.Repository<StudentClass>()
                .FilterAsync(sc => sc.StudentId == studentId, x=> x.Student, x=> x.Class);

            var result = mapper.Map<List<StudentClassDto>>(studentClasses);

            return result;
        }


        public async Task<List<StudentClassDto>> GetEnrolledStudentsAsync(int classId)
        {
            var studentClasses = await unitOfWork.Repository<StudentClass>()
                .FilterAsync(sc => sc.ClassId == classId, x => x.Student, x => x.Class);

            var result = mapper.Map<List<StudentClassDto>>(studentClasses);

            return result;
        }

      
    }
}
