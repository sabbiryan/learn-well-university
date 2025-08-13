using LearnWellUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Infrastructure.Persistences.EntityConfigurations
{
    public static class BusinessEntitiesConfigurationExtension
    {
        public static void AddBusinessEntitiesConfigurations(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grading>(e =>
            {
                e.HasKey(g => g.Id);
                e.Property(g => g.Id).ValueGeneratedOnAdd();
                e.Property(g => g.Name).IsRequired().HasMaxLength(100);
                e.Property(g => g.Description).HasMaxLength(500);
                e.Property(g => g.MinScore).HasPrecision(3, 2).IsRequired();
                e.Property(g => g.MaxScore).HasPrecision(3, 2).IsRequired();
                e.Property(g => g.GradePoint).HasPrecision(3,2).IsRequired();
                e.Property(g => g.IsActive).IsRequired().HasDefaultValue(true);
            });

            modelBuilder.Entity<Address>(e =>
            {
                e.HasKey(a => a.Id);
                e.Property(a => a.Id).ValueGeneratedOnAdd();
                e.Property(a => a.Street).IsRequired().HasMaxLength(200);
                e.Property(a => a.City).IsRequired().HasMaxLength(100);
                e.Property(a => a.State).IsRequired().HasMaxLength(100);
                e.Property(a => a.ZipCode).IsRequired().HasMaxLength(20);
            });

            modelBuilder.Entity<Department>(e =>
            {
                e.HasKey(d => d.Id);
                e.Property(d => d.Id).ValueGeneratedOnAdd();
                e.HasIndex(d => d.Code).IsUnique();
                e.Property(d => d.Name).IsRequired().HasMaxLength(100);
                e.Property(d => d.Description).HasMaxLength(500);
                e.HasOne(d => d.ParentDepartment)
                    .WithMany()
                    .HasForeignKey(d => d.ParentDepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


            var timeOnlyConverter = new ValueConverter<TimeOnly, TimeSpan>(t => t.ToTimeSpan(), ts => TimeOnly.FromTimeSpan(ts));

            modelBuilder.Entity<Schedule>(e =>
            {
                e.HasKey(s => s.Id);
                e.Property(s => s.Id).ValueGeneratedOnAdd();
                e.Property(s => s.Day).HasConversion<string>().IsRequired();
                e.Property(s => s.StartTime).HasConversion(timeOnlyConverter).IsRequired();
                e.Property(s => s.EndTime).HasConversion(timeOnlyConverter).IsRequired();
            });

            

            modelBuilder.Entity<Class>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Id).ValueGeneratedOnAdd();
                e.HasIndex(c => c.Code).IsUnique();
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);
                e.Property(c => c.Description).HasMaxLength(500);
            });


            modelBuilder.Entity<ClassSchedule>(e =>
            {
                e.HasKey(cs => new { cs.ClassId, cs.ScheduleId });
                
                e.HasOne(cs => cs.Class)
                    .WithMany(c => c.ClassSchedules)
                    .HasForeignKey(cs => cs.ClassId);

                e.HasOne(cs => cs.Schedule)
                    .WithMany(s => s.ClassSchedules)
                    .HasForeignKey(cs => cs.ScheduleId);
            });

            modelBuilder.Entity<Course>(e =>
            {
                e.HasKey(c => c.Id);
                e.Property(c => c.Id).ValueGeneratedOnAdd();
                e.HasIndex(c => c.Code).IsUnique();
                e.Property(c => c.Name).IsRequired().HasMaxLength(100);
                e.Property(c => c.Description).HasMaxLength(500);
                e.Property(c => c.CreditHour).HasPrecision(3, 2).IsRequired().HasDefaultValue(0);
            });

            modelBuilder.Entity<CourseClass>(e =>
            {
                e.HasKey(cc => new { cc.CourseId, cc.ClassId });
                
                e.HasOne(cc => cc.Course)
                    .WithMany(c => c.CourseClasses)
                    .HasForeignKey(cc => cc.CourseId);

                e.HasOne(cc => cc.Class)
                    .WithMany(c => c.CourseClasses)
                    .HasForeignKey(cc => cc.ClassId);

                e.HasOne(cc => cc.EnrollmentStaff)
                    .WithMany(s => s.CourseClasses)
                    .HasForeignKey(cc => cc.EnrollmentStaffId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


           
            modelBuilder.Entity<StudentCourse>(e =>
            {
                e.HasKey(sc => new { sc.StudentId, sc.CourseId });
                e.Property(sc => sc.Score).HasPrecision(3, 2).IsRequired(false);

                e.HasOne(g => g.Grading)
                    .WithMany()
                    .HasForeignKey(sc => sc.GradingId)
                    .IsRequired(false);

                e.HasOne(sc => sc.Student)
                    .WithMany(s => s.StudentCourses)
                    .HasForeignKey(sc => sc.StudentId);

                e.HasOne(sc => sc.Course)
                    .WithMany(c => c.StudentCourses)
                    .HasForeignKey(sc => sc.CourseId);

                e.HasOne(sc => sc.EnrollmentStaff)
                    .WithMany(s => s.StudentCourses)
                    .HasForeignKey(sc => sc.EnrollmentStaffId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<StudentClass>(e =>
            {
                e.HasKey(sc => new { sc.StudentId, sc.ClassId });

                e.HasOne(sc => sc.Student)
                    .WithMany(s => s.StudentClasses)
                    .HasForeignKey(sc => sc.StudentId);

                e.HasOne(sc => sc.Class)
                    .WithMany(c => c.StudentClasses)
                    .HasForeignKey(sc => sc.ClassId);

                e.HasOne(sc => sc.EnrollmentStaff)
                    .WithMany(s => s.StudentClasses)
                    .HasForeignKey(sc => sc.EnrollmentStaffId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
