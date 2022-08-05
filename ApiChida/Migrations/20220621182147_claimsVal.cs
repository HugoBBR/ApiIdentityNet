using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiChida.Migrations
{
    public partial class claimsVal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClaimValue",
                table: "AspNetClaims",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "ClaimType",
                table: "AspNetClaims",
                newName: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "AspNetClaims",
                newName: "ClaimValue");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "AspNetClaims",
                newName: "ClaimType");
        }
    }
}
