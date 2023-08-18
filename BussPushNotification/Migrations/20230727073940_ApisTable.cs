using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BussPushNotification.Migrations
{
    /// <inheritdoc />
    public partial class ApisTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Apis",
                columns: table => new
                {
                    Apiname = table.Column<string>(type: "text", nullable: false),
                    Apikey = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Apis", x => x.Apiname);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Apis");
        }
    }
}
