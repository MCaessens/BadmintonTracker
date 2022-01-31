using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Imi.Project.Api.Infrastructure.Migrations
{
    public partial class AddCustomClaimTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    LastName = table.Column<string>(maxLength: 100, nullable: false),
                    AccountIntegrityId = table.Column<Guid>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
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
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
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
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(maxLength: 30, nullable: false),
                    City = table.Column<string>(maxLength: 100, nullable: false),
                    Street = table.Column<string>(maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    Longitude = table.Column<float>(nullable: false),
                    Latitude = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Rackets",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Brand = table.Column<string>(maxLength: 100, nullable: false),
                    Model = table.Column<string>(maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    RacketType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rackets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rackets_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ShuttleCocks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Brand = table.Column<string>(maxLength: 100, nullable: false),
                    Model = table.Column<string>(maxLength: 100, nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    ShuttleType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShuttleCocks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShuttleCocks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Opponent = table.Column<string>(maxLength: 100, nullable: false),
                    LocationId = table.Column<Guid>(nullable: true),
                    ShuttleCockId = table.Column<Guid>(nullable: true),
                    RacketId = table.Column<Guid>(nullable: true),
                    Score = table.Column<int>(nullable: false),
                    OpponentScore = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Games_Rackets_RacketId",
                        column: x => x.RacketId,
                        principalTable: "Rackets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Games_ShuttleCocks_ShuttleCockId",
                        column: x => x.ShuttleCockId,
                        principalTable: "ShuttleCocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Games_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "fd98a13f-8523-4702-ba54-036f789449a7", "admin", "ADMIN" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "bbbdedda-ae07-4932-9d16-4c6098016dfa", "user", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountIntegrityId", "ConcurrencyStamp", "DateOfBirth", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), 0, new Guid("e0314c59-1db9-4d12-99cb-ad3880a4d250"), "97707fa7-a803-4327-9fcd-28eb67d4b13e", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "caessens.marco@gmail.com", false, "Marco", "Caessens", false, null, "CAESSENS.MARCO@GMAIL.COM", "WOLLOW", "AQAAAAEAACcQAAAAEDFjPdcOC2rFdxmnek9W9ojcPnOU5Oots4k7l2+wNIcR94P518I04/BG/VKcgOwilA==", null, false, "s2mkkh6qbU", false, "WolloW" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), 0, new Guid("2f568aba-0f69-4939-954d-2c35bf783d7f"), "a9bbfad6-e63f-47ca-ba2a-e7154124bb8a", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "cedric.theys@gmail.com", false, "Cedric", "Theys", false, null, "CEDRIC.THEYS@GMAIL.COM", "EATLE", "AQAAAAEAACcQAAAAEAwV6zPEUttXt21sF8ivybuHzKkXys5otI2z84N/B0tc0hVJwV+Xi1l4ah/8J/mFyQ==", null, false, "n2E7T6ssNJ", false, "Eatle" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), 0, new Guid("b113d67d-8cfb-4f73-a318-da6eb2b59a31"), "e66d88fa-12ae-49db-80f7-c574a39ab1ab", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "hennesley.moerman@gmail.com", false, "Hennesley", "Moerman", false, null, "HENNESLEY.MOERMAN@GMAIL.COM", "JUSTALAD", "AQAAAAEAACcQAAAAECGlWZA7W3K4fa3bYyCO2G5r7mzN2q/qMnZspWMu9uHglHu3DNl4bpw0eT43SDMQ/w==", null, false, "G3rvJEt2r8", false, "JustALad" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), 0, new Guid("10c93c2c-c3da-4ed9-a633-2b0f3a0d9bfd"), "3aecea96-3326-4c48-83b0-fa5a7c2d8d90", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "felien.braeckevelt@gmail.com", false, "Felien", "Braeckevelt", false, null, "FELIEN.BRAECKEVELT@GMAIL.COM", "NOORIE", "AQAAAAEAACcQAAAAELPyE22FpNc0NC/RXAJp8fxiOU4LqygKBPBwujkozfpoU9pEVJ+AoFmgfO543NaydA==", null, false, "XasGBe9U74", false, "Noorie" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), 0, new Guid("ca79c203-9468-4966-b5f7-3d92a44c5758"), "0ceaaf78-622e-43c9-8e0a-48e8bd0c09df", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "louis.caessens@gmail.com", false, "Louis", "Caessens", false, null, "LOUIS.CAESSENS@GMAIL.COM", "LUIGI6509", "AQAAAAEAACcQAAAAECcMZKpVDm0hJ8kD47dyifQaKJdQLU3+b36Zo1mCCnO/KwQzH4D0l21oCOxP+2WVww==", null, false, "6fIqpRuE7E", false, "Luigi6509" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), 0, new Guid("5a939582-5c05-4297-a010-821f0eeb3bb4"), "d8eb974b-ce03-492c-b6d4-a41c8656883d", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "wesley.caessens@gmail.com", false, "Wesley", "Caessens", false, null, "WESLEY.CAESSENS@GMAIL.COM", "COBBLEWOBBLES", "AQAAAAEAACcQAAAAEEm0eh6tNWXygrzYmbpSO3uVhsATTBfwk5e4PIiY/6+eb3J3szQLNjfmoeBDUR6nLQ==", null, false, "L62fp75ope", false, "CobbleWobbles" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), 0, new Guid("6fb29060-ab74-4b4d-ad7e-7acf28ab9004"), "4a7d6d26-8347-4f61-b22c-23c51f2ff886", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "pamela.valcke@gmail.com", false, "Pamela", "Valcke", false, null, "PAMELA.VALCKE@GMAIL.COM", "PAMKE", "AQAAAAEAACcQAAAAEL5Zc6zmCqp2G1B/Clnc0Ndfw+G/9XU8lLkFD+RBLOkplIOvyZaeeIW1IrWiOlGf5g==", null, false, "6d7QUQ6wue", false, "Pamke" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), 0, new Guid("b3fd51c8-8b03-48f3-bef8-ae64cd74f420"), "ff015dd7-6d94-4bb7-b7db-7b02d88850b5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "spyro.caessens@gmail.com", false, "Spyro", "Caessens", false, null, "SPYRO.CAESSENS@GMAIL.COM", "SPYROENKIE", "AQAAAAEAACcQAAAAEIBCfdWotAedJj3jUVve0nky4zGvQLzr+KWSv2Lr0rbXmXkqYk2jjD87HIXWQAUBJQ==", null, false, "YnNTPhEWik", false, "Spyroenkie" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), 0, new Guid("b1625154-9aa3-4bf9-b83a-623e52305965"), "e157de5f-8882-4383-ab7e-ecc2e9ccf358", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "helena.bafort@gmail.com", false, "Valerie", "Seline", false, null, "HELENA.BAFORT@GMAIL.COM", "VASELINE", "AQAAAAEAACcQAAAAECbmIeIvLzhVjU2oUD7gh4P15PiDZx4gOAdFwOFWJY95lqTcpfialwilDDU88obppQ==", null, false, "lMLdnKH1il", false, "VaSeline" },
                    { new Guid("00000000-0000-0000-0000-000000000010"), 0, new Guid("5a76172e-256d-414e-b8c8-3224dca39259"), "f9dcc6ec-1fc6-4fda-b237-38518cacc1a9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "jill.valentijn@gmail.com", false, "Jill", "Valentijn", false, null, "JILL.VALENTIJN@GMAIL.COM", "JILLVAL", "AQAAAAEAACcQAAAAEFe+bJ1X0QDoHjChxXZpfnksBZo3RRdRnjNnoR4ljpdmWAsHprRlSvadf06380aBPg==", null, false, "3xSn0ECaag", false, "JillVal" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 16, "userId", "00000000-0000-0000-0000-000000000006", new Guid("00000000-0000-0000-0000-000000000006") },
                    { 4, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "felien.braeckevelt@gmail.com", new Guid("00000000-0000-0000-0000-000000000004") },
                    { 14, "userId", "00000000-0000-0000-0000-000000000004", new Guid("00000000-0000-0000-0000-000000000004") },
                    { 24, "accountIntegrityId", "10c93c2c-c3da-4ed9-a633-2b0f3a0d9bfd", new Guid("00000000-0000-0000-0000-000000000004") },
                    { 29, "accountIntegrityId", "b1625154-9aa3-4bf9-b83a-623e52305965", new Guid("00000000-0000-0000-0000-000000000009") },
                    { 19, "userId", "00000000-0000-0000-0000-000000000009", new Guid("00000000-0000-0000-0000-000000000009") },
                    { 9, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "helena.bafort@gmail.com", new Guid("00000000-0000-0000-0000-000000000009") },
                    { 5, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "louis.caessens@gmail.com", new Guid("00000000-0000-0000-0000-000000000005") },
                    { 15, "userId", "00000000-0000-0000-0000-000000000005", new Guid("00000000-0000-0000-0000-000000000005") },
                    { 25, "accountIntegrityId", "ca79c203-9468-4966-b5f7-3d92a44c5758", new Guid("00000000-0000-0000-0000-000000000005") },
                    { 28, "accountIntegrityId", "b3fd51c8-8b03-48f3-bef8-ae64cd74f420", new Guid("00000000-0000-0000-0000-000000000008") },
                    { 18, "userId", "00000000-0000-0000-0000-000000000008", new Guid("00000000-0000-0000-0000-000000000008") },
                    { 8, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "spyro.caessens@gmail.com", new Guid("00000000-0000-0000-0000-000000000008") },
                    { 6, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "wesley.caessens@gmail.com", new Guid("00000000-0000-0000-0000-000000000006") },
                    { 27, "accountIntegrityId", "6fb29060-ab74-4b4d-ad7e-7acf28ab9004", new Guid("00000000-0000-0000-0000-000000000007") },
                    { 26, "accountIntegrityId", "5a939582-5c05-4297-a010-821f0eeb3bb4", new Guid("00000000-0000-0000-0000-000000000006") },
                    { 10, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "jill.valentijn@gmail.com", new Guid("00000000-0000-0000-0000-000000000010") },
                    { 20, "userId", "00000000-0000-0000-0000-000000000010", new Guid("00000000-0000-0000-0000-000000000010") },
                    { 30, "accountIntegrityId", "5a76172e-256d-414e-b8c8-3224dca39259", new Guid("00000000-0000-0000-0000-000000000010") },
                    { 23, "accountIntegrityId", "b113d67d-8cfb-4f73-a318-da6eb2b59a31", new Guid("00000000-0000-0000-0000-000000000003") },
                    { 1, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "caessens.marco@gmail.com", new Guid("00000000-0000-0000-0000-000000000001") },
                    { 11, "userId", "00000000-0000-0000-0000-000000000001", new Guid("00000000-0000-0000-0000-000000000001") },
                    { 21, "accountIntegrityId", "e0314c59-1db9-4d12-99cb-ad3880a4d250", new Guid("00000000-0000-0000-0000-000000000001") },
                    { 17, "userId", "00000000-0000-0000-0000-000000000007", new Guid("00000000-0000-0000-0000-000000000007") },
                    { 2, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "cedric.theys@gmail.com", new Guid("00000000-0000-0000-0000-000000000002") },
                    { 12, "userId", "00000000-0000-0000-0000-000000000002", new Guid("00000000-0000-0000-0000-000000000002") },
                    { 7, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "pamela.valcke@gmail.com", new Guid("00000000-0000-0000-0000-000000000007") },
                    { 3, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "hennesley.moerman@gmail.com", new Guid("00000000-0000-0000-0000-000000000003") },
                    { 13, "userId", "00000000-0000-0000-0000-000000000003", new Guid("00000000-0000-0000-0000-000000000003") },
                    { 22, "accountIntegrityId", "2f568aba-0f69-4939-954d-2c35bf783d7f", new Guid("00000000-0000-0000-0000-000000000002") }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000002") }
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "City", "ImageUrl", "Latitude", "Longitude", "Name", "PostalCode", "Street", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Bredene", "images/locations/00000000-0000-0000-0000-000000000010.jpg", 51.237938f, 2.9659376f, "Sporthal Ter Polder", "8450", "Spuikomlaan 21", new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Beerse", "images/locations/00000000-0000-0000-0000-000000000002.jpg", 51.309048f, 4.840872f, "Sportcentrum Beerse", "2340", "Rerum Novarumlaan 31", new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Lievegem", "images/locations/00000000-0000-0000-0000-000000000003.jpg", 51.12313f, 3.5617187f, "Zwembad Den Boer", "9930", "Den Boer 17", new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Lievegem", "images/locations/00000000-0000-0000-0000-000000000004.jpg", 51.15446f, 3.6095564f, "Sportcentrum Waarschoot", "9950", "Kleine Weg 3", new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Brugge", "images/locations/00000000-0000-0000-0000-000000000005.jpg", 51.206585f, 3.2418122f, "Sport Vlaanderen Brugge", "8310", "Nijverheidsstraat 112", new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Oostende", "images/locations/00000000-0000-0000-0000-000000000009.jpg", 51.209965f, 2.9246674f, "Mister V-arena", "8400", "Sportparklaan 6", new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Gent", "images/locations/00000000-0000-0000-0000-000000000006.jpg", 51.03461f, 3.703925f, "Sporthal Gent", "9000", "Sint-Denijslaan 251", new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Eeklo", "images/locations/00000000-0000-0000-0000-000000000001.jpg", 51.18451f, 3.57635f, "Sportpark Eeklo", "9900", "Burgemeester Pussemierstraat 3", new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Gent", "images/locations/00000000-0000-0000-0000-000000000007.jpg", 51.003216f, 3.7108703f, "Sporthal Hekers", "9052", "Ter Linden 29", new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Aalter", "images/locations/00000000-0000-0000-0000-000000000008.jpg", 51.089367f, 3.4387248f, "Sportpark Aalter-centrum", "9880", "Lindestraat 17", new Guid("00000000-0000-0000-0000-000000000008") }
                });

            migrationBuilder.InsertData(
                table: "Rackets",
                columns: new[] { "Id", "Brand", "ImageUrl", "Model", "RacketType", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Yonex", "images/rackets/00000000-0000-0000-0000-000000000004.jpg", "Isometric TR-1 White", 0, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Yonex", "images/rackets/00000000-0000-0000-0000-000000000001.jpg", "Voltric DG7 Lime", 0, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Yonex", "images/rackets/00000000-0000-0000-0000-000000000002.jpg", "Atrox 2 Blue", 1, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Yonex", "images/rackets/00000000-0000-0000-0000-000000000003.jpg", "Atrox 5 FX", 2, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Adidas", "images/rackets/00000000-0000-0000-0000-000000000008.jpg", "E08.2 Groen", 1, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Yonex", "images/rackets/00000000-0000-0000-0000-000000000006.jpg", "Astrox 55 Light Silver", 2, new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Perfly", "images/rackets/00000000-0000-0000-0000-000000000007.jpg", "BR700", 0, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Victor", "images/rackets/00000000-0000-0000-0000-000000000010.jpg", "Advanced Junior", 2, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Yonex", "images/rackets/00000000-0000-0000-0000-000000000005.jpg", "Nanoflare Blue", 2, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Idema", "images/rackets/00000000-0000-0000-0000-000000000009.jpg", "Spordas Junior", 2, new Guid("00000000-0000-0000-0000-000000000009") }
                });

            migrationBuilder.InsertData(
                table: "ShuttleCocks",
                columns: new[] { "Id", "Brand", "ImageUrl", "Model", "ShuttleType", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Yonex", "images/shuttlecocks/00000000-0000-0000-0000-000000000004.jpg", "League 7", 0, new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Yonex", "images/shuttlecocks/00000000-0000-0000-0000-000000000007.jpg", "Mavis 200", 1, new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Perfly", "images/shuttlecocks/00000000-0000-0000-0000-000000000003.jpg", "PSC 130", 1, new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Yonex", "images/shuttlecocks/00000000-0000-0000-0000-000000000008.jpg", "Mavis 2000", 1, new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Perfly", "images/shuttlecocks/00000000-0000-0000-0000-000000000002.jpg", "PSC 100", 1, new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Yonex", "images/shuttlecocks/00000000-0000-0000-0000-000000000005.jpg", "Mavis 350", 1, new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Yonex", "images/shuttlecocks/00000000-0000-0000-0000-000000000009.jpg", "Mavis 600", 1, new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Perfly", "images/shuttlecocks/00000000-0000-0000-0000-000000000001.jpg", "FSC 930", 0, new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Yonex", "images/shuttlecocks/00000000-0000-0000-0000-000000000010.jpg", "Mavis 10", 1, new Guid("00000000-0000-0000-0000-000000000010") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Yonex", "images/shuttlecocks/00000000-0000-0000-0000-000000000006.jpg", "Aerosensa 30", 0, new Guid("00000000-0000-0000-0000-000000000006") }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "LocationId", "Opponent", "OpponentScore", "RacketId", "Score", "ShuttleCockId", "UserId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001"), "Louis Caessens", 0, new Guid("00000000-0000-0000-0000-000000000001"), 0, new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000001") },
                    { new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002"), "Felien Braeckevelt", 0, new Guid("00000000-0000-0000-0000-000000000002"), 0, new Guid("00000000-0000-0000-0000-000000000002"), new Guid("00000000-0000-0000-0000-000000000002") },
                    { new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000003"), "Filip Bruyr", 0, new Guid("00000000-0000-0000-0000-000000000003"), 0, new Guid("00000000-0000-0000-0000-000000000003"), new Guid("00000000-0000-0000-0000-000000000003") },
                    { new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000004"), "Amber Lippens", 0, new Guid("00000000-0000-0000-0000-000000000004"), 0, new Guid("00000000-0000-0000-0000-000000000004"), new Guid("00000000-0000-0000-0000-000000000004") },
                    { new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000005"), "Stefaan Turpyn", 0, new Guid("00000000-0000-0000-0000-000000000005"), 0, new Guid("00000000-0000-0000-0000-000000000005"), new Guid("00000000-0000-0000-0000-000000000005") },
                    { new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000006"), "Wesley Caessens", 0, new Guid("00000000-0000-0000-0000-000000000006"), 0, new Guid("00000000-0000-0000-0000-000000000006"), new Guid("00000000-0000-0000-0000-000000000006") },
                    { new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000007"), "Louis Caessens", 0, new Guid("00000000-0000-0000-0000-000000000007"), 0, new Guid("00000000-0000-0000-0000-000000000007"), new Guid("00000000-0000-0000-0000-000000000007") },
                    { new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008"), "Tine Franchois", 0, new Guid("00000000-0000-0000-0000-000000000008"), 0, new Guid("00000000-0000-0000-0000-000000000008"), new Guid("00000000-0000-0000-0000-000000000008") },
                    { new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000009"), "Tine Franchois", 0, new Guid("00000000-0000-0000-0000-000000000009"), 0, new Guid("00000000-0000-0000-0000-000000000009"), new Guid("00000000-0000-0000-0000-000000000009") },
                    { new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000010"), "John Doe", 0, new Guid("00000000-0000-0000-0000-000000000010"), 0, new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000010") }
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
                name: "IX_Games_LocationId",
                table: "Games",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_RacketId",
                table: "Games",
                column: "RacketId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_ShuttleCockId",
                table: "Games",
                column: "ShuttleCockId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_UserId",
                table: "Games",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_UserId",
                table: "Locations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Rackets_UserId",
                table: "Rackets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ShuttleCocks_UserId",
                table: "ShuttleCocks",
                column: "UserId");
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
                name: "Games");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Rackets");

            migrationBuilder.DropTable(
                name: "ShuttleCocks");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
