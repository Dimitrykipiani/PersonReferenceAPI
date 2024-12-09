using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TBC.PersonReference.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedPersonRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonRelations_Persons_PersonId",
                table: "PersonRelations");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "PersonRelations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRelations_Persons_PersonId",
                table: "PersonRelations",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PersonRelations_Persons_PersonId",
                table: "PersonRelations");

            migrationBuilder.AlterColumn<int>(
                name: "PersonId",
                table: "PersonRelations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PersonRelations_Persons_PersonId",
                table: "PersonRelations",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
