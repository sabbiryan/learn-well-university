using LearnWellUniversity.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Models.Statics
{
    public record StaticSchedule(Day Day, TimeOnly StartTime, TimeOnly EndTime)
    {
        public static StaticSchedule Saturday9Am { get; } = new StaticSchedule(Day.Saturday, new TimeOnly(9, 0), new TimeOnly(10, 30));
        public static StaticSchedule Saturday10_30Am { get; } = new StaticSchedule(Day.Saturday, new TimeOnly(10, 30), new TimeOnly(12, 0));    
        public static StaticSchedule Saturday12Pm { get; } = new StaticSchedule(Day.Saturday, new TimeOnly(12, 0), new TimeOnly(13, 30));
        public static StaticSchedule Saturday1_30Pm { get; } = new StaticSchedule(Day.Saturday, new TimeOnly(13, 30), new TimeOnly(15, 0));
        public static StaticSchedule Saturday3Pm { get; } = new StaticSchedule(Day.Saturday, new TimeOnly(15, 0), new TimeOnly(16, 30));
        public static StaticSchedule Saturday4_30Pm { get; } = new StaticSchedule(Day.Saturday, new TimeOnly(16, 30), new TimeOnly(18, 0));
        public static StaticSchedule Saturday6Pm { get; } = new StaticSchedule(Day.Saturday, new TimeOnly(18, 0), new TimeOnly(19, 30));

        public static StaticSchedule Sunday9Am { get; } = new StaticSchedule(Day.Sunday, new TimeOnly(9, 0), new TimeOnly(10, 30));
        public static StaticSchedule Sunday10_30Am { get; } = new StaticSchedule(Day.Sunday, new TimeOnly(10, 30), new TimeOnly(12, 0));
        public static StaticSchedule Sunday12Pm { get; } = new StaticSchedule(Day.Sunday, new TimeOnly(12, 0), new TimeOnly(13, 30));
        public static StaticSchedule Sunday1_30Pm { get; } = new StaticSchedule(Day.Sunday, new TimeOnly(13, 30), new TimeOnly(15, 0));
        public static StaticSchedule Sunday3Pm { get; } = new StaticSchedule(Day.Sunday, new TimeOnly(15, 0), new TimeOnly(16, 30));
        public static StaticSchedule Sunday4_30Pm { get; } = new StaticSchedule(Day.Sunday, new TimeOnly(16, 30), new TimeOnly(18, 0));
        public static StaticSchedule Sunday6Pm { get; } = new StaticSchedule(Day.Sunday, new TimeOnly(18, 0), new TimeOnly(19, 30));

        public static StaticSchedule Monday9Am { get; } = new StaticSchedule(Day.Monday, new TimeOnly(9, 0), new TimeOnly(10, 30));
        public static StaticSchedule Monday10_30Am { get; } = new StaticSchedule(Day.Monday, new TimeOnly(10, 30), new TimeOnly(12, 0));
        public static StaticSchedule Monday12Pm { get; } = new StaticSchedule(Day.Monday, new TimeOnly(12, 0), new TimeOnly(13, 30));
        public static StaticSchedule Monday1_30Pm { get; } = new StaticSchedule(Day.Monday, new TimeOnly(13, 30), new TimeOnly(15, 0));
        public static StaticSchedule Monday3Pm { get; } = new StaticSchedule(Day.Monday, new TimeOnly(15, 0), new TimeOnly(16, 30));
        public static StaticSchedule Monday4_30Pm { get; } = new StaticSchedule(Day.Monday, new TimeOnly(16, 30), new TimeOnly(18, 0));
        public static StaticSchedule Monday6Pm { get; } = new StaticSchedule(Day.Monday, new TimeOnly(18, 0), new TimeOnly(19, 30));

        public static StaticSchedule Tuesday9Am { get; } = new StaticSchedule(Day.Tuesday, new TimeOnly(9, 0), new TimeOnly(10, 30));
        public static StaticSchedule Tuesday10_30Am { get; } = new StaticSchedule(Day.Tuesday, new TimeOnly(10, 30), new TimeOnly(12, 0));
        public static StaticSchedule Tuesday12Pm { get; } = new StaticSchedule(Day.Tuesday, new TimeOnly(12, 0), new TimeOnly(13, 30));
        public static StaticSchedule Tuesday1_30Pm { get; } = new StaticSchedule(Day.Tuesday, new TimeOnly(13, 30), new TimeOnly(15, 0));
        public static StaticSchedule Tuesday3Pm { get; } = new StaticSchedule(Day.Tuesday, new TimeOnly(15, 0), new TimeOnly(16, 30));
        public static StaticSchedule Tuesday4_30Pm { get; } = new StaticSchedule(Day.Tuesday, new TimeOnly(16, 30), new TimeOnly(18, 0));
        public static StaticSchedule Tuesday6Pm { get; } = new StaticSchedule(Day.Tuesday, new TimeOnly(18, 0), new TimeOnly(19, 30));

        public static StaticSchedule Wednesday9Am { get; } = new StaticSchedule(Day.Wednesday, new TimeOnly(9, 0), new TimeOnly(10, 30));
        public static StaticSchedule Wednesday10_30Am { get; } = new StaticSchedule(Day.Wednesday, new TimeOnly(10, 30), new TimeOnly(12, 0));
        public static StaticSchedule Wednesday12Pm { get; } = new StaticSchedule(Day.Wednesday, new TimeOnly(12, 0), new TimeOnly(13, 30));
        public static StaticSchedule Wednesday1_30Pm { get; } = new StaticSchedule(Day.Wednesday, new TimeOnly(13, 30), new TimeOnly(15, 0));
        public static StaticSchedule Wednesday3Pm { get; } = new StaticSchedule(Day.Wednesday, new TimeOnly(15, 0), new TimeOnly(16, 30));
        public static StaticSchedule Wednesday4_30Pm { get; } = new StaticSchedule(Day.Wednesday, new TimeOnly(16, 30), new TimeOnly(18, 0));
        public static StaticSchedule Wednesday6Pm { get; } = new StaticSchedule(Day.Wednesday, new TimeOnly(18, 0), new TimeOnly(19, 30));
      
        public static StaticSchedule Thursday9Am { get; } = new StaticSchedule(Day.Thursday, new TimeOnly(9, 0), new TimeOnly(10, 30));
        public static StaticSchedule Thursday10_30Am { get; } = new StaticSchedule(Day.Thursday, new TimeOnly(10, 30), new TimeOnly(12, 0));
        public static StaticSchedule Thursday12Pm { get; } = new StaticSchedule(Day.Thursday, new TimeOnly(12, 0), new TimeOnly(13, 30));
        public static StaticSchedule Thursday1_30Pm { get; } = new StaticSchedule(Day.Thursday, new TimeOnly(13, 30), new TimeOnly(15, 0));
        public static StaticSchedule Thursday3Pm { get; } = new StaticSchedule(Day.Thursday, new TimeOnly(15, 0), new TimeOnly(16, 30));
        public static StaticSchedule Thursday4_30Pm { get; } = new StaticSchedule(Day.Thursday, new TimeOnly(16, 30), new TimeOnly(18, 0));
        public static StaticSchedule Thursday6Pm { get; } = new StaticSchedule(Day.Thursday, new TimeOnly(18, 0), new TimeOnly(19, 30));


        public static StaticSchedule[] AllSchedules { get; } =
            [
            // Saturday
            Saturday9Am,
            Saturday10_30Am,
            Saturday12Pm,
            Saturday1_30Pm,
            Saturday3Pm,
            Saturday4_30Pm,
            Saturday6Pm,
            // Sunday
            Sunday9Am,
            Sunday10_30Am,
            Sunday12Pm,
            Sunday1_30Pm,
            Sunday3Pm,
            Sunday4_30Pm,
            Sunday6Pm,
            // Monday
            Monday9Am,
            Monday10_30Am,
            Monday12Pm,
            Monday1_30Pm,
            Monday3Pm,
            Monday4_30Pm,
            Monday6Pm,
            // Tuesday
            Tuesday9Am,
            Tuesday10_30Am,
            Tuesday12Pm,
            Tuesday1_30Pm,
            Tuesday3Pm,
            Tuesday4_30Pm,
            Tuesday6Pm,
            // Wednesday
            Wednesday9Am,
            Wednesday10_30Am,
            Wednesday12Pm,
            Wednesday1_30Pm,
            Wednesday3Pm,
            Wednesday4_30Pm,
            Wednesday6Pm,
            // Thursday
            Thursday9Am,
            Thursday10_30Am,
            Thursday12Pm,
            Thursday1_30Pm,
            Thursday3Pm,
            Thursday4_30Pm,
            Thursday6Pm
        ];
    }
}
