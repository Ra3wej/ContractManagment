using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManagment.Api.Migrations
{
    /// <inheritdoc />
    public partial class add_some_business_rules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DocumentName",
                schema: "ContractApi",
                table: "ContractDocuments",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "ContractApi",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyRegistrationNumber",
                schema: "ContractApi",
                table: "Clients",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ContractApi",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDocuments_DocumentName_ContractId",
                schema: "ContractApi",
                table: "ContractDocuments",
                columns: new[] { "DocumentName", "ContractId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Clients_CompanyRegistrationNumber",
                schema: "ContractApi",
                table: "Clients",
                column: "CompanyRegistrationNumber",
                unique: true,
                filter: "[CompanyRegistrationNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                schema: "ContractApi",
                table: "Clients",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Name",
                schema: "ContractApi",
                table: "Categories",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ContractDocuments_DocumentName_ContractId",
                schema: "ContractApi",
                table: "ContractDocuments");

            migrationBuilder.DropIndex(
                name: "IX_Clients_CompanyRegistrationNumber",
                schema: "ContractApi",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Clients_Email",
                schema: "ContractApi",
                table: "Clients");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Name",
                schema: "ContractApi",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "DocumentName",
                schema: "ContractApi",
                table: "ContractDocuments",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "ContractApi",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "CompanyRegistrationNumber",
                schema: "ContractApi",
                table: "Clients",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "ContractApi",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
