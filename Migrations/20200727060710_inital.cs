using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Identity.Migrations
{
    public partial class inital : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodSchedulerTimeStarts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodSchedulerTimeStarts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrainDish",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrainDish", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GrainDishNutrient",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrainName = table.Column<int>(nullable: false),
                    SoupRequired = table.Column<bool>(nullable: false),
                    KaroMainIngredientsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GrainDishNutrient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LightFood",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LightFood", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LightFoodNutrient",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LightFoodName = table.Column<int>(nullable: false),
                    LightFoodMainIngredient = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LightFoodNutrient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MainIngredient",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    ClassOfFood = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainIngredient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Soup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Swallow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Swallow", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SwallowNutrient",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SwallowName = table.Column<int>(nullable: false),
                    MainIngredientsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwallowNutrient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SwallowSoup",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SwallowSoupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SwallowSoup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserGrainDishSelection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    UserGrainDishId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserGrainDishSelection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLightFoodSelection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    UserLightFoodId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLightFoodSelection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSoupSelection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    UserSoupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSoupSelection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserSwallowSelection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    UserSwallowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSwallowSelection", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodSchedulerTimeStarts");

            migrationBuilder.DropTable(
                name: "GrainDish");

            migrationBuilder.DropTable(
                name: "GrainDishNutrient");

            migrationBuilder.DropTable(
                name: "LightFood");

            migrationBuilder.DropTable(
                name: "LightFoodNutrient");

            migrationBuilder.DropTable(
                name: "MainIngredient");

            migrationBuilder.DropTable(
                name: "Soup");

            migrationBuilder.DropTable(
                name: "Swallow");

            migrationBuilder.DropTable(
                name: "SwallowNutrient");

            migrationBuilder.DropTable(
                name: "SwallowSoup");

            migrationBuilder.DropTable(
                name: "UserGrainDishSelection");

            migrationBuilder.DropTable(
                name: "UserLightFoodSelection");

            migrationBuilder.DropTable(
                name: "UserSoupSelection");

            migrationBuilder.DropTable(
                name: "UserSwallowSelection");
        }
    }
}
