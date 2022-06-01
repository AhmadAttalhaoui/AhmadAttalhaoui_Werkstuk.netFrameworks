using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API3.Migrations
{
    public partial class _100110 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Critique_Film_imdbID",
                table: "Critique");

            migrationBuilder.AlterColumn<string>(
                name: "imdbID",
                table: "Critique",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Critique_Film_imdbID",
                table: "Critique",
                column: "imdbID",
                principalTable: "Film",
                principalColumn: "imdbID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Critique_Film_imdbID",
                table: "Critique");

            migrationBuilder.AlterColumn<string>(
                name: "imdbID",
                table: "Critique",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Critique_Film_imdbID",
                table: "Critique",
                column: "imdbID",
                principalTable: "Film",
                principalColumn: "imdbID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
