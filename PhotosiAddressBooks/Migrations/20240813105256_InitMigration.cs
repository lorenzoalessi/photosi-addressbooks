using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PhotosiAddressBooks.Migrations
{
    /// <inheritdoc />
    public partial class InitMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address_book",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    address_name = table.Column<string>(type: "text", nullable: false),
                    address_number = table.Column<string>(type: "text", nullable: false),
                    cap = table.Column<string>(type: "text", nullable: false),
                    city_name = table.Column<string>(type: "text", nullable: false),
                    country_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_address_book", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_address_book_address_name_address_number_cap_city_name_coun~",
                table: "address_book",
                columns: new[] { "address_name", "address_number", "cap", "city_name", "country_name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "address_book");
        }
    }
}
