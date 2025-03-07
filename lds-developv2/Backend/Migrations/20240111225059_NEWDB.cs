using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Friendly_.Migrations
{
    /// <inheritdoc />
    public partial class NEWDB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MetodoPagamento",
                columns: table => new
                {
                    metodoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    descricao = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetodoPagamento", x => x.metodoID);
                });

            migrationBuilder.CreateTable(
                name: "Recintos",
                columns: table => new
                {
                    recintoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    concelho = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    latitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    longitude = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    modalidade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    preco = table.Column<float>(type: "real", nullable: false),
                    contacto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    imagem = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recintos", x => x.recintoID);
                });

            migrationBuilder.CreateTable(
                name: "UserContacts",
                columns: table => new
                {
                    userContactID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContacts", x => x.userContactID);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    userID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userContactID = table.Column<int>(type: "int", nullable: true),
                    nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    morada = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    nascimento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    genero = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: false),
                    telemovel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    nif = table.Column<string>(type: "nvarchar(9)", maxLength: 9, nullable: false),
                    pontos = table.Column<int>(type: "int", nullable: false),
                    isAccountActivated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.userID);
                    table.ForeignKey(
                        name: "FK_User_UserContacts_userContactID",
                        column: x => x.userContactID,
                        principalTable: "UserContacts",
                        principalColumn: "userContactID");
                });

            migrationBuilder.CreateTable(
                name: "Pagamento",
                columns: table => new
                {
                    pagamentoID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    metodoID = table.Column<int>(type: "int", nullable: false),
                    total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamento", x => x.pagamentoID);
                    table.ForeignKey(
                        name: "FK_Pagamento_MetodoPagamento_metodoID",
                        column: x => x.metodoID,
                        principalTable: "MetodoPagamento",
                        principalColumn: "metodoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Pagamento_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    ratingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    userID = table.Column<int>(type: "int", nullable: false),
                    recintoID = table.Column<int>(type: "int", nullable: false),
                    username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    rating = table.Column<int>(type: "int", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.ratingID);
                    table.ForeignKey(
                        name: "FK_Ratings_Recintos_recintoID",
                        column: x => x.recintoID,
                        principalTable: "Recintos",
                        principalColumn: "recintoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reserva",
                columns: table => new
                {
                    reservaID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    recintoID = table.Column<int>(type: "int", nullable: false),
                    userID = table.Column<int>(type: "int", nullable: false),
                    userContactID = table.Column<int>(type: "int", nullable: true),
                    pagamentoID = table.Column<int>(type: "int", nullable: true),
                    dataInicial = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    horaReserva = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    horaJogo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    horaCancelamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    preco = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    estado = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reserva", x => x.reservaID);
                    table.ForeignKey(
                        name: "FK_Reserva_Pagamento_pagamentoID",
                        column: x => x.pagamentoID,
                        principalTable: "Pagamento",
                        principalColumn: "pagamentoID");
                    table.ForeignKey(
                        name: "FK_Reserva_Recintos_recintoID",
                        column: x => x.recintoID,
                        principalTable: "Recintos",
                        principalColumn: "recintoID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reserva_UserContacts_userContactID",
                        column: x => x.userContactID,
                        principalTable: "UserContacts",
                        principalColumn: "userContactID");
                    table.ForeignKey(
                        name: "FK_Reserva_User_userID",
                        column: x => x.userID,
                        principalTable: "User",
                        principalColumn: "userID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_metodoID",
                table: "Pagamento",
                column: "metodoID");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamento_userID",
                table: "Pagamento",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_recintoID",
                table: "Ratings",
                column: "recintoID");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_userID",
                table: "Ratings",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_pagamentoID",
                table: "Reserva",
                column: "pagamentoID");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_recintoID",
                table: "Reserva",
                column: "recintoID");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_userContactID",
                table: "Reserva",
                column: "userContactID");

            migrationBuilder.CreateIndex(
                name: "IX_Reserva_userID",
                table: "Reserva",
                column: "userID");

            migrationBuilder.CreateIndex(
                name: "IX_User_userContactID",
                table: "User",
                column: "userContactID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Reserva");

            migrationBuilder.DropTable(
                name: "Pagamento");

            migrationBuilder.DropTable(
                name: "Recintos");

            migrationBuilder.DropTable(
                name: "MetodoPagamento");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserContacts");
        }
    }
}
