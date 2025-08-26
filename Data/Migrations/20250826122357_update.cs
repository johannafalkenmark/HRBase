using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmploymentId",
                table: "Employees",
                column: "EmploymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employments_EmploymentId",
                table: "Employees",
                column: "EmploymentId",
                principalTable: "Employments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employments_EmploymentId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_EmploymentId",
                table: "Employees");
        }
    }
}
