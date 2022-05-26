using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API3.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Critique_Film_ImdbID",
                table: "Critique");

            migrationBuilder.RenameColumn(
                name: "ImdbID",
                table: "Critique",
                newName: "imdbID");

            migrationBuilder.AddForeignKey(
                name: "FK_Critique_Film_imdbID",
                table: "Critique",
                column: "imdbID",
                principalTable: "Film",
                principalColumn: "imdbID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Critique_Film_imdbID",
                table: "Critique");

            migrationBuilder.RenameColumn(
                name: "imdbID",
                table: "Critique",
                newName: "ImdbID");

            migrationBuilder.AddForeignKey(
                name: "FK_Critique_Film_ImdbID",
                table: "Critique",
                column: "ImdbID",
                principalTable: "Film",
                principalColumn: "imdbID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
