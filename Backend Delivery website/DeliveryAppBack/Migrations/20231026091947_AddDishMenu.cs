using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryAppBack.Migrations
{
    /// <inheritdoc />
    public partial class AddDishMenu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DishMenu",
                columns: table => new
                {
                    Id=table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name= table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description= table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price= table.Column<double>(type: "float", nullable: false),
                    Image= table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Vegeterian= table.Column<bool>(type: "bit", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DishMenu", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DishMenu");
        }
    }
}
