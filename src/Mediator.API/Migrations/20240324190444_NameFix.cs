using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mediator.API.Migrations
{
    /// <inheritdoc />
    public partial class NameFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShortenedUrl",
                table: "Links",
                newName: "ShorteredUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ShorteredUrl",
                table: "Links",
                newName: "ShortenedUrl");
        }
    }
}
