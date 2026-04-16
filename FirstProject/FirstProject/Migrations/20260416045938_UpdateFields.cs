using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FirstProject.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "PersonDatas");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "PersonDatas",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<decimal>(
                name: "HeightInFeet",
                table: "PersonDatas",
                type: "decimal(3,2)",
                precision: 3,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "WeightInKg",
                table: "PersonDatas",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "PersonDatas");

            migrationBuilder.DropColumn(
                name: "HeightInFeet",
                table: "PersonDatas");

            migrationBuilder.DropColumn(
                name: "WeightInKg",
                table: "PersonDatas");

            migrationBuilder.AddColumn<decimal>(
                name: "Height",
                table: "PersonDatas",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
