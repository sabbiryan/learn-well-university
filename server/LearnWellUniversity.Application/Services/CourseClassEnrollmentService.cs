using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Contracts.Auths;
using LearnWellUniversity.Application.Contracts.UoW;
using LearnWellUniversity.Application.Models.Dtos;
using LearnWellUniversity.Application.Models.Requestes;
using LearnWellUniversity.Domain.Entities;
using MapsterMapper;

namespace LearnWellUniversity.Application.Services
{
    public class CourseClassEnrollmentService(IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserContext userContext) : ICourseClassEnrollmentService
    {
        public async Task<CourseClassDto> EnrollAsync(CourseClassRequest request)
        {            
            var courseClass = await unitOfWork.Repository<CourseClass>()
                .FindAsync(cc => cc.CourseId == request.CourseId && cc.ClassId == request.ClassId);

            if (courseClass != null)
            {
                throw new InvalidOperationException("Already enrolled in this class.");
            }

            try
            {                
                courseClass = mapper.Map<CourseClass>(request);
                courseClass.EnrollmentStaffId = userContext.GetTypedFromValue<int?>(userContext.StaffId);

                await unitOfWork.Repository<CourseClass>().AddAsync(courseClass);

                await unitOfWork.SaveChangesAsync();

                return mapper.Map<CourseClassDto>(courseClass);

            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("An error occurred while enrolling in the class.", ex);
            }
        }

        public async Task<CourseClassDto> UnenrollAsync(CourseClassRequest request)
        {
            var courseClass = await unitOfWork.Repository<CourseClass>()
                .FindAsync(cc => cc.CourseId == request.CourseId && cc.ClassId == request.ClassId) ?? throw new InvalidOperationException("Enrollment not found.");
           
            unitOfWork.Repository<CourseClass>().Remove(courseClass);

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<CourseClassDto>(courseClass);
        }

        public async Task<List<CourseClassDto>> GetCourseEnrolledClassesAsync(int courseId)
        {
           var courseClasses = await unitOfWork.Repository<CourseClass>()
                .FilterAsync(cc => cc.CourseId == courseId, x => x.Course, x => x.Class);
            
            var result = mapper.Map<List<CourseClassDto>>(courseClasses);

            return result;
        }

        public async Task<List<CourseClassDto>> GetClassEnrolledCoursesAsync(int classId)
        {
            var courseClasses = await unitOfWork.Repository<CourseClass>()
                .FilterAsync(cc => cc.ClassId == classId, x => x.Course, x => x.Class);

            var result = mapper.Map<List<CourseClassDto>>(courseClasses);

            return result;
        }

       
    }
}
