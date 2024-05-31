using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BreeceWorks.Shared.Migrations.CommunicationDb
{
    /// <inheritdoc />
    public partial class addActiveFlagToPhoneNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CompanyPhoneNumbers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CompanyPhoneNumbers");
        }
    }
}
