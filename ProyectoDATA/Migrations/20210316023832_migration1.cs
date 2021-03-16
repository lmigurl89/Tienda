using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;
using MySql.EntityFrameworkCore.Metadata;
using MySQLValueGenerationStrategy = MySql.Data.EntityFrameworkCore.Metadata.MySQLValueGenerationStrategy;

namespace ProyectoDATA.Migrations
{
    public partial class migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Discriminator = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(200) CHARACTER SET utf8mb4", maxLength: 200, nullable: true),
                    Name = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(100) CHARACTER SET utf8mb4", maxLength: 100, nullable: false),
                    Apellidos = table.Column<string>(type: "varchar(100) CHARACTER SET utf8mb4", maxLength: 100, nullable: false),
                    UserName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "varchar(256) CHARACTER SET utf8mb4", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Nombre = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Descripcion = table.Column<string>(type: "varchar(100) CHARACTER SET utf8mb4", maxLength: 100, nullable: true),
                    Slug = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    Precio = table.Column<double>(type: "double", nullable: false),
                    Cantidad = table.Column<int>(type: "int", maxLength: 100, nullable: false),
                    CreadoPor = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    FechaCreado = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trazas",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    UserName = table.Column<string>(type: "varchar(100) CHARACTER SET utf8mb4", maxLength: 100, nullable: false),
                    NombreAccion = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: true),
                    NombrePC = table.Column<string>(type: "varchar(100) CHARACTER SET utf8mb4", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false),
                    ObjetoCreado = table.Column<string>(type: "json", nullable: true),
                    ObjetoAntesModificar = table.Column<string>(type: "json", nullable: true),
                    ObjetoModificado = table.Column<string>(type: "json", nullable: true),
                    ObjetoEliminado = table.Column<string>(type: "json", nullable: true),
                    FechaCreado = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trazas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ClaimType = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    ClaimValue = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ProviderKey = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    RoleId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Discriminator = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    LoginProvider = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Name = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    Value = table.Column<string>(type: "longtext CHARACTER SET utf8mb4", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ordenes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(50)", nullable: false),
                    Fecha = table.Column<DateTime>(type: "Date", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "varchar(255) CHARACTER SET utf8mb4", nullable: false),
                    ProductoId = table.Column<string>(type: "varchar(50)", nullable: false),
                    FechaCreado = table.Column<DateTime>(type: "datetime(6)", nullable: false, defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ordenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ordenes_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ordenes_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Descripcion", "Discriminator", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "91e0f2a3-3a4c-46e4-83a6-94356cd46725", "dc0aeb28-2e47-483d-9a1a-1396f6aa374d", "Administradores de la tienda", "Rol", "Administrador", null },
                    { "f56dc294-76e6-4277-9685-b4ca524f61d8", "b6ef9fe3-415b-4688-9fec-64a46c0a20e9", "Vendedor de la tienda", "Rol", "Vendedor", null },
                    { "49efd009-ced5-4fb9-868d-cf365a34765f", "b1327f26-fce3-4e93-bc2a-8edd332bdc78", "Cliente de la tienda", "Rol", "Cliente", null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Apellidos", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "Nombre", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0", 0, "Administrador", "44c8c6e9-f046-4af2-92ba-eb91b232b110", "administrador@tienda.com", false, true, null, "Usuario", "ADMINISTRADOR@TIENDA.COM", "ADMINISTRADOR", "AQAAAAEAACcQAAAAEAkXw5Al5CMhhZiQOvGxpO4d5hOXE4YdHbHv7NvP7wXhuwIPQkBLgjM+4zHCkBqaPA==", null, false, "NLN3QCO657I7UVCHLVZ2AMKFMK37JCIY", false, "administrador" },
                    { "549889bf-fec3-4403-a536-133cd8cdeac5", 0, "Vendedor", "44c8c6e9-f046-4af2-92ba-eb91b232b110", "vendedor@tienda.com", false, true, null, "Usuario", "VENDEDOR@TIENDA.COM", "VENDEDOR", "AQAAAAEAACcQAAAAEAkXw5Al5CMhhZiQOvGxpO4d5hOXE4YdHbHv7NvP7wXhuwIPQkBLgjM+4zHCkBqaPA==", null, false, "NLN3QCO657I7UVCHLVZ2AMKFMK37JCIY", false, "vendedor" },
                    { "787d0bc9-048c-4e9f-b6c3-df267442a3aa", 0, "Cliente", "44c8c6e9-f046-4af2-92ba-eb91b232b110", "cliente@tienda.com", false, true, null, "Usuario", "CLIENTE@TIENDA.COM", "CLIENTE", "AQAAAAEAACcQAAAAEAkXw5Al5CMhhZiQOvGxpO4d5hOXE4YdHbHv7NvP7wXhuwIPQkBLgjM+4zHCkBqaPA==", null, false, "NLN3QCO657I7UVCHLVZ2AMKFMK37JCIY", false, "cliente" }
                });

            migrationBuilder.InsertData(
                table: "Productos",
                columns: new[] { "Id", "Cantidad", "CreadoPor", "Descripcion", "FechaCreado", "Nombre", "Precio", "Slug" },
                values: new object[,]
                {
                    { "5d1a6250-4bdc-46c0-8fb7-b965dad45d79", 220, "vendedor", "MotherBoard", new DateTime(2021, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Motherboard", 20.989999999999998, null },
                    { "4b5a5caa-8858-4faa-9e86-c5023d1119cd", 614, "vendedor", "Mouse", new DateTime(2021, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mouse", 52.0, null },
                    { "4a7b536f-7777-4af9-bec8-e9a9530d630d", 20, "vendedor", "Monitor", new DateTime(2021, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Monitor", 65.650000000000006, null },
                    { "1df535e8-9f12-4b9a-a780-f32a21c123a9", 220, "vendedor", "RAM", new DateTime(2021, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Ram", 20.0, null },
                    { "6c875378-7bf3-4ca3-b056-bc360d65ac8c", 220, "vendedor", "KeyBoard", new DateTime(2021, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Keyboard", 10.99, null }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Discriminator" },
                values: new object[,]
                {
                    { "91e0f2a3-3a4c-46e4-83a6-94356cd46725", "f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0", "Rol_Usuarios" },
                    { "f56dc294-76e6-4277-9685-b4ca524f61d8", "549889bf-fec3-4403-a536-133cd8cdeac5", "Rol_Usuarios" },
                    { "49efd009-ced5-4fb9-868d-cf365a34765f", "787d0bc9-048c-4e9f-b6c3-df267442a3aa", "Rol_Usuarios" }
                });

            migrationBuilder.InsertData(
                table: "Ordenes",
                columns: new[] { "Id", "Cantidad", "Estado", "Fecha", "ProductoId", "UsuarioId" },
                values: new object[,]
                {
                    { "40ea0e5d-c412-45f7-94f6-3e0051350f96", 10, 0, new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "1df535e8-9f12-4b9a-a780-f32a21c123a9", "f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0" },
                    { "72690b9a-0afb-4b3c-98c8-26809816bd7d", 5, 0, new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "1df535e8-9f12-4b9a-a780-f32a21c123a9", "f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0" },
                    { "f2f4a187-3f67-4eb3-8bc8-b1550bd9ffe8", 2, 2, new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "1df535e8-9f12-4b9a-a780-f32a21c123a9", "f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0" },
                    { "4cd7a29e-1e21-4697-b3c9-b7f8ea30a72d", 20, 1, new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "6c875378-7bf3-4ca3-b056-bc360d65ac8c", "f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0" },
                    { "0d4d759f-58f6-4dd2-a890-079f2ffef68c", 20, 1, new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "6c875378-7bf3-4ca3-b056-bc360d65ac8c", "f6a8c8eb-309c-4ca7-91e1-46aa0ff16bc0" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE",
                table: "Ordenes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_Fecha",
                table: "Ordenes",
                column: "Fecha");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_ProductoId",
                table: "Ordenes",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Ordenes_UsuarioId",
                table: "Ordenes",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE",
                table: "Productos",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "Nombre_UNIQUE",
                table: "Productos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "FK_NombreAccion_Trazas_idx",
                table: "Trazas",
                column: "NombreAccion");

            migrationBuilder.CreateIndex(
                name: "Id_UNIQUE",
                table: "Trazas",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Ordenes");

            migrationBuilder.DropTable(
                name: "Trazas");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Productos");
        }
    }
}
