using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanInv.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Corrigindoconfigurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proventos_Posicoes_PosicaoId1",
                table: "Proventos");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Corretoras_CorretoraId1",
                table: "Transacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Posicoes_PosicaoId1",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_CorretoraId1",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Transacoes_PosicaoId1",
                table: "Transacoes");

            migrationBuilder.DropIndex(
                name: "IX_Proventos_PosicaoId1",
                table: "Proventos");

            migrationBuilder.DropColumn(
                name: "CorretoraId1",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "PosicaoId1",
                table: "Transacoes");

            migrationBuilder.DropColumn(
                name: "PosicaoId1",
                table: "Proventos");

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "Usuarios",
                type: "bit",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Ativa",
                table: "Posicoes",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_Cpf",
                table: "Usuarios",
                column: "Cpf",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Usuarios_Cpf",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "Usuarios");

            migrationBuilder.AddColumn<int>(
                name: "CorretoraId1",
                table: "Transacoes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PosicaoId1",
                table: "Transacoes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PosicaoId1",
                table: "Proventos",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Ativa",
                table: "Posicoes",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_CorretoraId1",
                table: "Transacoes",
                column: "CorretoraId1");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_PosicaoId1",
                table: "Transacoes",
                column: "PosicaoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Proventos_PosicaoId1",
                table: "Proventos",
                column: "PosicaoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Proventos_Posicoes_PosicaoId1",
                table: "Proventos",
                column: "PosicaoId1",
                principalTable: "Posicoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Corretoras_CorretoraId1",
                table: "Transacoes",
                column: "CorretoraId1",
                principalTable: "Corretoras",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Posicoes_PosicaoId1",
                table: "Transacoes",
                column: "PosicaoId1",
                principalTable: "Posicoes",
                principalColumn: "Id");
        }
    }
}
