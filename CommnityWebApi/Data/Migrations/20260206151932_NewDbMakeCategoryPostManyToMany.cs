using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CommnityWebApi.Data.Migrations
{
    /// <inheritdoc />
    public partial class NewDbMakeCategoryPostManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Category_Posts_PostId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Category_PostId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "Category");

            migrationBuilder.CreateTable(
                name: "CategoryPost",
                columns: table => new
                {
                    CategoriesCategoryId = table.Column<int>(type: "int", nullable: false),
                    PostsPostId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryPost", x => new { x.CategoriesCategoryId, x.PostsPostId });
                    table.ForeignKey(
                        name: "FK_CategoryPost_Category_CategoriesCategoryId",
                        column: x => x.CategoriesCategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryPost_Posts_PostsPostId",
                        column: x => x.PostsPostId,
                        principalTable: "Posts",
                        principalColumn: "PostId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryPost_PostsPostId",
                table: "CategoryPost",
                column: "PostsPostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoryPost");

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Category_PostId",
                table: "Category",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Posts_PostId",
                table: "Category",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId");
        }
    }
}
