using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ST10028058_CLDV6212_POE_Final.Data.Migrations
{
    /// <inheritdoc />
    public partial class addedordersupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_ProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "Orders",
                newName: "Product_ID");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Orders",
                newName: "Order_Id");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "Order_Date");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Orders",
                newName: "Order_Address");

            migrationBuilder.AlterColumn<int>(
                name: "Order_Id",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CustomerName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Order_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CustomerName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Product_ID",
                table: "Orders",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "Order_Date",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "Order_Address",
                table: "Orders",
                newName: "FirstName");

            migrationBuilder.RenameColumn(
                name: "Order_Id",
                table: "Orders",
                newName: "ProductId");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Orders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "Orders",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProductId",
                table: "Orders",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Products_ProductId",
                table: "Orders",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
