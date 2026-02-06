using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManagment.Api.Migrations
{
    /// <inheritdoc />
    public partial class remove_notNeeded_fields_and_apply_some_constraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentRandomName",
                schema: "ContractApi",
                table: "ContractDocuments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "ContractApi",
                table: "Clients");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ContractApi",
                table: "ContractDocumentTypes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDocumentTypes_Name",
                schema: "ContractApi",
                table: "ContractDocumentTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContractDocumentTypes_Name",
                schema: "ContractApi",
                table: "ContractDocumentTypes");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ContractApi",
                table: "ContractDocumentTypes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "DocumentRandomName",
                schema: "ContractApi",
                table: "ContractDocuments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "ContractApi",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
