using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManager.Infrastructure.Migrations
{
#nullable disable

    namespace SchoolManager.Infrastructure.Migrations
    {
        [DbContext(typeof(Context))]
        [Migration("20250821195000_AddBaseSalary")]
        public partial class AddBaseSalary : Migration
        {
            protected override void Up(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.AddColumn<decimal>(
                    name: "BaseSalary",
                    table: "Employees",
                    type: "numeric",
                    nullable: true);
            }

            protected override void Down(MigrationBuilder migrationBuilder)
            {
                migrationBuilder.DropColumn(
                    name: "BaseSalary",
                    table: "Employees");
            }
        }
    }
}