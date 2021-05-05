using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StopHunger.Migrations
{
    public partial class withManyToMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Categorie = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_Products_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShelterInfoType",
                columns: table => new
                {
                    ShelterInfoTypeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    Src = table.Column<string>(nullable: false),
                    Type = table.Column<string>(nullable: false),
                    ShortDescription = table.Column<string>(maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShelterInfoType", x => x.ShelterInfoTypeId);
                    table.ForeignKey(
                        name: "FK_ShelterInfoType_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProductDonations",
                columns: table => new
                {
                    UserProductDonationId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProductDonations", x => x.UserProductDonationId);
                    table.ForeignKey(
                        name: "FK_UserProductDonations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProductDonations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductDonutionToShelters",
                columns: table => new
                {
                    ProductDonutionToShelterId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    UpdatedAt = table.Column<DateTime>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ShelterInfoTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDonutionToShelters", x => x.ProductDonutionToShelterId);
                    table.ForeignKey(
                        name: "FK_ProductDonutionToShelters_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDonutionToShelters_ShelterInfoType_ShelterInfoTypeId",
                        column: x => x.ShelterInfoTypeId,
                        principalTable: "ShelterInfoType",
                        principalColumn: "ShelterInfoTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDonutionToShelters_ProductId",
                table: "ProductDonutionToShelters",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDonutionToShelters_ShelterInfoTypeId",
                table: "ProductDonutionToShelters",
                column: "ShelterInfoTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UserId",
                table: "Products",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShelterInfoType_UserId",
                table: "ShelterInfoType",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductDonations_ProductId",
                table: "UserProductDonations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductDonations_UserId",
                table: "UserProductDonations",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDonutionToShelters");

            migrationBuilder.DropTable(
                name: "UserProductDonations");

            migrationBuilder.DropTable(
                name: "ShelterInfoType");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
