using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Train_Management_App.Migrations
{
    /// <inheritdoc />
    public partial class AddUniqueIndexToUniqueNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UniqueNumber",
                table: "TrainComponents",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TrainComponents_UniqueNumber",
                table: "TrainComponents",
                column: "UniqueNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TrainComponents_UniqueNumber",
                table: "TrainComponents");

            migrationBuilder.AlterColumn<string>(
                name: "UniqueNumber",
                table: "TrainComponents",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
