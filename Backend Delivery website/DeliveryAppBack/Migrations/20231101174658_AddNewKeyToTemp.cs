using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeliveryAppBack.Migrations
{
    /// <inheritdoc />
    public partial class AddNewKeyToTemp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TempOrderhistory",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TempOrderhistory",
                table: "TempOrderhistory",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TempOrderhistory",
                table: "TempOrderhistory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TempOrderhistory");
        }
    }
}
