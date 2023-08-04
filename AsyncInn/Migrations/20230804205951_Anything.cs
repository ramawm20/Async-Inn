using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AsyncInn.Migrations
{
    /// <inheritdoc />
    public partial class Anything : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmenitiesRoom",
                columns: table => new
                {
                    AmenitiesId = table.Column<int>(type: "int", nullable: false),
                    RoomsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmenitiesRoom", x => new { x.AmenitiesId, x.RoomsId });
                    table.ForeignKey(
                        name: "FK_AmenitiesRoom_Amenities_AmenitiesId",
                        column: x => x.AmenitiesId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AmenitiesRoom_Rooms_RoomsId",
                        column: x => x.RoomsId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "hotelRoom",
                columns: new[] { "HotelId", "RoomNumber", "Rate", "RoomId", "isPetFriendly" },
                values: new object[,]
                {
                    { 1, 101, 150.0m, 1, true },
                    { 1, 102, 180.0m, 2, false },
                    { 2, 201, 160.0m, 1, true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AmenitiesRoom_RoomsId",
                table: "AmenitiesRoom",
                column: "RoomsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AmenitiesRoom");

            migrationBuilder.DeleteData(
                table: "hotelRoom",
                keyColumns: new[] { "HotelId", "RoomNumber" },
                keyValues: new object[] { 1, 101 });

            migrationBuilder.DeleteData(
                table: "hotelRoom",
                keyColumns: new[] { "HotelId", "RoomNumber" },
                keyValues: new object[] { 1, 102 });

            migrationBuilder.DeleteData(
                table: "hotelRoom",
                keyColumns: new[] { "HotelId", "RoomNumber" },
                keyValues: new object[] { 2, 201 });
        }
    }
}
