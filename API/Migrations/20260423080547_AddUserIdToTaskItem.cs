using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentTaskTrackerAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToTaskItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TaskItems",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TaskItems");
        }
    }
}
