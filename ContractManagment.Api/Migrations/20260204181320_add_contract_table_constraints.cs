using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContractManagment.Api.Migrations
{
    /// <inheritdoc />
    public partial class add_contract_table_constraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddCheckConstraint(
                name: "CK_Contract_Value_greater_than_zero",
                schema: "ContractApi",
                table: "Contracts",
                sql: "[ContractValue] >= 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Contract_Value_greater_than_zero",
                schema: "ContractApi",
                table: "Contracts");
        }
    }
}
