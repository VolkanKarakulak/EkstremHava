using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EkstremHava.Data.Migrations
{
    /// <inheritdoc />
    public partial class QuantityCorrection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantitiy",
                table: "OrderDetails",
                newName: "Quantity");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderDetails",
                newName: "Quantitiy");
        }
    }
}
