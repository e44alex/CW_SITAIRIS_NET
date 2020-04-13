using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CW_SITAIRIS.Migrations
{
    public partial class secondary : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "idCar",
                table: "Warehouses");

            migrationBuilder.AddColumn<int>(
                name: "idWarehouse",
                table: "Warehouses",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "picture",
                table: "Cars",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses",
                column: "idWarehouse");

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    idCar = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.idCar);
                });

            migrationBuilder.CreateTable(
                name: "TestDrives",
                columns: table => new
                {
                    idDrive = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userId = table.Column<int>(nullable: false),
                    carId = table.Column<int>(nullable: false),
                    date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDrives", x => x.idDrive);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "TestDrives");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "idWarehouse",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "picture",
                table: "Cars");

            migrationBuilder.AddColumn<int>(
                name: "idCar",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Warehouses",
                table: "Warehouses",
                column: "idCar");
        }
    }
}
