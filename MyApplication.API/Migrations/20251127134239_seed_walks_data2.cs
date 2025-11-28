using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyApplication.API.Migrations
{
    /// <inheritdoc />
    public partial class seed_walks_data2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Walks",
                keyColumn: "Id",
                keyValue: new Guid("13bfd5e4-d207-49d0-82f3-ce7093cc4aac"));

            migrationBuilder.DeleteData(
                table: "Walks",
                keyColumn: "Id",
                keyValue: new Guid("80b40aae-9c3e-4c7b-968f-1a778e200e38"));

            migrationBuilder.InsertData(
                table: "Walks",
                columns: new[] { "Id", "Description", "DifficultyId", "LengthInKm", "Name", "RegionId", "WalkImageUrl" },
                values: new object[,]
                {
                    { new Guid("1036043c-1955-4b53-bf46-a3651dbe33f6"), "this is the descrion of Mount Victoria Lookout Walk", new Guid("f808ddcd-b5e5-4d80-b732-1ca523e48434"), 10.0, "Great China Wall", new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"), "some-dummy.png" },
                    { new Guid("b6e6dbb4-49fe-425f-8786-b4dcc1edbdda"), "this is the descrion of Mount Victoria Lookout Walk", new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"), 5.0, "Mount Victoria Lookout Walk", new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"), "some-dummy.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Walks",
                keyColumn: "Id",
                keyValue: new Guid("1036043c-1955-4b53-bf46-a3651dbe33f6"));

            migrationBuilder.DeleteData(
                table: "Walks",
                keyColumn: "Id",
                keyValue: new Guid("b6e6dbb4-49fe-425f-8786-b4dcc1edbdda"));

            migrationBuilder.InsertData(
                table: "Walks",
                columns: new[] { "Id", "Description", "DifficultyId", "LengthInKm", "Name", "RegionId", "WalkImageUrl" },
                values: new object[,]
                {
                    { new Guid("13bfd5e4-d207-49d0-82f3-ce7093cc4aac"), "this is the descrion of Mount Victoria Lookout Walk", new Guid("f808ddcd-b5e5-4d80-b732-1ca523e48434"), 10.0, "Great China Wall", new Guid("cfa06ed2-bf65-4b65-93ed-c9d286ddb0de"), "some-dummy.png" },
                    { new Guid("80b40aae-9c3e-4c7b-968f-1a778e200e38"), "this is the descrion of Mount Victoria Lookout Walk", new Guid("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"), 5.0, "Mount Victoria Lookout Walk", new Guid("f077a22e-4248-4bf6-b564-c7cf4e250263"), "some-dummy.png" }
                });
        }
    }
}
