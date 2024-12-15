using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaUnit.Infra.Migrations
{
    /// <inheritdoc />
    public partial class populate : Migration
    {

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "role",
                columns: new[] { "id", "name", "timestamp", "isdeleted" },
                values: new object[,]
                {
                    { 1, "Admin", DateTimeOffset.Now, false  } ,
                    { 2, "Employee", DateTimeOffset.Now, false  } ,
                    { 3, "Manager", DateTimeOffset.Now, false  }
                }
            );

            migrationBuilder.InsertData(
            table: "status",
            columns: new[] { "id", "name", "timestamp", "isdeleted" },
            values: new object[,]
            {
                    { 1, "Active", DateTimeOffset.Now, false  } ,
                    { 2, "Canceled", DateTimeOffset.Now, false  } ,
                    { 3, "Suspense", DateTimeOffset.Now, false  }
            }
        );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DeleteData(
                table: "role",
                keyColumn: "id",
                keyValues: new object[]
                {
                 1
                ,2
                ,3
                }
            );

            migrationBuilder.DeleteData(
            table: "status",
            keyColumn: "id",
            keyValues: new object[]
            {
                1
            ,2
            ,3
            }
        );

        }
    }
}
