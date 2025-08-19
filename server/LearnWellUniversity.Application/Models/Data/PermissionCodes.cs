using LearnWellUniversity.Application.Extensions;

namespace LearnWellUniversity.Application.Models.Data
{
    

    public static class PermissionCodes
    {
        public static class Auth
        {
            public const string Register = $"{nameof(Auth)}.{nameof(Register)}";
            public const string ChangePassword = $"{nameof(Auth)}.{nameof(ChangePassword)}";
            public const string Revoke = $"{nameof(Auth)}.{nameof(Revoke)}";
        }

        public static class Class
        {
            public const string Create = $"{nameof(Class)}.{nameof(Create)}";
            public const string Read = $"{nameof(Class)}.{nameof(Read)}";
            public const string Update = $"{nameof(Class)}.{nameof(Update)}";
            public const string Delete = $"{nameof(Class)}.{nameof(Delete)}";
        }

        public static class Course
        {
            public const string Create = $"{nameof(Course)}.{nameof(Create)}";
            public const string Read = $"{nameof(Course)}.{nameof(Read)}";
            public const string Update = $"{nameof(Course)}.{nameof(Update)}";
            public const string Delete = $"{nameof(Course)}.{nameof(Delete)}";
        }

        public static class Role
        {
            public const string Create = $"{nameof(Role)}.{nameof(Create)}";
            public const string Read = $"{nameof(Role)}.{nameof(Read)}";
            public const string Update = $"{nameof(Role)}.{nameof(Update)}";
            public const string Delete = $"{nameof(Role)}.{nameof(Delete)}";
        }

        public static class Staff
        {
            public const string Create = $"{nameof(Staff)}.{nameof(Create)}";
            public const string Read = $"{nameof(Staff)}.{nameof(Read)}";
            public const string Update = $"{nameof(Staff)}.{nameof(Update)}";
            public const string Delete = $"{nameof(Staff)}.{nameof(Delete)}";
        }

        public static class Student
        {
            public const string Create = $"{nameof(Student)}.{nameof(Create)}";
            public const string Read = $"{nameof(Student)}.{nameof(Read)}";
            public const string Update = $"{nameof(Student)}.{nameof(Update)}";
            public const string Delete = $"{nameof(Student)}.{nameof(Delete)}";
            public const string ClassesFriends = $"{nameof(Student)}.{nameof(ClassesFriends)}";
        }

        public static class User
        {
            public const string Create = $"{nameof(User)}.{nameof(Create)}";
            public const string Read = $"{nameof(User)}.{nameof(Read)}";
            public const string Update = $"{nameof(User)}.{nameof(Update)}";
            public const string Delete = $"{nameof(User)}.{nameof(Delete)}";
        }


        public static class Enrollment
        {
            public static class StudentClass
            {
                public const string Enroll = $"{nameof(Enrollment)}.{nameof(StudentClass)}.{nameof(Enroll)}";
                public const string Unenroll = $"{nameof(Enrollment)}.{nameof(StudentClass)}.{nameof(Unenroll)}";
                public const string EnrolledClasses = $"{nameof(Enrollment)}.{nameof(StudentClass)}.{nameof(EnrolledClasses)}";
                public const string EnrolledStudents = $"{nameof(Enrollment)}.{nameof(StudentClass)}.{nameof(EnrolledStudents)}";
            }

            public static class StudentCourse
            {
                public const string Enroll = $"{nameof(Enrollment)}.{nameof(StudentCourse)}.{nameof(Enroll)}";
                public const string Unenroll = $"{nameof(Enrollment)}.{nameof(StudentCourse)}.{nameof(Unenroll)}";
                public const string EnrolledCourses = $"{nameof(Enrollment)}.{nameof(StudentCourse)}.{nameof(EnrolledCourses)}";
                public const string EnrolledStudents = $"{nameof(Enrollment)}.{nameof(StudentCourse)}.{nameof(EnrolledStudents)}";
            }
        }


        public static List<string> GetAllPermissionCodes()
        {
            var allPermissions = PermissionHelper.GetAllPermissions();

            return allPermissions;
        }
    }
}
