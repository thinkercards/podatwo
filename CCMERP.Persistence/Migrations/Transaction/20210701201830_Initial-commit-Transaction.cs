using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace CCMERP.Persistence.Migrations.Transaction
{
    public partial class InitialcommitTransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    VatID = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    ShippingAddress1 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    ShippingAddress2 = table.Column<string>(type: "varchar(100)", maxLength: 100),
                    ShippingCity = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ShippingState = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ShippingCountry = table.Column<int>(type: "int", nullable: false),
                    ShippingZipCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    BillingAddress1 = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    BillingAddress2 = table.Column<string>(type: "varchar(100)", maxLength: 100),
                    BillingCity = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    BillingState = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    BillingCountry = table.Column<int>(type: "int", nullable: false),
                    BillingZipCode = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    ContactPerson = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ContactPosition = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ContactNumber = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    ContactEmail = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedByUser = table.Column<int>(type: "int", nullable: false),
                    CreatedByProgram = table.Column<string>(type: "varchar(50)", maxLength: 50),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModifiedBy = table.Column<int>(type: "int", nullable: false),
                    LastModifiedByProgram = table.Column<string>(type: "varchar(50)", maxLength: 50),
                    ExternalReference = table.Column<string>(type: "varchar(50)", maxLength: 50),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerID);
                });

            migrationBuilder.CreateTable(
                name: "organizationCustomerMappings",
                columns: table => new
                {
                    Org_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    User_ID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizationCustomerMappings", x => x.Org_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "organizationCustomerMappings");
        }
    }
}
