using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VianaSoft.Partner.Data.Migrations
{
    /// <inheritdoc />
    public partial class Partner2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", nullable: false),
                    BusinessEmail = table.Column<string>(type: "varchar(255)", nullable: true),
                    PersonalEmail = table.Column<string>(type: "varchar(255)", nullable: true),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    IsExclude = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    UpdateBy = table.Column<string>(type: "varchar(255)", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    PartnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contacts_Partners_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "Partners",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Phones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DDICode = table.Column<string>(type: "varchar(5)", nullable: false),
                    DDDCode = table.Column<string>(type: "varchar(5)", nullable: true),
                    Number = table.Column<string>(type: "varchar(20)", nullable: false),
                    IsCellPhone = table.Column<bool>(type: "bit", nullable: false),
                    IsWhatsapp = table.Column<bool>(type: "bit", nullable: false),
                    IsTelegram = table.Column<bool>(type: "bit", nullable: false),
                    IsEnable = table.Column<bool>(type: "bit", nullable: false),
                    IsExclude = table.Column<bool>(type: "bit", nullable: false),
                    CreateBy = table.Column<string>(type: "varchar(255)", nullable: false),
                    CreateAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    UpdateBy = table.Column<string>(type: "varchar(255)", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "datetime2(7)", nullable: true),
                    ContactId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Phones_Contacts_ContactId",
                        column: x => x.ContactId,
                        principalTable: "Contacts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_PartnerId",
                table: "Contacts",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Phones_ContactId",
                table: "Phones",
                column: "ContactId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Phones");

            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
