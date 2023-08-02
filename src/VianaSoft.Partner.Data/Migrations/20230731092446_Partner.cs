using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VianaSoft.Partner.Data.Migrations
{
    /// <inheritdoc />
    public partial class Partner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Partners",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Document = table.Column<string>(type: "varchar(14)", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    Description = table.Column<string>(type: "varchar(500)", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    IsExclude = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    UpdateBy = table.Column<string>(type: "varchar(255)", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2(7)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Partners", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Partners");
        }
    }
}
