using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API3.Migrations
{
    public partial class _5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Critique",
                table: "Critique");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Critique",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Critique",
                table: "Critique",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Critique_imdbID",
                table: "Critique",
                column: "imdbID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Critique",
                table: "Critique");

            migrationBuilder.DropIndex(
                name: "IX_Critique_imdbID",
                table: "Critique");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Critique");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Critique",
                table: "Critique",
                column: "imdbID");
        }
    }
}
