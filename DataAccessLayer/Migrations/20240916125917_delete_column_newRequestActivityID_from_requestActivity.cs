using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class delete_column_newRequestActivityID_from_requestActivity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestActivities_Activities_NewActivityStatusID",
                table: "RequestActivities");

            migrationBuilder.DropIndex(
                name: "IX_RequestActivities_NewActivityStatusID",
                table: "RequestActivities");

            migrationBuilder.DropColumn(
                name: "NewActivityStatusID",
                table: "RequestActivities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NewActivityStatusID",
                table: "RequestActivities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestActivities_NewActivityStatusID",
                table: "RequestActivities",
                column: "NewActivityStatusID");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestActivities_Activities_NewActivityStatusID",
                table: "RequestActivities",
                column: "NewActivityStatusID",
                principalTable: "Activities",
                principalColumn: "ActivityID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
