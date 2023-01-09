using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Article.MVC.Migrations
{
    public partial class UserInviteCode_AppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InviteCode",
                table: "AspNetUsers",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InviteCode",
                table: "AspNetUsers");
        }
    }
}
