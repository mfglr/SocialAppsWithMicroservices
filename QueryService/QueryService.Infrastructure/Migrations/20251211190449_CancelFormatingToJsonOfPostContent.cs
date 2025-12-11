using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueryService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CancelFormatingToJsonOfPostContent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Content",
                table: "Posts",
                newName: "Content_Value");

            migrationBuilder.AddColumn<int>(
                name: "Content_ModerationResult_Hate",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Content_ModerationResult_SelfHarm",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Content_ModerationResult_Sexual",
                table: "Posts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Content_ModerationResult_Violence",
                table: "Posts",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Content_ModerationResult_Hate",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Content_ModerationResult_SelfHarm",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Content_ModerationResult_Sexual",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Content_ModerationResult_Violence",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "Content_Value",
                table: "Posts",
                newName: "Content");
        }
    }
}
