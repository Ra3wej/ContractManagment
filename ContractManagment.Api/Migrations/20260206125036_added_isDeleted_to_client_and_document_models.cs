using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManagment.Api.Migrations
{
    /// <inheritdoc />
    public partial class added_isDeleted_to_client_and_document_models : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClonedFromContractId",
                schema: "ContractApi",
                table: "Contracts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "ContractApi",
                table: "Contracts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "ContractApi",
                table: "ContractDocuments",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "ContractApi",
                table: "Clients",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                schema: "ContractApi",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            //migrationBuilder.CreateIndex(
            //    name: "IX_Contracts_ClonedFromContractId",
            //    schema: "ContractApi",
            //    table: "Contracts",
            //    column: "ClonedFromContractId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_Contracts_ClonedFromContractId",
                schema: "ContractApi",
                table: "Contracts",
                column: "ClonedFromContractId",
                principalSchema: "ContractApi",
                principalTable: "Contracts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_Contracts_ClonedFromContractId",
                schema: "ContractApi",
                table: "Contracts");

            //migrationBuilder.DropIndex(
            //    name: "IX_Contracts_ClonedFromContractId",
            //    schema: "ContractApi",
            //    table: "Contracts");

            migrationBuilder.DropColumn(
                name: "ClonedFromContractId",
                schema: "ContractApi",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "ContractApi",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "ContractApi",
                table: "ContractDocuments");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "ContractApi",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                schema: "ContractApi",
                table: "Categories");
        }
    }
}
