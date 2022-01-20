using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API3.Migrations
{
    public partial class create : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Film",
                columns: table => new
                {
                    imdbID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Year = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Poster = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plot = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Runtime = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Film", x => x.imdbID);
                });

            migrationBuilder.CreateTable(
                name: "ApiFilm",
                columns: table => new
                {
                    ApiFilmId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    imdbID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilmimdbID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiFilm", x => x.ApiFilmId);
                    table.ForeignKey(
                        name: "FK_ApiFilm_Film_FilmimdbID",
                        column: x => x.FilmimdbID,
                        principalTable: "Film",
                        principalColumn: "imdbID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApiFilm_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiFilm_FilmimdbID",
                table: "ApiFilm",
                column: "FilmimdbID");

            migrationBuilder.CreateIndex(
                name: "IX_ApiFilm_UserId",
                table: "ApiFilm",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiFilm");

            migrationBuilder.DropTable(
                name: "Film");
        }
    }
}
