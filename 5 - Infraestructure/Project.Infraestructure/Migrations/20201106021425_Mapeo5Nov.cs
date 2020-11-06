using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Infraestructure.Migrations
{
    public partial class Mapeo5Nov : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(unicode: false, maxLength: 30, nullable: false),
                    Password = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(unicode: false, maxLength: 50, nullable: false),
                    Role = table.Column<string>(maxLength: 15, nullable: false),
                    Telephone = table.Column<string>(unicode: false, maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductRepairs",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    BrandId = table.Column<long>(nullable: false),
                    CategoryId = table.Column<long>(nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    CostPrice = table.Column<decimal>(nullable: true),
                    SalePrice = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRepairs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReparis_Brand",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReparis_Category",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Image = table.Column<byte[]>(nullable: false),
                    Code = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CategoryId = table.Column<long>(nullable: false),
                    BrandId = table.Column<long>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    CostPrice = table.Column<decimal>(nullable: false),
                    Aliquot = table.Column<decimal>(nullable: false),
                    Stock = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Brand",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Product_Category",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TechnicalServices",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    SerialNumber = table.Column<string>(maxLength: 200, nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    ProductRepairId = table.Column<long>(nullable: false),
                    Observations = table.Column<string>(maxLength: 9999999, nullable: false),
                    AccessoriesReceived = table.Column<string>(maxLength: 9999999, nullable: true),
                    EquipmentFailure = table.Column<string>(maxLength: 9999999, nullable: false),
                    DateReceived = table.Column<DateTime>(nullable: false),
                    ServiceStatus = table.Column<string>(maxLength: 15, nullable: false),
                    DateStatus = table.Column<DateTime>(nullable: false),
                    TotalInputs = table.Column<decimal>(nullable: false),
                    TotalLabor = table.Column<decimal>(nullable: false),
                    Total = table.Column<decimal>(nullable: false),
                    Diagnostic = table.Column<string>(maxLength: 9999999, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TechnicalServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TechnicalService_ProductRepair",
                        column: x => x.ProductRepairId,
                        principalTable: "ProductRepairs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TechnicalService_User",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductRepairs_BrandId",
                table: "ProductRepairs",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRepairs_CategoryId",
                table: "ProductRepairs",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalServices_ProductRepairId",
                table: "TechnicalServices",
                column: "ProductRepairId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalServices_UserId",
                table: "TechnicalServices",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "TechnicalServices");

            migrationBuilder.DropTable(
                name: "ProductRepairs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Brands");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
