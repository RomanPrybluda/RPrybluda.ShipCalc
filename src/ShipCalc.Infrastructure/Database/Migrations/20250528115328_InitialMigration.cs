using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipCalc.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "capacity_ice_strength_corr_factors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ice_class = table.Column<int>(type: "integer", nullable: false),
                    constant_a = table.Column<decimal>(type: "numeric(5,4)", precision: 5, scale: 4, nullable: false),
                    constant_b = table.Column<decimal>(type: "numeric(4,1)", precision: 4, scale: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_capacity_ice_strength_corr_factors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cii_rating_thresholds",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ship_type = table.Column<int>(type: "integer", nullable: false),
                    lower_deadweight = table.Column<int>(type: "integer", precision: 18, scale: 2, nullable: true),
                    upper_deadweight = table.Column<int>(type: "integer", precision: 18, scale: 2, nullable: true),
                    d1 = table.Column<decimal>(type: "numeric(3,2)", precision: 3, scale: 2, nullable: false, comment: "Threshold D1 for CII rating"),
                    d2 = table.Column<decimal>(type: "numeric(3,2)", precision: 3, scale: 2, nullable: false, comment: "Threshold D2 for CII rating"),
                    d3 = table.Column<decimal>(type: "numeric(3,2)", precision: 3, scale: 2, nullable: false, comment: "Threshold D3 for CII rating"),
                    d4 = table.Column<decimal>(type: "numeric(3,2)", precision: 3, scale: 2, nullable: false, comment: "Threshold D4 for CII rating")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cii_rating_thresholds", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cii_ref_line_params",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ship_type = table.Column<int>(type: "integer", nullable: false),
                    lower_bound = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: true),
                    upper_bound = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: true),
                    parameter_a = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false),
                    parameter_c = table.Column<decimal>(type: "numeric(18,6)", precision: 18, scale: 6, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cii_ref_line_params", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cii_req_reduction_factors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    year = table.Column<int>(type: "integer", nullable: false, comment: "The year for which the reduction factor applies (2023-2030)"),
                    reduction_factor_percentage = table.Column<int>(type: "integer", nullable: false, comment: "The reduction factor percentage (Z%) for the CII relative to the 2019 reference line")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cii_req_reduction_factors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ia_super_and_ia_ice_corr_factors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ice_class = table.Column<int>(type: "integer", nullable: false),
                    correction_factor = table.Column<decimal>(type: "numeric(3,2)", precision: 3, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ia_super_and_ia_ice_corr_factors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ref_design_block_coeffs",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    ship_type = table.Column<int>(type: "integer", nullable: false),
                    min_deadweight = table.Column<int>(type: "integer", nullable: true),
                    max_deadweight = table.Column<int>(type: "integer", nullable: true),
                    block_coefficient = table.Column<decimal>(type: "numeric(3,2)", precision: 3, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ref_design_block_coeffs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ships",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    imo_number = table.Column<int>(type: "integer", nullable: false, comment: "Unique IMO number for the ship"),
                    ship_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    gross_tonnage = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    summer_deadweight = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    block_coefficient = table.Column<decimal>(type: "numeric(4,3)", precision: 4, scale: 3, nullable: false),
                    cargo_compartment_cubic_capacity = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    ship_type = table.Column<int>(type: "integer", nullable: false),
                    ice_class = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_ships", x => x.id);
                    table.CheckConstraint("ck_ship_block_coefficient_range", "\"block_coefficient\" >= 0 AND \"block_coefficient\" <= 1");
                });

            migrationBuilder.CreateTable(
                name: "calculation_datas",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    co2emissions_in_tons = table.Column<decimal>(type: "numeric(10,3)", precision: 10, scale: 3, nullable: false),
                    distance_travelled_in_n_ms = table.Column<decimal>(type: "numeric(10,3)", precision: 10, scale: 3, nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    capacity = table.Column<decimal>(type: "numeric(10,3)", precision: 10, scale: 3, nullable: false),
                    ref_line_parameter_a = table.Column<decimal>(type: "numeric(10,3)", precision: 10, scale: 3, nullable: false),
                    ref_line_parameter_c = table.Column<decimal>(type: "numeric(10,3)", precision: 10, scale: 3, nullable: false),
                    ref_line = table.Column<decimal>(type: "numeric(10,3)", precision: 10, scale: 3, nullable: false),
                    ref_line_reduction_factor = table.Column<int>(type: "integer", nullable: false),
                    required_carbon_intensity_indicator = table.Column<decimal>(type: "numeric(10,3)", precision: 10, scale: 3, nullable: false),
                    ice_clased_ship_capacity_corr_factor = table.Column<decimal>(type: "numeric(10,3)", precision: 10, scale: 3, nullable: false),
                    ia_super_and_ia_ice_corr_factor = table.Column<decimal>(type: "numeric(10,3)", precision: 10, scale: 3, nullable: false),
                    attained_carbon_intensity_indicator = table.Column<decimal>(type: "numeric(10,3)", precision: 10, scale: 3, nullable: false),
                    carbon_intensity_indicator_numerical_rating = table.Column<decimal>(type: "numeric(10,3)", precision: 10, scale: 3, nullable: false),
                    carbon_intensity_indicator_rating = table.Column<int>(type: "integer", nullable: false),
                    calculation_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    ship_id = table.Column<Guid>(type: "uuid", nullable: false, comment: "Foreign key referencing the associated Ship")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_calculation_datas", x => x.id);
                    table.ForeignKey(
                        name: "fk_calculation_datas_ships_ship_id",
                        column: x => x.ship_id,
                        principalTable: "ships",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_calculation_datas_ship_id",
                table: "calculation_datas",
                column: "ship_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarbonIntensityIndicatorRatingThresholds_ShipType_Deadweight",
                table: "cii_rating_thresholds",
                columns: new[] { "ship_type", "lower_deadweight", "upper_deadweight" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_ships_imo_number",
                table: "ships",
                column: "imo_number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "calculation_datas");

            migrationBuilder.DropTable(
                name: "capacity_ice_strength_corr_factors");

            migrationBuilder.DropTable(
                name: "cii_rating_thresholds");

            migrationBuilder.DropTable(
                name: "cii_ref_line_params");

            migrationBuilder.DropTable(
                name: "cii_req_reduction_factors");

            migrationBuilder.DropTable(
                name: "ia_super_and_ia_ice_corr_factors");

            migrationBuilder.DropTable(
                name: "ref_design_block_coeffs");

            migrationBuilder.DropTable(
                name: "ships");
        }
    }
}
