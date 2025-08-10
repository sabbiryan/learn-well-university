using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Infrastructure.Persistences.Seeds.Models
{
    public record StaticUser(string FirstName, string LastName, string Email, string? Phone, string password)
    {
        public static StaticUser Admin { get; } = new StaticUser("Admin", "User", "admin@user.email", null, "Admin@1!");
        public static StaticUser Staff { get; } = new StaticUser("Staff", "User", "staff@user.email", null, "Staff@1!");
        public static StaticUser Teacher { get; } = new StaticUser("Teacher", "User", "teacher@user.email", null, "Teacher@1!");
        public static StaticUser Student { get; } = new StaticUser("Student", "User", "student@ser.email",null, "Student@1!");

        public static StaticUser[] AllUsers { get; } =
        [
            Admin,
            Staff,
            Teacher,
            Student
        ];
    }
}
