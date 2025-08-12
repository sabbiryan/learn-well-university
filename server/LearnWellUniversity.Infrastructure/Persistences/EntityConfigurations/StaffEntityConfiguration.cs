using LearnWellUniversity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Infrastructure.Persistences.EntityConfigurations
{
    public class StaffEntityConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Id).ValueGeneratedOnAdd();
            builder.HasIndex(s => s.Code).IsUnique();
            builder.Property(s => s.FirstName).HasMaxLength(100);
            builder.Property(s => s.LastName).HasMaxLength(100);
            builder.Property(s => s.FullName).HasComputedColumnSql("first_name || ' ' || last_name", stored: true);
            builder.Property(s => s.Email).IsRequired().HasMaxLength(100);

            builder.HasOne(e => e.PresentAddress)
                .WithMany()
                .HasForeignKey(s => s.PresentAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.PermanentAddress)
                .WithMany()
                .HasForeignKey(s => s.PermanentAddressId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.Department)
                .WithMany(d => d.Staffs)
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(s => s.User)
                .WithMany(u => u.Staffs)
                .HasForeignKey(s => s.UserId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
