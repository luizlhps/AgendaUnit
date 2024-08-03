using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgendaUnit.Infra.Migrations
{
    /// <inheritdoc />
    public partial class alter_login_field_user : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "login",
                table: "user",
                newName: "username");

            migrationBuilder.RenameIndex(
                name: "ix_user_login",
                table: "user",
                newName: "ix_user_username");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "user",
                newName: "login");

            migrationBuilder.RenameIndex(
                name: "ix_user_username",
                table: "user",
                newName: "ix_user_login");
        }
    }
}
