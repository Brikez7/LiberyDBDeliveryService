using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiberyDBDeliveryService.Migrations
{
    public partial class AddIdWorkShift : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    idTelegram = table.Column<long>(type: "bigint", nullable: false),
                    post = table.Column<short>(type: "smallint", nullable: false),
                    loginTelegram = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    life = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => new { x.idTelegram, x.post, x.name });
                    table.UniqueConstraint("AK_Account_idTelegram", x => x.idTelegram);
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    idOrder = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dateOrder = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    statusOrder = table.Column<short>(type: "smallint", nullable: false),
                    typeMovement = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    product = table.Column<string>(type: "nvarchar(max)", nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    weight = table.Column<double>(type: "float", nullable: false),
                    addresWarehouse = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    deliveryTime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    phoneSenders = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    phoneClient = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    addresClient = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    fenceTime = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    deposit = table.Column<double>(type: "float", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false),
                    describe = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, collation: "SQL_Latin1_General_CP1_CI_AS"),
                    idTelegramDeliver = table.Column<long>(type: "bigint", nullable: true),
                    idTelegramShop = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Id", x => x.idOrder);
                    table.ForeignKey(
                        name: "FK_Order_idTelegramEmploye",
                        column: x => x.idTelegramDeliver,
                        principalTable: "Account",
                        principalColumn: "idTelegram");
                    table.ForeignKey(
                        name: "FK_Order_idTelegramShop",
                        column: x => x.idTelegramShop,
                        principalTable: "Account",
                        principalColumn: "idTelegram");
                });

            migrationBuilder.CreateTable(
                name: "WorkShift",
                columns: table => new
                {
                    IdWorkShift = table.Column<long>(type: "bigint", nullable: false)
                                            .Annotation("SqlServer:Identity", "1, 1"),
                    idTelegramEmploye = table.Column<long>(type: "bigint", nullable: false),
                    date = table.Column<DateTime>(type: "datetime", nullable: false),
                    status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkShift", x => x.IdWorkShift ); 
                    table.ForeignKey(
                        name: "FK_WorkShift_idTelegramEmploye",
                        column: x => x.idTelegramEmploye,
                        principalTable: "Account",
                        principalColumn: "idTelegram");
                });

            migrationBuilder.CreateIndex(
                name: "KEY_Account_idTelegram",
                table: "Account",
                column: "idTelegram",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_idTelegramDeliver",
                table: "Order",
                column: "idTelegramDeliver");

            migrationBuilder.CreateIndex(
                name: "IX_Order_idTelegramShop",
                table: "Order",
                column: "idTelegramShop");

            migrationBuilder.CreateIndex(
                name: "IDX_WorkShift",
                table: "WorkShift",
                column: "idTelegramEmploye");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "WorkShift");

            migrationBuilder.DropTable(
                name: "Account");
        }
    }
}
