using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniLibraryAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldPhoneInOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Orders",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Orders");
        }
    }
}
