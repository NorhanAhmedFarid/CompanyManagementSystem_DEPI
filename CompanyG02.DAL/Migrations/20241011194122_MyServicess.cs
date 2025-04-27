using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CompanyG02.DAL.Migrations
{
    public partial class MyServicess : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Services_Departments_DepartmentId",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "MyServices");

            migrationBuilder.RenameIndex(
                name: "IX_Services_DepartmentId",
                table: "MyServices",
                newName: "IX_MyServices_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MyServices",
                table: "MyServices",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MyServices_Departments_DepartmentId",
                table: "MyServices",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MyServices_Departments_DepartmentId",
                table: "MyServices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MyServices",
                table: "MyServices");

            migrationBuilder.RenameTable(
                name: "MyServices",
                newName: "Services");

            migrationBuilder.RenameIndex(
                name: "IX_MyServices_DepartmentId",
                table: "Services",
                newName: "IX_Services_DepartmentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_Departments_DepartmentId",
                table: "Services",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
