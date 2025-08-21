using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace SchoolManager.Infrastructure.Migrations
{
    public partial class AddTeacherSalary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<decimal>(
            //    name: "BaseSalary",
            //    table: "Employees",
            //    type: "numeric(18,2)",
            //    nullable: true);

            migrationBuilder.CreateTable(
                name: "TeacherSalaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SchoolYearStart = table.Column<int>(type: "integer", nullable: false),
                    Semester = table.Column<int>(type: "integer", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    IsApproved = table.Column<bool>(type: "boolean", nullable: false),
                    ApprovedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSalaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MotivationalAllowances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TeacherSalaryId = table.Column<int>(type: "integer", nullable: false),
                    TeacherId = table.Column<int>(type: "integer", nullable: false),
                    Percentage = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotivationalAllowances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotivationalAllowances_Employees_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MotivationalAllowances_TeacherSalaries_TeacherSalaryId",
                        column: x => x.TeacherSalaryId,
                        principalTable: "TeacherSalaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MotivationalAllowances_TeacherId",
                table: "MotivationalAllowances",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_MotivationalAllowances_TeacherSalaryId",
                table: "MotivationalAllowances",
                column: "TeacherSalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSalaries_SchoolYearStart_Semester",
                table: "TeacherSalaries",
                columns: new[] { "SchoolYearStart", "Semester" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotivationalAllowances");

            migrationBuilder.DropTable(
                name: "TeacherSalaries");

            migrationBuilder.DropColumn(
                name: "BaseSalary",
                table: "Employees");
        }
    }
}