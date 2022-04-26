using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DigiTrade.Invoicing.Migrations
{
    public partial class AddProductCust : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Brands",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand_Name = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cust_Name = table.Column<string>(maxLength: 128, nullable: false),
                    Cust_Phone = table.Column<string>(maxLength: 10, nullable: false),
                    Cust_Email = table.Column<string>(maxLength: 128, nullable: false),
                    Cust_Address = table.Column<string>(maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(maxLength: 128, nullable: false),
                    description = table.Column<string>(maxLength: 255, nullable: false),
                    sale_price = table.Column<long>(nullable: false),
                    cur_stock = table.Column<long>(nullable: false),
                    tax = table.Column<byte>(nullable: true),
                    BrandId = table.Column<int>(nullable: true),
                    processor = table.Column<string>(maxLength: 128, nullable: false),
                    Ram = table.Column<short>(nullable: false),
                    Rom = table.Column<short>(nullable: false),
                    primary_cam = table.Column<byte>(nullable: false),
                    front_cam = table.Column<byte>(nullable: false),
                    battery = table.Column<short>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SalesInvoices",
                columns: table => new
                {
                    Invoice_num = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Invoice_Date = table.Column<DateTime>(nullable: false),
                    Cust_ID = table.Column<int>(nullable: false),
                    Product_ID = table.Column<int>(nullable: false),
                    Qty = table.Column<int>(nullable: false),
                    Rate = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesInvoices", x => x.Invoice_num);
                    table.ForeignKey(
                        name: "FK_SalesInvoices_Customers_Cust_ID",
                        column: x => x.Cust_ID,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SalesInvoices_Products_Product_ID",
                        column: x => x.Product_ID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_BrandId",
                table: "Products",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_Cust_ID",
                table: "SalesInvoices",
                column: "Cust_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SalesInvoices_Product_ID",
                table: "SalesInvoices",
                column: "Product_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesInvoices");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Brands");
        }
    }
}
