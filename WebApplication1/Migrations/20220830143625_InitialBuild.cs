using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RAWFIM.Migrations
{
    public partial class InitialBuild : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marques",
                columns: table => new
                {
                    Id_marque = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom_marque = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marques", x => x.Id_marque);
                });

            migrationBuilder.CreateTable(
                name: "Type_Machines",
                columns: table => new
                {
                    Id_type_machine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description_type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Type_Machines", x => x.Id_type_machine);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Materiels",
                columns: table => new
                {
                    Id_machine = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_type_machine = table.Column<int>(type: "int", nullable: true),
                    Num_marché = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_marque = table.Column<int>(type: "int", nullable: true),
                    Date_reception = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date_fin_garantie = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description_machine = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantite_stock = table.Column<int>(type: "int", nullable: false),
                    Nom_machine = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materiels", x => x.Id_machine);
                    table.ForeignKey(
                        name: "FK_Materiels_Marques_Id_marque",
                        column: x => x.Id_marque,
                        principalTable: "Marques",
                        principalColumn: "Id_marque");
                    table.ForeignKey(
                        name: "FK_Materiels_Type_Machines_Id_type_machine",
                        column: x => x.Id_type_machine,
                        principalTable: "Type_Machines",
                        principalColumn: "Id_type_machine");
                });

            migrationBuilder.CreateTable(
                name: "Affectation_Materiels",
                columns: table => new
                {
                    Id_affectation = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_machine = table.Column<int>(type: "int", nullable: false),
                    Id_demandeur = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date_affectation = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_validateur = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Id_demande = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Affectation_Materiels", x => x.Id_affectation);
                    table.ForeignKey(
                        name: "FK_Affectation_Materiels_Materiels_Id_machine",
                        column: x => x.Id_machine,
                        principalTable: "Materiels",
                        principalColumn: "Id_machine",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Matricule_agent = table.Column<int>(type: "int", nullable: false),
                    Prenom_agent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nom_agent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id_division = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "Demandes",
                columns: table => new
                {
                    Id_demande = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date_demande = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Id_type_machine = table.Column<int>(type: "int", nullable: false),
                    Id_agent = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Desctription_demande = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demandes", x => x.Id_demande);
                    table.ForeignKey(
                        name: "FK_Demandes_AspNetUsers_Id_agent",
                        column: x => x.Id_agent,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Demandes_Type_Machines_Id_type_machine",
                        column: x => x.Id_type_machine,
                        principalTable: "Type_Machines",
                        principalColumn: "Id_type_machine",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Divisions",
                columns: table => new
                {
                    Id_division = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom_division = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_chef = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Divisions", x => x.Id_division);
                    table.ForeignKey(
                        name: "FK_Divisions_AspNetUsers_Id_chef",
                        column: x => x.Id_chef,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Transfert_Materiels",
                columns: table => new
                {
                    Id_emprunt = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_affectation = table.Column<int>(type: "int", nullable: false),
                    Matricule_agent_emprunteur = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Date_emprunt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Matricule_agent_empruntant = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfert_Materiels", x => x.Id_emprunt);
                    table.ForeignKey(
                        name: "FK_Transfert_Materiels_Affectation_Materiels_Id_affectation",
                        column: x => x.Id_affectation,
                        principalTable: "Affectation_Materiels",
                        principalColumn: "Id_affectation",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfert_Materiels_AspNetUsers_Matricule_agent_empruntant",
                        column: x => x.Matricule_agent_empruntant,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transfert_Materiels_AspNetUsers_Matricule_agent_emprunteur",
                        column: x => x.Matricule_agent_emprunteur,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Validations_admin",
                columns: table => new
                {
                    Id_demande = table.Column<int>(type: "int", nullable: false),
                    Id_validateur = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status_validation = table.Column<bool>(type: "bit", nullable: true),
                    Date_decision = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Motif_refus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validations_admin", x => x.Id_demande);
                    table.ForeignKey(
                        name: "FK_Validations_admin_AspNetUsers_Id_validateur",
                        column: x => x.Id_validateur,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Validations_admin_Demandes_Id_demande",
                        column: x => x.Id_demande,
                        principalTable: "Demandes",
                        principalColumn: "Id_demande",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Validations_chef",
                columns: table => new
                {
                    Id_demande = table.Column<int>(type: "int", nullable: false),
                    Id_validateur = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Status_validation = table.Column<bool>(type: "bit", nullable: true),
                    Date_decision = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Motif_refus = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validations_chef", x => x.Id_demande);
                    table.ForeignKey(
                        name: "FK_Validations_chef_AspNetUsers_Id_validateur",
                        column: x => x.Id_validateur,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Validations_chef_Demandes_Id_demande",
                        column: x => x.Id_demande,
                        principalTable: "Demandes",
                        principalColumn: "Id_demande",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Affectation_Materiels_Id_demande",
                table: "Affectation_Materiels",
                column: "Id_demande");

            migrationBuilder.CreateIndex(
                name: "IX_Affectation_Materiels_Id_demandeur",
                table: "Affectation_Materiels",
                column: "Id_demandeur");

            migrationBuilder.CreateIndex(
                name: "IX_Affectation_Materiels_Id_machine",
                table: "Affectation_Materiels",
                column: "Id_machine");

            migrationBuilder.CreateIndex(
                name: "IX_Affectation_Materiels_Id_validateur",
                table: "Affectation_Materiels",
                column: "Id_validateur");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

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
                name: "IX_AspNetUsers_Id_division",
                table: "AspNetUsers",
                column: "Id_division");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Matricule_agent",
                table: "AspNetUsers",
                column: "Matricule_agent",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Demandes_Id_agent",
                table: "Demandes",
                column: "Id_agent");

            migrationBuilder.CreateIndex(
                name: "IX_Demandes_Id_type_machine",
                table: "Demandes",
                column: "Id_type_machine");

            migrationBuilder.CreateIndex(
                name: "IX_Divisions_Id_chef",
                table: "Divisions",
                column: "Id_chef");

            migrationBuilder.CreateIndex(
                name: "IX_Materiels_Id_marque",
                table: "Materiels",
                column: "Id_marque");

            migrationBuilder.CreateIndex(
                name: "IX_Materiels_Id_type_machine",
                table: "Materiels",
                column: "Id_type_machine");

            migrationBuilder.CreateIndex(
                name: "IX_Transfert_Materiels_Id_affectation",
                table: "Transfert_Materiels",
                column: "Id_affectation");

            migrationBuilder.CreateIndex(
                name: "IX_Transfert_Materiels_Matricule_agent_empruntant",
                table: "Transfert_Materiels",
                column: "Matricule_agent_empruntant");

            migrationBuilder.CreateIndex(
                name: "IX_Transfert_Materiels_Matricule_agent_emprunteur",
                table: "Transfert_Materiels",
                column: "Matricule_agent_emprunteur");

            migrationBuilder.CreateIndex(
                name: "IX_Validations_admin_Id_validateur",
                table: "Validations_admin",
                column: "Id_validateur");

            migrationBuilder.CreateIndex(
                name: "IX_Validations_chef_Id_validateur",
                table: "Validations_chef",
                column: "Id_validateur");

            migrationBuilder.AddForeignKey(
                name: "FK_Affectation_Materiels_AspNetUsers_Id_demandeur",
                table: "Affectation_Materiels",
                column: "Id_demandeur",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Affectation_Materiels_AspNetUsers_Id_validateur",
                table: "Affectation_Materiels",
                column: "Id_validateur",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Affectation_Materiels_Demandes_Id_demande",
                table: "Affectation_Materiels",
                column: "Id_demande",
                principalTable: "Demandes",
                principalColumn: "Id_demande");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Divisions_Id_division",
                table: "AspNetUsers",
                column: "Id_division",
                principalTable: "Divisions",
                principalColumn: "Id_division");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Divisions_AspNetUsers_Id_chef",
                table: "Divisions");

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
                name: "Transfert_Materiels");

            migrationBuilder.DropTable(
                name: "Validations_admin");

            migrationBuilder.DropTable(
                name: "Validations_chef");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Affectation_Materiels");

            migrationBuilder.DropTable(
                name: "Demandes");

            migrationBuilder.DropTable(
                name: "Materiels");

            migrationBuilder.DropTable(
                name: "Marques");

            migrationBuilder.DropTable(
                name: "Type_Machines");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Divisions");
        }
    }
}
