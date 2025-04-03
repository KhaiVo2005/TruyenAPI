using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TruyenAPI.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stories",
                columns: table => new
                {
                    StoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoryDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StoryImage = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stories", x => x.StoryID);
                });

            migrationBuilder.CreateTable(
                name: "chapters",
                columns: table => new
                {
                    ChapterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChapterNumber = table.Column<int>(type: "int", nullable: false),
                    StoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chapters", x => x.ChapterID);
                    table.ForeignKey(
                        name: "FK_chapters_stories_StoryID",
                        column: x => x.StoryID,
                        principalTable: "stories",
                        principalColumn: "StoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "chapterImages",
                columns: table => new
                {
                    ChapterImageID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Image = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    ChapterID = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chapterImages", x => x.ChapterImageID);
                    table.ForeignKey(
                        name: "FK_chapterImages_chapters_ChapterID",
                        column: x => x.ChapterID,
                        principalTable: "chapters",
                        principalColumn: "ChapterID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_chapterImages_ChapterID",
                table: "chapterImages",
                column: "ChapterID");

            migrationBuilder.CreateIndex(
                name: "IX_chapters_StoryID",
                table: "chapters",
                column: "StoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chapterImages");

            migrationBuilder.DropTable(
                name: "chapters");

            migrationBuilder.DropTable(
                name: "stories");
        }
    }
}
