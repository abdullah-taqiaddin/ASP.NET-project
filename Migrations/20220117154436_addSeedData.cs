using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Final_Project.Migrations
{
    public partial class addSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "AuthorId", "DOB", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, new DateTime(1991, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JK@gmail.com", "JK Rowlings", 28653212 },
                    { 2, new DateTime(1985, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "JMD@outlook.com", "J. M. DeMatteis", 88653212 },
                    { 3, new DateTime(1963, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "DM@gmail.com", "David Mitchell", 98653212 }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Catagory", "Cover", "Name", "Price", "Publsiher", "ReleseDate" },
                values: new object[,]
                {
                    { 1, 3, "/images/Harry_Potter_and_the_Philosopher's_Stone.jpg", "Harry Potter", 50.0, "PublisherCo", new DateTime(1995, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, "/images/SpidermanKLH.jpg", "Spiderman: Kravens Last Hunt", 65.989999999999995, "NewPublisherCo", new DateTime(2002, 5, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, "/images/Cloud_atlas.jpg", "Cloud Atlas", 35.0, "ThePublisher", new DateTime(1951, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Authorship",
                columns: new[] { "ID", "AuthorId", "BookId", "Role" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 1, 2 },
                    { 3, 3, 2, 1 },
                    { 4, 3, 2, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authorship",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Authorship",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Authorship",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Authorship",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "AuthorId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 2);
        }
    }
}
