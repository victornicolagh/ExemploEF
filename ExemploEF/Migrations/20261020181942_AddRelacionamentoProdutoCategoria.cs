using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExemploEF.Migrations
{
    /// <inheritdoc />
    public partial class AddRelacionamentoProdutoCategoria : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoriaId",
                table: "tbProdutos",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_tbProdutos_CategoriaId",
                table: "tbProdutos",
                column: "CategoriaId");

            migrationBuilder.AddForeignKey(
                name: "FK_tbProdutos_tbCategorias_CategoriaId",
                table: "tbProdutos",
                column: "CategoriaId",
                principalTable: "tbCategorias",
                principalColumn: "CategoriaId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tbProdutos_tbCategorias_CategoriaId",
                table: "tbProdutos");

            migrationBuilder.DropIndex(
                name: "IX_tbProdutos_CategoriaId",
                table: "tbProdutos");

            migrationBuilder.DropColumn(
                name: "CategoriaId",
                table: "tbProdutos");
        }
    }
}
