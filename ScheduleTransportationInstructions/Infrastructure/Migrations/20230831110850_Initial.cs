using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "instructions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    instruction_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    client_name = table.Column<string>(type: "text", nullable: false),
                    pickup_address = table.Column<string>(type: "text", nullable: false),
                    delivery_address = table.Column<string>(type: "text", nullable: false),
                    client_ref = table.Column<string>(type: "text", nullable: false),
                    billing_ref = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_instructions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    product_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    product_description = table.Column<string>(type: "text", nullable: false),
                    product_code = table.Column<string>(type: "text", nullable: false),
                    qty = table.Column<decimal>(type: "numeric", nullable: false),
                    instruction_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_product", x => x.product_id);
                    table.ForeignKey(
                        name: "fk_product_instructions_instruction_id",
                        column: x => x.instruction_id,
                        principalTable: "instructions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "schedule_transport",
                columns: table => new
                {
                    schedule_transport_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    date_scheduled = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    transporter = table.Column<string>(type: "text", nullable: false),
                    instruction_id = table.Column<int>(type: "integer", nullable: false),
                    product_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_schedule_transport", x => x.schedule_transport_id);
                    table.ForeignKey(
                        name: "fk_schedule_transport_instructions_instruction_id",
                        column: x => x.instruction_id,
                        principalTable: "instructions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_schedule_transport_product_product_id",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "product_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_product_instruction_id",
                table: "product",
                column: "instruction_id");

            migrationBuilder.CreateIndex(
                name: "ix_schedule_transport_instruction_id",
                table: "schedule_transport",
                column: "instruction_id");

            migrationBuilder.CreateIndex(
                name: "ix_schedule_transport_product_id",
                table: "schedule_transport",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "schedule_transport");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "instructions");
        }
    }
}
