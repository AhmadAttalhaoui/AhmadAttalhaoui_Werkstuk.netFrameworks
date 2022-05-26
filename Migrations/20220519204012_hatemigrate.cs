using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API3.Migrations
{
    public partial class hatemigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Critique",
                columns: table => new
                {
                    ImdbID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Critique", x => x.ImdbID);
                    table.ForeignKey(
                        name: "FK_Critique_Film_ImdbID",
                        column: x => x.ImdbID,
                        principalTable: "Film",
                        principalColumn: "imdbID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Critique");
        }
    }
}
