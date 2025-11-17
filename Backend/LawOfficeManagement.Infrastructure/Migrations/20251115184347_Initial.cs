using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawOfficeManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cases");

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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
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
                name: "CaseTypes",
                schema: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientRoles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CourtTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Offices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfficeName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ManagerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    WebSitUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EstablishmentDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Opponents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpponentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "اسم الخصم"),
                    OpponentMobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "رقم جوال الخصم"),
                    OpponentAddress = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true, comment: "عنوان الخصم"),
                    Type = table.Column<int>(type: "int", nullable: false, comment: "نوع الخصم"),
                    OpponentLawyer = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true, comment: "محامي الخصم"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opponents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceOffices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ServiceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ServicePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOffices", x => x.Id);
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
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlImageNationalId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ClientType = table.Column<int>(type: "int", nullable: false),
                    ClientRoleId = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_ClientRoles_ClientRoleId",
                        column: x => x.ClientRoleId,
                        principalTable: "ClientRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CourtTypeId = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courts_CourtTypes_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalTable: "CourtTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    DocumentType = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<int>(type: "int", nullable: false),
                    EntityOwnerType = table.Column<int>(type: "int", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lawyers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    IdentityImagePath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    QualificationDocumentsPath = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false),
                    OfficeId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lawyers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lawyers_Offices_OfficeId",
                        column: x => x.OfficeId,
                        principalTable: "Offices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CourtDivisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    JudgeName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourtDivisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CourtDivisions_Courts_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LegalConsultations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    MobileNumber2 = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    LawyerId = table.Column<int>(type: "int", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Details = table.Column<string>(type: "nvarchar(MAX)", nullable: false),
                    ConsultationType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "مكتبية"),
                    ServiceOfficeId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UrlLegalConsultation = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UrlLegalConsultationInvoice = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "مكتملة"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalConsultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LegalConsultations_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LegalConsultations_ServiceOffices_ServiceOfficeId",
                        column: x => x.ServiceOfficeId,
                        principalTable: "ServiceOffices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PowerOfAttorneys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgencyNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IssuingAuthority = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    OfficeID = table.Column<int>(type: "int", nullable: true),
                    LawyerID = table.Column<int>(type: "int", nullable: true),
                    AgencyType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DerivedPowerOfAttorney = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "Active"),
                    Document_Agent_Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ClientId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PowerOfAttorneys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PowerOfAttorneys_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PowerOfAttorneys_Clients_ClientId1",
                        column: x => x.ClientId1,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PowerOfAttorneys_Lawyers_LawyerID",
                        column: x => x.LawyerID,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PowerOfAttorneys_Offices_OfficeID",
                        column: x => x.OfficeID,
                        principalTable: "Offices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Cases",
                schema: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CaseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CaseNumberProsecution = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CaseNumberInPolice = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    InternalReference = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    FilingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FirstSessionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CourtTypeId = table.Column<int>(type: "int", nullable: false),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    CourtDivisionId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    PowerOfAttorneyId = table.Column<int>(type: "int", nullable: true),
                    OpponentId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CaseTypeId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IsConfidential = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    Outcome = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ClientId1 = table.Column<int>(type: "int", nullable: true),
                    CourtId1 = table.Column<int>(type: "int", nullable: true),
                    CourtTypeId1 = table.Column<int>(type: "int", nullable: true),
                    OpponentId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cases_CaseTypes_CaseTypeId",
                        column: x => x.CaseTypeId,
                        principalSchema: "Cases",
                        principalTable: "CaseTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cases_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cases_Clients_ClientId1",
                        column: x => x.ClientId1,
                        principalTable: "Clients",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cases_CourtDivisions_CourtDivisionId",
                        column: x => x.CourtDivisionId,
                        principalTable: "CourtDivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cases_CourtTypes_CourtTypeId",
                        column: x => x.CourtTypeId,
                        principalTable: "CourtTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cases_CourtTypes_CourtTypeId1",
                        column: x => x.CourtTypeId1,
                        principalTable: "CourtTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cases_Courts_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cases_Courts_CourtId1",
                        column: x => x.CourtId1,
                        principalTable: "Courts",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cases_Opponents_OpponentId",
                        column: x => x.OpponentId,
                        principalTable: "Opponents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Cases_Opponents_OpponentId1",
                        column: x => x.OpponentId1,
                        principalTable: "Opponents",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Cases_PowerOfAttorneys_PowerOfAttorneyId",
                        column: x => x.PowerOfAttorneyId,
                        principalTable: "PowerOfAttorneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "DerivedPowerOfAttorneys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentPowerOfAttorneyId = table.Column<int>(type: "int", nullable: false),
                    LawyerId = table.Column<int>(type: "int", nullable: false),
                    DerivedNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IssueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AuthorityScope = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Derived_Document_Agent_Url = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LawyerId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DerivedPowerOfAttorneys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DerivedPowerOfAttorneys_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DerivedPowerOfAttorneys_Lawyers_LawyerId1",
                        column: x => x.LawyerId1,
                        principalTable: "Lawyers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DerivedPowerOfAttorneys_PowerOfAttorneys_ParentPowerOfAttorneyId",
                        column: x => x.ParentPowerOfAttorneyId,
                        principalTable: "PowerOfAttorneys",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseDocuments",
                schema: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseDocuments_Cases_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "Cases",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "caseSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: true),
                    CourtId = table.Column<int>(type: "int", nullable: false),
                    CourtDivisionId = table.Column<int>(type: "int", nullable: false),
                    AssignedLawyerId = table.Column<int>(type: "int", nullable: true),
                    SessionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SessionTime = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SessionNumber = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SessionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Decision = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    NextSessionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LawyerAttended = table.Column<bool>(type: "bit", nullable: false),
                    ClientAttended = table.Column<bool>(type: "bit", nullable: false),
                    SessionStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_caseSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_caseSessions_Cases_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "Cases",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_caseSessions_CourtDivisions_CourtDivisionId",
                        column: x => x.CourtDivisionId,
                        principalTable: "CourtDivisions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_caseSessions_Courts_CourtId",
                        column: x => x.CourtId,
                        principalTable: "Courts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_caseSessions_Lawyers_AssignedLawyerId",
                        column: x => x.AssignedLawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaseStages",
                schema: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Stage = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Priority = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    EndDateStage = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseStages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseStages_Cases_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "Cases",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaseTeams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LawyerId = table.Column<int>(type: "int", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    DerivedPowerOfAttorneyId = table.Column<int>(type: "int", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false, defaultValue: "مساعد"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseTeams", x => x.Id);
                    table.CheckConstraint("CK_CaseTeam_EndDate", "[EndDate] IS NULL OR [EndDate] > [StartDate]");
                    table.ForeignKey(
                        name: "FK_CaseTeams_Cases_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "Cases",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CaseTeams_Lawyers_LawyerId",
                        column: x => x.LawyerId,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ContractNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContractDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Active"),
                    FinancialAgreementType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TotalCaseAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Percentage = table.Column<int>(type: "int", nullable: true),
                    FinalAgreedAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ContractDocumentUrl = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.CheckConstraint("CK_Contract_EndDate", "[EndDate] IS NULL OR [EndDate] > [StartDate]");
                    table.CheckConstraint("CK_Contract_Percentage", "[Percentage] IS NULL OR ([Percentage] >= 0 AND [Percentage] <= 100)");
                    table.ForeignKey(
                        name: "FK_Contracts_Cases_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "Cases",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Contracts_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CaseEvidences",
                schema: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    CaseSessionId = table.Column<int>(type: "int", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EvidenceType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Source = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    SubmissionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    CourtNotes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseEvidences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseEvidences_Cases_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "Cases",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CaseEvidences_caseSessions_CaseSessionId",
                        column: x => x.CaseSessionId,
                        principalTable: "caseSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseWitnesses",
                schema: "Cases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseId = table.Column<int>(type: "int", nullable: false),
                    CaseSessionId = table.Column<int>(type: "int", nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NationalId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    TestimonySummary = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    IsAttended = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    TestimonyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseWitnesses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseWitnesses_Cases_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "Cases",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CaseWitnesses_caseSessions_CaseSessionId",
                        column: x => x.CaseSessionId,
                        principalTable: "caseSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SessionDocument",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CaseSessionId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionDocument", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionDocument_caseSessions_CaseSessionId",
                        column: x => x.CaseSessionId,
                        principalTable: "caseSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Pending"),
                    Priority = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false, defaultValue: "Normal"),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    CaseTeamId = table.Column<int>(type: "int", nullable: true),
                    CaseId = table.Column<int>(type: "int", nullable: true),
                    CaseTeamId1 = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskItems_CaseTeams_CaseTeamId",
                        column: x => x.CaseTeamId,
                        principalTable: "CaseTeams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskItems_CaseTeams_CaseTeamId1",
                        column: x => x.CaseTeamId1,
                        principalTable: "CaseTeams",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TaskItems_Cases_CaseId",
                        column: x => x.CaseId,
                        principalSchema: "Cases",
                        principalTable: "Cases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskItems_Lawyers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TaskComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskItemId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskComments_Lawyers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Lawyers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TaskComments_TaskItems_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskDocuments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskItemId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FileType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskDocuments_TaskItems_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "TaskItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CaseDocuments_CaseId",
                schema: "Cases",
                table: "CaseDocuments",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseEvidences_CaseId",
                schema: "Cases",
                table: "CaseEvidences",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseEvidences_CaseId_IsAccepted",
                schema: "Cases",
                table: "CaseEvidences",
                columns: new[] { "CaseId", "IsAccepted" });

            migrationBuilder.CreateIndex(
                name: "IX_CaseEvidences_CaseSessionId",
                schema: "Cases",
                table: "CaseEvidences",
                column: "CaseSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseEvidences_CaseSessionId_EvidenceType",
                schema: "Cases",
                table: "CaseEvidences",
                columns: new[] { "CaseSessionId", "EvidenceType" });

            migrationBuilder.CreateIndex(
                name: "IX_CaseEvidences_EvidenceType",
                schema: "Cases",
                table: "CaseEvidences",
                column: "EvidenceType");

            migrationBuilder.CreateIndex(
                name: "IX_CaseEvidences_IsAccepted",
                schema: "Cases",
                table: "CaseEvidences",
                column: "IsAccepted");

            migrationBuilder.CreateIndex(
                name: "IX_CaseEvidences_SubmissionDate",
                schema: "Cases",
                table: "CaseEvidences",
                column: "SubmissionDate");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CaseNumber",
                schema: "Cases",
                table: "Cases",
                column: "CaseNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CaseTypeId",
                schema: "Cases",
                table: "Cases",
                column: "CaseTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CaseTypeId_FilingDate",
                schema: "Cases",
                table: "Cases",
                columns: new[] { "CaseTypeId", "FilingDate" });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ClientId",
                schema: "Cases",
                table: "Cases",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ClientId_Status",
                schema: "Cases",
                table: "Cases",
                columns: new[] { "ClientId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_Cases_ClientId1",
                schema: "Cases",
                table: "Cases",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CourtDivisionId",
                schema: "Cases",
                table: "Cases",
                column: "CourtDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CourtId",
                schema: "Cases",
                table: "Cases",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CourtId1",
                schema: "Cases",
                table: "Cases",
                column: "CourtId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CourtTypeId",
                schema: "Cases",
                table: "Cases",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_CourtTypeId1",
                schema: "Cases",
                table: "Cases",
                column: "CourtTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_FilingDate",
                schema: "Cases",
                table: "Cases",
                column: "FilingDate");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_FirstSessionDate",
                schema: "Cases",
                table: "Cases",
                column: "FirstSessionDate");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_InternalReference",
                schema: "Cases",
                table: "Cases",
                column: "InternalReference",
                unique: true,
                filter: "[InternalReference] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_IsArchived",
                schema: "Cases",
                table: "Cases",
                column: "IsArchived");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_IsConfidential",
                schema: "Cases",
                table: "Cases",
                column: "IsConfidential");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_OpponentId",
                schema: "Cases",
                table: "Cases",
                column: "OpponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_OpponentId1",
                schema: "Cases",
                table: "Cases",
                column: "OpponentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_PowerOfAttorneyId",
                schema: "Cases",
                table: "Cases",
                column: "PowerOfAttorneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_Status",
                schema: "Cases",
                table: "Cases",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Cases_Status_IsArchived",
                schema: "Cases",
                table: "Cases",
                columns: new[] { "Status", "IsArchived" });

            migrationBuilder.CreateIndex(
                name: "IX_caseSessions_AssignedLawyerId",
                table: "caseSessions",
                column: "AssignedLawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_caseSessions_CaseId",
                table: "caseSessions",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_caseSessions_CourtDivisionId",
                table: "caseSessions",
                column: "CourtDivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_caseSessions_CourtId",
                table: "caseSessions",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_caseSessions_SessionDate",
                table: "caseSessions",
                column: "SessionDate");

            migrationBuilder.CreateIndex(
                name: "IX_caseSessions_SessionStatus",
                table: "caseSessions",
                column: "SessionStatus");

            migrationBuilder.CreateIndex(
                name: "IX_CaseStages_CaseId",
                schema: "Cases",
                table: "CaseStages",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseStages_IsActive",
                schema: "Cases",
                table: "CaseStages",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTeams_CaseId",
                table: "CaseTeams",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTeams_CaseId_IsActive",
                table: "CaseTeams",
                columns: new[] { "CaseId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_CaseTeams_DerivedPowerOfAttorneyId",
                table: "CaseTeams",
                column: "DerivedPowerOfAttorneyId",
                unique: true,
                filter: "[DerivedPowerOfAttorneyId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTeams_EndDate",
                table: "CaseTeams",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTeams_IsActive",
                table: "CaseTeams",
                column: "IsActive");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTeams_LawyerId",
                table: "CaseTeams",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTeams_LawyerId_CaseId",
                table: "CaseTeams",
                columns: new[] { "LawyerId", "CaseId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CaseTeams_LawyerId_IsActive",
                table: "CaseTeams",
                columns: new[] { "LawyerId", "IsActive" });

            migrationBuilder.CreateIndex(
                name: "IX_CaseTeams_Role",
                table: "CaseTeams",
                column: "Role");

            migrationBuilder.CreateIndex(
                name: "IX_CaseTeams_StartDate",
                table: "CaseTeams",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_CaseWitnesses_CaseId",
                schema: "Cases",
                table: "CaseWitnesses",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseWitnesses_CaseId_IsAttended",
                schema: "Cases",
                table: "CaseWitnesses",
                columns: new[] { "CaseId", "IsAttended" });

            migrationBuilder.CreateIndex(
                name: "IX_CaseWitnesses_CaseSessionId",
                schema: "Cases",
                table: "CaseWitnesses",
                column: "CaseSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseWitnesses_CaseSessionId_IsAttended",
                schema: "Cases",
                table: "CaseWitnesses",
                columns: new[] { "CaseSessionId", "IsAttended" });

            migrationBuilder.CreateIndex(
                name: "IX_CaseWitnesses_FullName",
                schema: "Cases",
                table: "CaseWitnesses",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_CaseWitnesses_IsAttended",
                schema: "Cases",
                table: "CaseWitnesses",
                column: "IsAttended");

            migrationBuilder.CreateIndex(
                name: "IX_CaseWitnesses_NationalId",
                schema: "Cases",
                table: "CaseWitnesses",
                column: "NationalId");

            migrationBuilder.CreateIndex(
                name: "IX_CaseWitnesses_NationalId_CaseId",
                schema: "Cases",
                table: "CaseWitnesses",
                columns: new[] { "NationalId", "CaseId" },
                unique: true,
                filter: "[NationalId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CaseWitnesses_TestimonyDate",
                schema: "Cases",
                table: "CaseWitnesses",
                column: "TestimonyDate");

            migrationBuilder.CreateIndex(
                name: "IX_ClientRoles_Name",
                table: "ClientRoles",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_ClientRoleId",
                table: "Clients",
                column: "ClientRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_Email",
                table: "Clients",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL AND [IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_FullName",
                table: "Clients",
                column: "FullName");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PhoneNumber",
                table: "Clients",
                column: "PhoneNumber",
                unique: true,
                filter: "[PhoneNumber] IS NOT NULL AND [IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_UrlImageNationalId",
                table: "Clients",
                column: "UrlImageNationalId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_CaseId",
                table: "Contracts",
                column: "CaseId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ClientId",
                table: "Contracts",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ContractNumber",
                table: "Contracts",
                column: "ContractNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_EndDate",
                table: "Contracts",
                column: "EndDate");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_FinancialAgreementType",
                table: "Contracts",
                column: "FinancialAgreementType");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_StartDate",
                table: "Contracts",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_Status",
                table: "Contracts",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_Status_StartDate",
                table: "Contracts",
                columns: new[] { "Status", "StartDate" });

            migrationBuilder.CreateIndex(
                name: "IX_CourtDivisions_CourtId",
                table: "CourtDivisions",
                column: "CourtId");

            migrationBuilder.CreateIndex(
                name: "IX_Courts_CourtTypeId",
                table: "Courts",
                column: "CourtTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Courts_Name",
                table: "Courts",
                column: "Name",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_CourtTypes_Name",
                table: "CourtTypes",
                column: "Name",
                unique: true,
                filter: "[IsDeleted] = 0");

            migrationBuilder.CreateIndex(
                name: "IX_DerivedPowerOfAttorneys_LawyerId",
                table: "DerivedPowerOfAttorneys",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_DerivedPowerOfAttorneys_LawyerId1",
                table: "DerivedPowerOfAttorneys",
                column: "LawyerId1");

            migrationBuilder.CreateIndex(
                name: "IX_DerivedPowerOfAttorneys_ParentPowerOfAttorneyId",
                table: "DerivedPowerOfAttorneys",
                column: "ParentPowerOfAttorneyId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_OfficeId",
                table: "Documents",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Lawyers_OfficeId",
                table: "Lawyers",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalConsultations_ConsultationType",
                table: "LegalConsultations",
                column: "ConsultationType");

            migrationBuilder.CreateIndex(
                name: "IX_LegalConsultations_CustomerName",
                table: "LegalConsultations",
                column: "CustomerName");

            migrationBuilder.CreateIndex(
                name: "IX_LegalConsultations_LawyerId",
                table: "LegalConsultations",
                column: "LawyerId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalConsultations_MobileNumber",
                table: "LegalConsultations",
                column: "MobileNumber");

            migrationBuilder.CreateIndex(
                name: "IX_LegalConsultations_ServiceOfficeId",
                table: "LegalConsultations",
                column: "ServiceOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_LegalConsultations_Status",
                table: "LegalConsultations",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_Opponents_Mobile",
                table: "Opponents",
                column: "OpponentMobile");

            migrationBuilder.CreateIndex(
                name: "IX_Opponents_Name",
                table: "Opponents",
                column: "OpponentName");

            migrationBuilder.CreateIndex(
                name: "IX_Opponents_Type",
                table: "Opponents",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_PowerOfAttorneys_ClientId",
                table: "PowerOfAttorneys",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_PowerOfAttorneys_ClientId1",
                table: "PowerOfAttorneys",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_PowerOfAttorneys_LawyerID",
                table: "PowerOfAttorneys",
                column: "LawyerID");

            migrationBuilder.CreateIndex(
                name: "IX_PowerOfAttorneys_OfficeID",
                table: "PowerOfAttorneys",
                column: "OfficeID");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOffices_ServiceName",
                table: "ServiceOffices",
                column: "ServiceName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOffices_ServicePrice",
                table: "ServiceOffices",
                column: "ServicePrice");

            migrationBuilder.CreateIndex(
                name: "IX_SessionDocument_CaseSessionId",
                table: "SessionDocument",
                column: "CaseSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_CreatedAt",
                table: "TaskComments",
                column: "CreatedAt");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_CreatedById",
                table: "TaskComments",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_TaskItemId",
                table: "TaskComments",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDocuments_FileType",
                table: "TaskDocuments",
                column: "FileType");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDocuments_TaskItemId",
                table: "TaskDocuments",
                column: "TaskItemId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskDocuments_TaskItemId_FileType",
                table: "TaskDocuments",
                columns: new[] { "TaskItemId", "FileType" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_CaseId",
                table: "TaskItems",
                column: "CaseId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_CaseId_Status",
                table: "TaskItems",
                columns: new[] { "CaseId", "Status" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_CaseTeamId",
                table: "TaskItems",
                column: "CaseTeamId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_CaseTeamId_DueDate",
                table: "TaskItems",
                columns: new[] { "CaseTeamId", "DueDate" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_CaseTeamId1",
                table: "TaskItems",
                column: "CaseTeamId1");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_CompletedAt",
                table: "TaskItems",
                column: "CompletedAt");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_CreatedById",
                table: "TaskItems",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_DueDate",
                table: "TaskItems",
                column: "DueDate");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_Priority",
                table: "TaskItems",
                column: "Priority");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_StartDate",
                table: "TaskItems",
                column: "StartDate");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_Status",
                table: "TaskItems",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_Status_DueDate",
                table: "TaskItems",
                columns: new[] { "Status", "DueDate" });
        }

        /// <inheritdoc />
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
                name: "CaseDocuments",
                schema: "Cases");

            migrationBuilder.DropTable(
                name: "CaseEvidences",
                schema: "Cases");

            migrationBuilder.DropTable(
                name: "CaseStages",
                schema: "Cases");

            migrationBuilder.DropTable(
                name: "CaseWitnesses",
                schema: "Cases");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "DerivedPowerOfAttorneys");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "LegalConsultations");

            migrationBuilder.DropTable(
                name: "SessionDocument");

            migrationBuilder.DropTable(
                name: "TaskComments");

            migrationBuilder.DropTable(
                name: "TaskDocuments");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ServiceOffices");

            migrationBuilder.DropTable(
                name: "caseSessions");

            migrationBuilder.DropTable(
                name: "TaskItems");

            migrationBuilder.DropTable(
                name: "CaseTeams");

            migrationBuilder.DropTable(
                name: "Cases",
                schema: "Cases");

            migrationBuilder.DropTable(
                name: "CaseTypes",
                schema: "Cases");

            migrationBuilder.DropTable(
                name: "CourtDivisions");

            migrationBuilder.DropTable(
                name: "Opponents");

            migrationBuilder.DropTable(
                name: "PowerOfAttorneys");

            migrationBuilder.DropTable(
                name: "Courts");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Lawyers");

            migrationBuilder.DropTable(
                name: "CourtTypes");

            migrationBuilder.DropTable(
                name: "ClientRoles");

            migrationBuilder.DropTable(
                name: "Offices");
        }
    }
}
