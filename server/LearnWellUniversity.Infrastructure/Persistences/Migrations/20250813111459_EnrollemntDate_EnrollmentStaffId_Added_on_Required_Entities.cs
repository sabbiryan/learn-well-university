using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnWellUniversity.Infrastructure.Persistences.Migrations
{
    /// <inheritdoc />
    public partial class EnrollemntDate_EnrollmentStaffId_Added_on_Required_Entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "score",
                table: "student_courses",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "enrollment_staff_id",
                table: "student_courses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "enrollment_date",
                table: "student_classes",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "enrollment_staff_id",
                table: "student_classes",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "min_score",
                table: "gradings",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "max_score",
                table: "gradings",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "grade_point",
                table: "gradings",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,2)",
                oldPrecision: 3,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "credit_hour",
                table: "courses",
                type: "numeric(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(3,2)",
                oldPrecision: 3,
                oldScale: 2,
                oldDefaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "enrollment_date",
                table: "course_classes",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "enrollment_staff_id",
                table: "course_classes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "ix_student_courses_enrollment_staff_id",
                table: "student_courses",
                column: "enrollment_staff_id");

            migrationBuilder.CreateIndex(
                name: "ix_student_classes_enrollment_staff_id",
                table: "student_classes",
                column: "enrollment_staff_id");

            migrationBuilder.CreateIndex(
                name: "ix_course_classes_enrollment_staff_id",
                table: "course_classes",
                column: "enrollment_staff_id");

            migrationBuilder.AddForeignKey(
                name: "fk_course_classes_staffs_enrollment_staff_id",
                table: "course_classes",
                column: "enrollment_staff_id",
                principalTable: "staffs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_student_classes_staffs_enrollment_staff_id",
                table: "student_classes",
                column: "enrollment_staff_id",
                principalTable: "staffs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_student_courses_staffs_enrollment_staff_id",
                table: "student_courses",
                column: "enrollment_staff_id",
                principalTable: "staffs",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_course_classes_staffs_enrollment_staff_id",
                table: "course_classes");

            migrationBuilder.DropForeignKey(
                name: "fk_student_classes_staffs_enrollment_staff_id",
                table: "student_classes");

            migrationBuilder.DropForeignKey(
                name: "fk_student_courses_staffs_enrollment_staff_id",
                table: "student_courses");

            migrationBuilder.DropIndex(
                name: "ix_student_courses_enrollment_staff_id",
                table: "student_courses");

            migrationBuilder.DropIndex(
                name: "ix_student_classes_enrollment_staff_id",
                table: "student_classes");

            migrationBuilder.DropIndex(
                name: "ix_course_classes_enrollment_staff_id",
                table: "course_classes");

            migrationBuilder.DropColumn(
                name: "enrollment_staff_id",
                table: "student_courses");

            migrationBuilder.DropColumn(
                name: "enrollment_date",
                table: "student_classes");

            migrationBuilder.DropColumn(
                name: "enrollment_staff_id",
                table: "student_classes");

            migrationBuilder.DropColumn(
                name: "enrollment_date",
                table: "course_classes");

            migrationBuilder.DropColumn(
                name: "enrollment_staff_id",
                table: "course_classes");

            migrationBuilder.AlterColumn<decimal>(
                name: "score",
                table: "student_courses",
                type: "numeric(3,2)",
                precision: 3,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "min_score",
                table: "gradings",
                type: "numeric(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "max_score",
                table: "gradings",
                type: "numeric(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "grade_point",
                table: "gradings",
                type: "numeric(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AlterColumn<decimal>(
                name: "credit_hour",
                table: "courses",
                type: "numeric(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,2)",
                oldPrecision: 5,
                oldScale: 2,
                oldDefaultValue: 0m);
        }
    }
}
