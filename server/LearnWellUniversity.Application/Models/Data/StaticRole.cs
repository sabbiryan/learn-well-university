using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Models.Statics
{
    public record StaticRole(string Name, string DisplayName, bool IsStatic = false)
    {
        public static StaticRole Host { get;} = new StaticRole("Host", "Host", true);
        public static StaticRole Admin { get; } = new StaticRole("Admin", "Administrator", true);
        public static StaticRole Staff { get; } = new StaticRole("Staff", "Staff", true);
        public static StaticRole Teacher { get; } = new StaticRole("Teacher", "Teacher", true);
        public static StaticRole Student { get; } = new StaticRole("Guest", "Guest User", true);
        public static StaticRole Guest { get; } = new StaticRole("Guest", "Guest User");

        public static StaticRole[] AllRoles { get; } =
        [
            Host,
            Admin,
            Staff,
            Teacher,
            Student,
            Guest
        ];
    }

}
