using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlanInv.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DBInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Ativos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Cnpj = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    CotacaoAtual = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ativos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Corretoras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corretoras", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Idade = table.Column<int>(type: "int", nullable: false),
                    Cpf = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    MetaDeAportesMensal = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Posicoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    AtivoId = table.Column<int>(type: "int", nullable: false),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    PrecoMedio = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    ValorInvestido = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DataPrimeiraCompra = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataUltimaTransacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativa = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posicoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posicoes_Ativos_AtivoId",
                        column: x => x.AtivoId,
                        principalTable: "Ativos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Posicoes_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Proventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosicaoId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataCom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    QuantidadeCotas = table.Column<int>(type: "int", nullable: false),
                    ValorPorCota = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    ValorBruto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Imposto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    Observacoes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PosicaoId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Proventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Proventos_Posicoes_PosicaoId",
                        column: x => x.PosicaoId,
                        principalTable: "Posicoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Proventos_Posicoes_PosicaoId1",
                        column: x => x.PosicaoId1,
                        principalTable: "Posicoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PosicaoId = table.Column<int>(type: "int", nullable: false),
                    CorretoraId = table.Column<int>(type: "int", nullable: false),
                    DataTransacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    QuantidadeCotas = table.Column<int>(type: "int", nullable: false),
                    PrecoUnitario = table.Column<decimal>(type: "decimal(18,6)", precision: 18, scale: 6, nullable: false),
                    Custos = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false, defaultValue: 0m),
                    Observacoes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CorretoraId1 = table.Column<int>(type: "int", nullable: true),
                    PosicaoId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transacoes_Corretoras_CorretoraId",
                        column: x => x.CorretoraId,
                        principalTable: "Corretoras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transacoes_Corretoras_CorretoraId1",
                        column: x => x.CorretoraId1,
                        principalTable: "Corretoras",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transacoes_Posicoes_PosicaoId",
                        column: x => x.PosicaoId,
                        principalTable: "Posicoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transacoes_Posicoes_PosicaoId1",
                        column: x => x.PosicaoId1,
                        principalTable: "Posicoes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ativos_Ticker",
                table: "Ativos",
                column: "Ticker",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Corretoras_Nome",
                table: "Corretoras",
                column: "Nome",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Posicoes_AtivoId",
                table: "Posicoes",
                column: "AtivoId");

            migrationBuilder.CreateIndex(
                name: "IX_Posicoes_UsuarioId",
                table: "Posicoes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Proventos_DataPagamento",
                table: "Proventos",
                column: "DataPagamento");

            migrationBuilder.CreateIndex(
                name: "IX_Proventos_PosicaoId",
                table: "Proventos",
                column: "PosicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Proventos_PosicaoId_DataCom",
                table: "Proventos",
                columns: new[] { "PosicaoId", "DataCom" });

            migrationBuilder.CreateIndex(
                name: "IX_Proventos_PosicaoId1",
                table: "Proventos",
                column: "PosicaoId1");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_CorretoraId",
                table: "Transacoes",
                column: "CorretoraId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_CorretoraId1",
                table: "Transacoes",
                column: "CorretoraId1");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_DataTransacao",
                table: "Transacoes",
                column: "DataTransacao");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_PosicaoId",
                table: "Transacoes",
                column: "PosicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_PosicaoId_DataTransacao",
                table: "Transacoes",
                columns: new[] { "PosicaoId", "DataTransacao" });

            migrationBuilder.CreateIndex(
                name: "IX_Transacoes_PosicaoId1",
                table: "Transacoes",
                column: "PosicaoId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Proventos");

            migrationBuilder.DropTable(
                name: "Transacoes");

            migrationBuilder.DropTable(
                name: "Corretoras");

            migrationBuilder.DropTable(
                name: "Posicoes");

            migrationBuilder.DropTable(
                name: "Ativos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
