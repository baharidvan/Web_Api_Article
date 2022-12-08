using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Books.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class SeedArticleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Articles",
                columns: new[] { "Id", "Author", "Name", "Price", "Publisher" },
                values: new object[,]
                {
                    { 1, "Doğan cüceloğlu", "Savaşçı", 100, "Timaş" },
                    { 2, "Can Yılmaz", "Osmanlı Tarihi", 120, "Can" },
                    { 3, "Tolstoy", "Savaş ve Barış", 140, "Zeren" },
                    { 4, "Ahmet Akpınar", "Kanlı Elmas", 200, "Parıltı" },
                    { 5, "Volkan Kırat", "Muhteşem İstanbul", 125, "Levent" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Articles",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
