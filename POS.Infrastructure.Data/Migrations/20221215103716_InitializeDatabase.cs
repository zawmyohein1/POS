using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace POS.Infrastructure.Data.Migrations
{
    public partial class InitializeDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audit_Trails",
                columns: table => new
                {
                    Audit_Trail_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action_ID = table.Column<int>(type: "int", nullable: false),
                    Module_ID = table.Column<int>(type: "int", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Audit_Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audit_Trails", x => x.Audit_Trail_ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Department_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Department_ID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Occupations",
                columns: table => new
                {
                    Occupation_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Occupation_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Department_ID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Occupations", x => x.Occupation_ID);
                    table.ForeignKey(
                        name: "FK_Occupations_Departments_Department_ID",
                        column: x => x.Department_ID,
                        principalTable: "Departments",
                        principalColumn: "Department_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_ID", "Created", "Email", "Gender", "IsDeleted", "Password", "Phone", "Role", "User_Name" },
                values: new object[] { 1, new DateTime(2022, 12, 15, 0, 0, 0, 0, DateTimeKind.Local), "admin@gmail.com", 1, false, "yngWIE500", "9484774", "", "admin" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "User_ID", "Created", "Email", "Gender", "IsDeleted", "Password", "Phone", "Role", "User_Name" },
                values: new object[] { 2, new DateTime(2022, 12, 15, 0, 0, 0, 0, DateTimeKind.Local), "user@gmail.com", 1, false, "yngWIE500", "7575664", "", "user" });

            migrationBuilder.CreateIndex(
                name: "IX_Occupations_Department_ID",
                table: "Occupations",
                column: "Department_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audit_Trails");

            migrationBuilder.DropTable(
                name: "Occupations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
