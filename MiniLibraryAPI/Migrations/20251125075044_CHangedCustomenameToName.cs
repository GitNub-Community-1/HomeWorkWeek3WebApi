using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniLibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class CHangedCustomenameToName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CustomerName",
                table: "Orders",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Orders",
                newName: "CustomerName");
        }
    }
}
