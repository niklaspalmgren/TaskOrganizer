using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class TaskEntityUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskBoards_TaskBoardId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "TaskBoardId",
                table: "Tasks",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskBoards_TaskBoardId",
                table: "Tasks",
                column: "TaskBoardId",
                principalTable: "TaskBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_TaskBoards_TaskBoardId",
                table: "Tasks");

            migrationBuilder.AlterColumn<int>(
                name: "TaskBoardId",
                table: "Tasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_TaskBoards_TaskBoardId",
                table: "Tasks",
                column: "TaskBoardId",
                principalTable: "TaskBoards",
                principalColumn: "Id");
        }
    }
}
