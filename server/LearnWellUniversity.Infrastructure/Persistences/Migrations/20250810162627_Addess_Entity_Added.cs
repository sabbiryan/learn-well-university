using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LearnWellUniversity.Infrastructure.Persistences.Migrations
{
    /// <inheritdoc />
    public partial class Addess_Entity_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "permanent_address_id",
                table: "students",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "present_address_id",
                table: "students",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "permanent_address_id",
                table: "staffs",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "present_address_id",
                table: "staffs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "addresses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    street = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    state = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    zip_code = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    country = table.Column<string>(type: "text", nullable: false),
                    creator = table.Column<string>(type: "text", nullable: true),
                    modifier = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addresses", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_students_permanent_address_id",
                table: "students",
                column: "permanent_address_id");

            migrationBuilder.CreateIndex(
                name: "ix_students_present_address_id",
                table: "students",
                column: "present_address_id");

            migrationBuilder.CreateIndex(
                name: "ix_staffs_permanent_address_id",
                table: "staffs",
                column: "permanent_address_id");

            migrationBuilder.CreateIndex(
                name: "ix_staffs_present_address_id",
                table: "staffs",
                column: "present_address_id");

            migrationBuilder.AddForeignKey(
                name: "fk_staffs_addresses_permanent_address_id",
                table: "staffs",
                column: "permanent_address_id",
                principalTable: "addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_staffs_addresses_present_address_id",
                table: "staffs",
                column: "present_address_id",
                principalTable: "addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_students_addresses_permanent_address_id",
                table: "students",
                column: "permanent_address_id",
                principalTable: "addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "fk_students_addresses_present_address_id",
                table: "students",
                column: "present_address_id",
                principalTable: "addresses",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_staffs_addresses_permanent_address_id",
                table: "staffs");

            migrationBuilder.DropForeignKey(
                name: "fk_staffs_addresses_present_address_id",
                table: "staffs");

            migrationBuilder.DropForeignKey(
                name: "fk_students_addresses_permanent_address_id",
                table: "students");

            migrationBuilder.DropForeignKey(
                name: "fk_students_addresses_present_address_id",
                table: "students");

            migrationBuilder.DropTable(
                name: "addresses");

            migrationBuilder.DropIndex(
                name: "ix_students_permanent_address_id",
                table: "students");

            migrationBuilder.DropIndex(
                name: "ix_students_present_address_id",
                table: "students");

            migrationBuilder.DropIndex(
                name: "ix_staffs_permanent_address_id",
                table: "staffs");

            migrationBuilder.DropIndex(
                name: "ix_staffs_present_address_id",
                table: "staffs");

            migrationBuilder.DropColumn(
                name: "permanent_address_id",
                table: "students");

            migrationBuilder.DropColumn(
                name: "present_address_id",
                table: "students");

            migrationBuilder.DropColumn(
                name: "permanent_address_id",
                table: "staffs");

            migrationBuilder.DropColumn(
                name: "present_address_id",
                table: "staffs");
        }
    }
}
