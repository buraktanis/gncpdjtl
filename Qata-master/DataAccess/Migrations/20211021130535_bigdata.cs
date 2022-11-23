using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class bigdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Adi",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Atanan",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Il",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Musteritipi",
                table: "BigDatas");

            migrationBuilder.AlterColumn<string>(
                name: "TcIdentityNumber",
                table: "Customers",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TaxAdministration",
                table: "Customers",
                maxLength: 10,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "BigDatas",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "CallPermission",
                table: "BigDatas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CariKodu",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConfirmationType",
                table: "BigDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "BigDatas",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CustomerClass",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BigDatas",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email1",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email2",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "EmailPermission",
                table: "BigDatas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Facebook",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KindOf",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LinkedIn",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LogoId",
                table: "BigDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BigDatas",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PersonInCharge",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone1",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone2",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Region",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Representative",
                table: "BigDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Slsman",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SmsPermission",
                table: "BigDatas",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "BigDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TaxAdministration",
                table: "BigDatas",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxNumber",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TcIdentityNumber",
                table: "BigDatas",
                maxLength: 11,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Town",
                table: "BigDatas",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Twitter",
                table: "BigDatas",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "BigDatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Youtube",
                table: "BigDatas",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "CallPermission",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "CariKodu",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "City",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "ConfirmationType",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "CustomerClass",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Email1",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Email2",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "EmailPermission",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Facebook",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "KindOf",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "LinkedIn",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "LogoId",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "PersonInCharge",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Phone1",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Phone2",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Region",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Representative",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Slsman",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "SmsPermission",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "TaxAdministration",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "TaxNumber",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "TcIdentityNumber",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Town",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Twitter",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "BigDatas");

            migrationBuilder.DropColumn(
                name: "Youtube",
                table: "BigDatas");

            migrationBuilder.AlterColumn<string>(
                name: "TcIdentityNumber",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TaxAdministration",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 10,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Adi",
                table: "BigDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Atanan",
                table: "BigDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Il",
                table: "BigDatas",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Musteritipi",
                table: "BigDatas",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
