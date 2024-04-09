using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AllupProjectMVC.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockCount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsPoster",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "BasketProducts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Banners");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Banners");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Sliders",
                newName: "SoftDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Products",
                newName: "SoftDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "ProductImages",
                newName: "SoftDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Features",
                newName: "SoftDeleted");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Categories",
                newName: "SoftDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Blogs",
                newName: "SoftDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Baskets",
                newName: "SoftDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "BasketProducts",
                newName: "SoftDeleted");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Banners",
                newName: "SoftDeleted");

            migrationBuilder.RenameColumn(
                name: "Fullname",
                table: "AspNetUsers",
                newName: "FullName");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Sliders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductImages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Blogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Banners",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Wishlists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wishlists_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WishlistProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WishlistId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SoftDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WishlistProducts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WishlistProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WishlistProducts_Wishlists_WishlistId",
                        column: x => x.WishlistId,
                        principalTable: "Wishlists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WishlistProducts_ProductId",
                table: "WishlistProducts",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_WishlistProducts_WishlistId",
                table: "WishlistProducts",
                column: "WishlistId");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_AppUserId",
                table: "Wishlists",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "WishlistProducts");

            migrationBuilder.DropTable(
                name: "Wishlists");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ProductImages");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Blogs");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Banners");

            migrationBuilder.RenameColumn(
                name: "SoftDeleted",
                table: "Sliders",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "SoftDeleted",
                table: "Products",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "SoftDeleted",
                table: "ProductImages",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "SoftDeleted",
                table: "Features",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "SoftDeleted",
                table: "Categories",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "SoftDeleted",
                table: "Blogs",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "SoftDeleted",
                table: "Baskets",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "SoftDeleted",
                table: "BasketProducts",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "SoftDeleted",
                table: "Banners",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "AspNetUsers",
                newName: "Fullname");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Sliders",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Sliders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "Products",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StockCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Products",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "ProductImages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPoster",
                table: "ProductImages",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "ProductImages",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Features",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Categories",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Categories",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Blogs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Blogs",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Blogs",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Baskets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "BasketProducts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Banners",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Banners",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_ProductImages_Products_ProductId",
                table: "ProductImages",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
