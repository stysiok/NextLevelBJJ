using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NextLevelBJJ.Api.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PassCode = table.Column<byte[]>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    HasDeclaration = table.Column<short>(nullable: false),
                    CreatedBy = table.Column<byte[]>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<short>(nullable: false),
                    ModifiedBy = table.Column<byte[]>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_Students_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Students_Students_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PassTypes",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Entries = table.Column<int>(nullable: false),
                    IsOpen = table.Column<short>(nullable: false),
                    CreatedBy = table.Column<byte[]>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<short>(nullable: false),
                    ModifiedBy = table.Column<byte[]>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PassTypes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PassTypes_Students_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PassTypes_Students_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Training",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    StartHour = table.Column<TimeSpan>(nullable: false),
                    FinishHour = table.Column<TimeSpan>(nullable: false),
                    IsKidsTraining = table.Column<short>(nullable: false),
                    CreatedBy = table.Column<byte[]>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<short>(nullable: false),
                    ModifiedBy = table.Column<byte[]>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Training", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Training_Students_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Training_Students_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passes",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    ExpirationDate = table.Column<DateTime>(nullable: false),
                    StudentId = table.Column<byte[]>(nullable: false),
                    Price = table.Column<int>(nullable: false),
                    TypeId = table.Column<byte[]>(nullable: false),
                    PassTypeId = table.Column<byte[]>(nullable: true),
                    CreatedBy = table.Column<byte[]>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<short>(nullable: false),
                    ModifiedBy = table.Column<byte[]>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Passes_Students_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Passes_Students_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Passes_PassTypes_PassTypeId",
                        column: x => x.PassTypeId,
                        principalTable: "PassTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Passes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    Id = table.Column<byte[]>(nullable: false),
                    IsFree = table.Column<short>(nullable: false),
                    PassId = table.Column<byte[]>(nullable: true),
                    StudentId = table.Column<byte[]>(nullable: true),
                    TrainingId = table.Column<byte[]>(nullable: true),
                    CreatedBy = table.Column<byte[]>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<short>(nullable: false),
                    ModifiedBy = table.Column<byte[]>(nullable: false),
                    ModifiedDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_Students_CreatedBy",
                        column: x => x.CreatedBy,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_Students_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_Passes_PassId",
                        column: x => x.PassId,
                        principalTable: "Passes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendance_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendance_Training_TrainingId",
                        column: x => x.TrainingId,
                        principalTable: "Training",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_CreatedBy",
                table: "Attendance",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_ModifiedBy",
                table: "Attendance",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_PassId",
                table: "Attendance",
                column: "PassId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_StudentId",
                table: "Attendance",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_TrainingId",
                table: "Attendance",
                column: "TrainingId");

            migrationBuilder.CreateIndex(
                name: "IX_Passes_CreatedBy",
                table: "Passes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Passes_ModifiedBy",
                table: "Passes",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Passes_PassTypeId",
                table: "Passes",
                column: "PassTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Passes_StudentId",
                table: "Passes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_PassTypes_CreatedBy",
                table: "PassTypes",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_PassTypes_ModifiedBy",
                table: "PassTypes",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Students_CreatedBy",
                table: "Students",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ModifiedBy",
                table: "Students",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Training_CreatedBy",
                table: "Training",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Training_ModifiedBy",
                table: "Training",
                column: "ModifiedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropTable(
                name: "Passes");

            migrationBuilder.DropTable(
                name: "Training");

            migrationBuilder.DropTable(
                name: "PassTypes");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
