using Microsoft.EntityFrameworkCore.Migrations;

namespace APPZ.DAL.Migrations
{
    public partial class AddIdentityToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdentityId",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_IdentityId",
                table: "Users",
                column: "IdentityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Identities_IdentityId",
                table: "Users",
                column: "IdentityId",
                principalTable: "Identities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Identities_IdentityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_IdentityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IdentityId",
                table: "Users");
        }
    }
}
