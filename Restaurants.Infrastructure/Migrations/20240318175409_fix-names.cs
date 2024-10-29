using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurants.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixnames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ConcactNumber",
                table: "Restaurants",
                newName: "ContactNumber");

            migrationBuilder.RenameColumn(
                name: "ConcactEmail",
                table: "Restaurants",
                newName: "ContactEmail");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContactNumber",
                table: "Restaurants",
                newName: "ConcactNumber");

            migrationBuilder.RenameColumn(
                name: "ContactEmail",
                table: "Restaurants",
                newName: "ConcactEmail");
        }
    }
}
