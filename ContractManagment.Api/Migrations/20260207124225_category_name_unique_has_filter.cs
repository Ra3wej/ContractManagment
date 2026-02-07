using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManagment.Api.Migrations
{
    /// <inheritdoc />
    public partial class category_name_unique_has_filter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                schema: "ContractApi",
                table: "Categories");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                schema: "ContractApi",
                table: "Categories",
                column: "Name",
                unique: true,
                filter: "[StatusIsActive] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                schema: "ContractApi",
                table: "Categories");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                schema: "ContractApi",
                table: "Categories",
                column: "Name",
                unique: true);
        }
    }
}
