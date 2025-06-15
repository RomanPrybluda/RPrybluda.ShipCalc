using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipCalc.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTableNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_calculation_datas_ships_ship_id",
                table: "calculation_datas");

            migrationBuilder.DropPrimaryKey(
                name: "pk_calculation_datas",
                table: "calculation_datas");

            migrationBuilder.RenameTable(
                name: "calculation_datas",
                newName: "cii_calculations");

            migrationBuilder.RenameIndex(
                name: "ix_calculation_datas_ship_id",
                table: "cii_calculations",
                newName: "ix_cii_calculations_ship_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_cii_calculations",
                table: "cii_calculations",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_cii_calculations_ships_ship_id",
                table: "cii_calculations",
                column: "ship_id",
                principalTable: "ships",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cii_calculations_ships_ship_id",
                table: "cii_calculations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_cii_calculations",
                table: "cii_calculations");

            migrationBuilder.RenameTable(
                name: "cii_calculations",
                newName: "calculation_datas");

            migrationBuilder.RenameIndex(
                name: "ix_cii_calculations_ship_id",
                table: "calculation_datas",
                newName: "ix_calculation_datas_ship_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_calculation_datas",
                table: "calculation_datas",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_calculation_datas_ships_ship_id",
                table: "calculation_datas",
                column: "ship_id",
                principalTable: "ships",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
