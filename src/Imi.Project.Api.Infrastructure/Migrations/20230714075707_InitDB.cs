using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Imi.Project.Api.Infrastructure.Migrations
{
    public partial class InitDB : Migration
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
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    HasApprovedTerms = table.Column<bool>(type: "bit", nullable: true),
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
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Diets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diets", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
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
                name: "Recipes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PrepTime = table.Column<int>(type: "int", nullable: false),
                    CookTime = table.Column<int>(type: "int", nullable: false),
                    Servings = table.Column<int>(type: "int", nullable: false),
                    ImgURL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DietId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Recipes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recipes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Recipes_Diets_DietId",
                        column: x => x.DietId,
                        principalTable: "Diets",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FavoriteRecipes",
                columns: table => new
                {
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteRecipes", x => new { x.RecipeId, x.ApplicationUserId });
                    table.ForeignKey(
                        name: "FK_FavoriteRecipes_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FavoriteRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StepNumber = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructions_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeIngredients",
                columns: table => new
                {
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IngredientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UnitId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeIngredients", x => new { x.IngredientId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeIngredients_Units_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateTimeStamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ApplicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reviews_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000001", "1122e952-b5e1-4db4-bce6-0f5129f4b6c3", "Admin", "ADMIN" },
                    { "00000000-0000-0000-0000-000000000002", "dc4e1d2e-22da-4ee5-8d04-e05e22470235", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "HasApprovedTerms", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000001", 0, "c8554266-b401-4519-9aeb-a9283053fc58", "admin@imi.be", true, null, false, null, "ADMIN@IMI.BE", "IMIADMIN", "AQAAAAEAACcQAAAAEKyB2p8wfQHi0Ia6TULSIezqZZQ4BAsDT0RsJMbCXna14MrewbILfYHX2nTpKCtvow==", null, false, "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA", false, "ImiAdmin" },
                    { "00000000-0000-0000-0000-000000000002", 0, "2b3ba136-4654-49c1-8329-1e266a1f7453", "user@imi.be", true, true, false, null, "USER@IMI.BE", "IMIUSER", "AQAAAAEAACcQAAAAEHj60BvW9LyTI8qdp/90qWarTsvPuGqOZQvAMAu++NPGS+hrbTgXTTa09CUlW5nCvQ==", null, false, "WWPCRDAS3MJWQD5CSW2GWPRADBXEZINA", false, "ImiUser" },
                    { "00000000-0000-0000-0000-000000000003", 0, "fad66526-654c-4000-b7b3-30349c41f171", "refuser@imi.be", true, false, false, null, "REFUSER@IMI.BE", "IMIREFUSER", "AQAAAAEAACcQAAAAEL1gUwl9vAdHFwSMJVHECHUVdZjxCDrlUKtX8N6oaRyzV6lSIdTa4+sCwDm37voGjg==", null, false, "YYPCRDAS3MJWQD5CSW2GWPRADBXEZINA", false, "ImiRefuser" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("166135bb-92aa-407d-b07c-20847ccbab75"), "snack" },
                    { new Guid("3e9d01b1-4f05-4730-b72c-a4a2c9227c7c"), "lunch" },
                    { new Guid("63088f18-a5a4-4774-a919-11cb0b59d6f2"), "dinner" },
                    { new Guid("8e23a30d-950a-4fcd-8514-9b4a0bd221cd"), "dessert" },
                    { new Guid("b1c6f419-022e-4a39-9635-eb49610afb1a"), "beverage" },
                    { new Guid("b25380fd-a7db-4af2-81dc-a2f7eedd8616"), "breakfast" },
                    { new Guid("edc0a329-aaa6-4290-8ea9-446844ba4f47"), "side dish" }
                });

            migrationBuilder.InsertData(
                table: "Diets",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("29034587-2814-4d82-ad04-4b92dd7e7a87"), "vegetarian" },
                    { new Guid("4244b52a-dc39-4c24-91e9-4aa15f14ca42"), "keto" },
                    { new Guid("938ea0e0-5336-49e6-b0df-b4aef759dd08"), "vegan" },
                    { new Guid("f0541e43-d655-4572-9c66-9bfba26a626d"), "anything" },
                    { new Guid("fecf1bc5-d3ef-4646-8eb0-3dfd87111b09"), "paleo" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("016d40e9-2717-47ed-aea1-c5c17ea30cf3"), "chili powder" },
                    { new Guid("08136d5c-60e5-4194-9519-71eed7e43c6b"), "smoked paprika" },
                    { new Guid("083f7222-e473-4143-9cb7-e1af84851dd0"), "dried oregano" },
                    { new Guid("1396e93a-41d7-4984-a274-184b77e0afa6"), "bacon" },
                    { new Guid("14ae69a7-6687-4541-b7de-01127e92792b"), "salt" },
                    { new Guid("29d04ae2-aaff-445e-b5ab-88d1c63b4036"), "white sugar" },
                    { new Guid("2be6e6be-2730-49b1-b3a6-d8bcc83ca23d"), "sesame oil" },
                    { new Guid("2cbbdaf1-0d36-482d-80e0-37d3423664c3"), "vanilla ice cream" },
                    { new Guid("31954772-5363-4681-9cdd-88e462fc7617"), "heavy cream" },
                    { new Guid("3261fca9-e1cf-4a67-9428-835bc8b4d2ed"), "cherry tomato" },
                    { new Guid("357effcd-160c-4126-a470-01d5e0109216"), "blackberries" },
                    { new Guid("3ad0ee1f-bddc-4347-9b98-9f5a963a804f"), "egg" },
                    { new Guid("4883e3d8-8418-4111-ae27-e7173bd11a2a"), "celery stalks" },
                    { new Guid("4df4f98a-8dbe-4a84-b58c-5857ec0c2fc4"), "all-purpose flour" },
                    { new Guid("514c7f84-86ca-4698-a64f-afcfc9862ba8"), "peanut butter" },
                    { new Guid("55378261-d8d3-45f8-826f-6d70ebf6caac"), "tuna" },
                    { new Guid("571e7109-ba66-4b22-ad12-71403baa12c5"), "rapeseed oil" },
                    { new Guid("588ac87c-5e0c-408e-a03b-429344f90b48"), "strawberries" },
                    { new Guid("5b23662d-05ac-4f9f-9cea-21a74a199681"), "kalamata olive" },
                    { new Guid("5d9247a4-f9db-4e3d-b861-133bb1d4cbd0"), "celery" },
                    { new Guid("5ef2c63d-11fe-414d-b2a6-744c00f0e63b"), "baking powder" },
                    { new Guid("5eff5316-270d-4e15-b0ef-e383dbd3771a"), "potatoe" },
                    { new Guid("5f928a6f-af9e-4dff-80af-3a2ef3e0deb1"), "maple syrup" },
                    { new Guid("60124aa8-c812-44b7-b766-65b4aca5c549"), "basil" },
                    { new Guid("6210ff3a-3eae-41ab-9b32-f03f9b6ec22a"), "butter" }
                });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("628442fa-5b33-4801-81ff-0e785ff2b276"), "brown rice" },
                    { new Guid("6692d32b-cce3-45f7-8551-6daac00157d7"), "kosher salt" },
                    { new Guid("72c4a267-5cdc-4b9b-9cb8-29046548d981"), "carrots" },
                    { new Guid("7314a433-7ed7-460e-b9c7-9d164c61f173"), "greek yogurt" },
                    { new Guid("745c7145-c67d-417a-bff5-f7d89dcfbc2d"), "almond milk" },
                    { new Guid("75591f19-3a32-48df-9c8e-19b4b0f2920d"), "yellow onion" },
                    { new Guid("77e3f942-11f8-4777-8c22-bdd6a9a6bf20"), "avocado" },
                    { new Guid("7c3e5129-4674-4302-a9c7-445ad1003d0f"), "soy sauce" },
                    { new Guid("7d53c213-5fca-4f8d-8bb1-54cd80d1e013"), "romaine lettuce" },
                    { new Guid("81aacc33-28ff-4134-8e57-a3a389ebd6ba"), "turmeric" },
                    { new Guid("81aba039-743f-4d59-b213-285ba4fd3097"), "milk" },
                    { new Guid("857e4064-ec7f-4a3d-9bb3-98fdde8da19e"), "balsemic vinegar" },
                    { new Guid("8bd1d2e8-1d8c-49ba-ad3d-526dae7cbc30"), "ground cumin" },
                    { new Guid("8f27980d-9715-4ae0-9862-d9a31b463643"), "red onion" },
                    { new Guid("90fd3b57-8529-4ebb-8d20-cb050134659b"), "banana" },
                    { new Guid("97fd73c1-4fa4-4ee3-b74c-7f3f1a575e3d"), "vanilla" },
                    { new Guid("9ff9358d-76ee-4703-895b-d0a1371da3b1"), "whipped topping" },
                    { new Guid("ac4c89ec-3197-459c-b73e-0bae67fe7a45"), "firm tofu" },
                    { new Guid("b1221915-f8a3-498e-995a-94601ea5aa39"), "lemon" },
                    { new Guid("b2ea6a30-2803-4361-ac99-1a06fc4e0f30"), "chocolate chips" },
                    { new Guid("bdc470ec-50b2-4e17-a15f-05449117af4e"), "sprinkles" },
                    { new Guid("c40532ee-a38a-47e9-8898-3cb9e476f16e"), "keto chocolate chips" },
                    { new Guid("ca2199b7-f8dc-4e90-bc70-5f035379f293"), "oat milk" },
                    { new Guid("cae5e798-f7ac-4c7d-9dc6-ba39e90a8343"), "protein powder" },
                    { new Guid("ccc5bd14-e3f0-45c5-9da2-d6bffb143a04"), "raspberries" },
                    { new Guid("d0f9aecb-223b-4f2b-a526-762f133bce19"), "pepper" },
                    { new Guid("d733c8f2-af08-43a8-bd07-66736c165214"), "bell pepper" },
                    { new Guid("dc1acf67-02ce-4195-8501-313f9ca9ca96"), "unsweetened cacao powder" },
                    { new Guid("dcf97f85-7af4-49ff-a89a-27806742abff"), "jalapenos" },
                    { new Guid("ddb921d6-0db4-42d7-b8f9-79aefe6ab7a1"), "nutritional yeast" },
                    { new Guid("e1ad2b89-4a1e-4bce-80c0-811ff9a4c336"), "peas" },
                    { new Guid("ee40da14-1c7d-4986-b06a-d22cdcc84257"), "lime" },
                    { new Guid("f272bf89-f85e-4424-94de-a2e2677ca461"), "green onion" },
                    { new Guid("f2d486d4-3319-4352-b1a2-add18993ed23"), "lean ground beef" },
                    { new Guid("f34d7d0f-4bf0-40bd-bf31-368403138aab"), "honey" },
                    { new Guid("f4e5958b-a80f-445e-8c47-64b01bfb39db"), "green beans" },
                    { new Guid("fbc241d8-d78f-43d9-894b-d8884307b3f5"), "garlic" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("22796b4b-f15d-48ee-89e5-90e6495951b4"), "grams" },
                    { new Guid("2d206a67-fdd4-4ee6-a56b-acd67111f8b1"), "clove" },
                    { new Guid("54722cb6-99f3-4631-b700-e2d310647848"), "piece" },
                    { new Guid("58cae114-6f93-47e0-9fb0-b3a081c8f281"), "block" },
                    { new Guid("65bbef54-0532-4b1a-a32d-eed284c585ea"), "garnish" }
                });

            migrationBuilder.InsertData(
                table: "Units",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("69d2b0cd-8239-4a2d-a8f5-fbb5cd8be3cc"), "ml" },
                    { new Guid("6ca91187-59cf-49ef-bb4a-caf57af5253e"), "tsp" },
                    { new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518"), "tbsp" },
                    { new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10"), "cup" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "UserId" },
                values: new object[,]
                {
                    { 1, "sub", "00000000-0000-0000-0000-000000000001", "00000000-0000-0000-0000-000000000001" },
                    { 2, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "ImiAdmin", "00000000-0000-0000-0000-000000000001" },
                    { 3, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "admin@imi.be", "00000000-0000-0000-0000-000000000001" },
                    { 4, "hasApprovedTerms", "", "00000000-0000-0000-0000-000000000001" },
                    { 5, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "Admin", "00000000-0000-0000-0000-000000000001" },
                    { 6, "sub", "00000000-0000-0000-0000-000000000002", "00000000-0000-0000-0000-000000000002" },
                    { 7, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "ImiUser", "00000000-0000-0000-0000-000000000002" },
                    { 8, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "user@imi.be", "00000000-0000-0000-0000-000000000002" },
                    { 9, "hasApprovedTerms", "True", "00000000-0000-0000-0000-000000000002" },
                    { 10, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "User", "00000000-0000-0000-0000-000000000002" },
                    { 11, "sub", "00000000-0000-0000-0000-000000000003", "00000000-0000-0000-0000-000000000003" },
                    { 12, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "ImiRefuser", "00000000-0000-0000-0000-000000000003" },
                    { 13, "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress", "refuser@imi.be", "00000000-0000-0000-0000-000000000003" },
                    { 14, "hasApprovedTerms", "False", "00000000-0000-0000-0000-000000000003" },
                    { 15, "http://schemas.microsoft.com/ws/2008/06/identity/claims/role", "User", "00000000-0000-0000-0000-000000000003" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "00000000-0000-0000-0000-000000000001", "00000000-0000-0000-0000-000000000001" },
                    { "00000000-0000-0000-0000-000000000002", "00000000-0000-0000-0000-000000000002" },
                    { "00000000-0000-0000-0000-000000000002", "00000000-0000-0000-0000-000000000003" }
                });

            migrationBuilder.InsertData(
                table: "Recipes",
                columns: new[] { "Id", "ApplicationUserId", "CategoryId", "CookTime", "Description", "DietId", "ImgURL", "PrepTime", "Servings", "Title" },
                values: new object[,]
                {
                    { new Guid("23edcc6c-6c39-4ec2-9f2b-e7eb220e0231"), "00000000-0000-0000-0000-000000000002", new Guid("8e23a30d-950a-4fcd-8514-9b4a0bd221cd"), 15, "I found this pancake recipe in my Grandma's recipe book. Judging from the weathered look of this recipe card, this was a family favorite.", new Guid("f0541e43-d655-4572-9c66-9bfba26a626d"), "https://images.unsplash.com/photo-1565299543923-37dd37887442?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=781&q=80", 5, 8, "Good Old-Fashioned Pancakes" },
                    { new Guid("56344d33-e648-4008-a8fc-df24da3d51e3"), "00000000-0000-0000-0000-000000000002", new Guid("b1c6f419-022e-4a39-9635-eb49610afb1a"), 5, "This easy milkshake recipe gives you the perfect ratio of milk to ice cream and is completely customizable! ", new Guid("f0541e43-d655-4572-9c66-9bfba26a626d"), "https://images.unsplash.com/photo-1568901839119-631418a3910d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Nnx8bWlsa3NoYWtlfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60", 5, 1, "Easy Vanilla Milkshake" },
                    { new Guid("6f1720f6-ad8f-48d8-ad08-6edc321e4d7b"), "00000000-0000-0000-0000-000000000003", new Guid("166135bb-92aa-407d-b07c-20847ccbab75"), 5, "If you’re still buying protein bars at the store, this quick and simple vegan protein bar recipe might completely change your life…", new Guid("f0541e43-d655-4572-9c66-9bfba26a626d"), "https://images.unsplash.com/photo-1622484212850-eb596d769edc?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80", 5, 10, "Easy Homemade Protein Bars" },
                    { new Guid("892f98b6-6a36-45ff-8567-f6f229e8cb1b"), "00000000-0000-0000-0000-000000000002", new Guid("3e9d01b1-4f05-4730-b72c-a4a2c9227c7c"), 5, "A fast and healthy lunch, packed with protein, for people in a hurry", new Guid("f0541e43-d655-4572-9c66-9bfba26a626d"), "https://www.wholesomeyum.com/wp-content/uploads/2019/05/wholesomeyum-avocado-tuna-salad-recipe-1.jpg", 5, 2, "Tuna avocado salad" },
                    { new Guid("8e080386-e385-425f-a569-51b83adef1fe"), "00000000-0000-0000-0000-000000000003", new Guid("edc0a329-aaa6-4290-8ea9-446844ba4f47"), 10, "A vegetarian Niçoise salad, that's packed with goodness - fibre, folate, iron, vitamin c and gluten-free too", new Guid("29034587-2814-4d82-ad04-4b92dd7e7a87"), "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80", 10, 2, "Egg Niçoise salad" },
                    { new Guid("9c1f4396-73f3-4d34-b942-0235d9de9228"), "00000000-0000-0000-0000-000000000003", new Guid("b1c6f419-022e-4a39-9635-eb49610afb1a"), 10, "A smoothie containing 3 types of berries", new Guid("f0541e43-d655-4572-9c66-9bfba26a626d"), "https://cookingformysoul.com/wp-content/uploads/2022/05/mixed-berry-smoothie-2-min.jpg", 5, 2, "Tripple berry smoothie" },
                    { new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), "00000000-0000-0000-0000-000000000002", new Guid("3e9d01b1-4f05-4730-b72c-a4a2c9227c7c"), 60, "Easy, 10-ingredient vegan fried rice that’s loaded with vegetables, crispy baked tofu, and tons of flavor! A healthy, satisfying plant-based side dish or entrée.", new Guid("938ea0e0-5336-49e6-b0df-b4aef759dd08"), "https://shortgirltallorder.com/wp-content/uploads/2020/03/veggie-fried-rice-square-4.jpg", 15, 4, "Easy Vegan Fried Rice" },
                    { new Guid("f6099364-c21f-4ea2-abe7-2482fad2e92b"), "00000000-0000-0000-0000-000000000002", new Guid("8e23a30d-950a-4fcd-8514-9b4a0bd221cd"), 60, "This keto mousse gets its rich, creamy texture from avocados.", new Guid("4244b52a-dc39-4c24-91e9-4aa15f14ca42"), "https://thebigmansworld.com/wp-content/uploads/2022/07/keto-chocolate-mousse.jpg", 15, 2, "Keto Chocolate Mousse" },
                    { new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), "00000000-0000-0000-0000-000000000002", new Guid("b25380fd-a7db-4af2-81dc-a2f7eedd8616"), 30, "A healthy vegan breakfast", new Guid("938ea0e0-5336-49e6-b0df-b4aef759dd08"), "https://simpleveganblog.com/wp-content/uploads/2022/07/tofu-scramble-1.jpg", 15, 2, "Scrambled Tofu" },
                    { new Guid("fee8e67f-6f07-49ac-aca8-2b5a45afd334"), "00000000-0000-0000-0000-000000000002", new Guid("63088f18-a5a4-4774-a919-11cb0b59d6f2"), 40, "A hearty bowl of chili is the perfect dinner for a blustery winter day.", new Guid("fecf1bc5-d3ef-4646-8eb0-3dfd87111b09"), "https://www.laurafuentes.com/wp-content/uploads/2013/10/Paleo_chili_recipe-card_1-2.jpg", 15, 2, "Paleo Chili" }
                });

            migrationBuilder.InsertData(
                table: "Instructions",
                columns: new[] { "Id", "Description", "RecipeId", "StepNumber" },
                values: new object[,]
                {
                    { new Guid("061fe121-07af-4f27-8a0a-a2c4de0957f0"), "While the tofu bakes prepare your rice by bringing 12 cups of water to a boil in a large pot. Once boiling, add rinsed rice and stir. Boil on high uncovered for 30 minutes, then strain for 10 seconds and return to pot removed from the heat. Cover with a lid and let steam for 10 minutes*.", new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 4 },
                    { new Guid("0a67885d-5bdd-4985-94c3-ffbae97ce7bf"), "When there is only a small amount of milk remaining, add all of the remaining ingredients and stir for another 3-4 minutes on low to medium heat.", new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), 5 },
                    { new Guid("0b3598a3-1601-4f53-ab2f-f203d526bb14"), "Serve with fresh bread.", new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), 6 },
                    { new Guid("12c9b72c-434d-461b-bb1b-73a8768d7bd5"), "Mix the dressing ingredients together in a small bowl with 1 tbsp water.", new Guid("8e080386-e385-425f-a569-51b83adef1fe"), 1 },
                    { new Guid("1af614c6-c7bc-49cf-ac13-f1684f62021c"), "Preheat oven to 400 degrees F (204 C) and line a baking sheet with parchment paper (or lightly grease with non-stick spray).", new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 1 },
                    { new Guid("1c151eb7-b889-4416-a16c-79fa4754e0cd"), "Add chili powder, cumin, oregano, and paprika and season with salt and pepper. Stir to combine and cook 2 minutes more. Add tomatoes and broth and bring to a simmer. Let cook 10 to 15 more minutes, until chili has thickened slightly. ", new Guid("fee8e67f-6f07-49ac-aca8-2b5a45afd334"), 3 },
                    { new Guid("27c38827-7ee2-485b-b57b-eae1c0f3ad40"), "To the still hot pan add garlic, green onion, peas and carrots. Sauté for 3-4 minutes, stirring occasionally, and season with 1 Tbsp (15 ml) tamari or soy sauce", new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 8 },
                    { new Guid("29c70cc7-5053-49ef-995a-f257a9b8e720"), "Ladle into bowls and top with reserved bacon, jalapeños, cilantro, and avocado.", new Guid("fee8e67f-6f07-49ac-aca8-2b5a45afd334"), 4 },
                    { new Guid("2ba57f48-d658-43de-9d3e-08a2b30c3427"), "Sift flour, baking powder, sugar, and salt together in a large bowl. Make a well in the center and add milk, melted butter, and egg; mix until smooth.", new Guid("23edcc6c-6c39-4ec2-9f2b-e7eb220e0231"), 1 },
                    { new Guid("31060047-f3f4-4d40-a760-5390f59d18cd"), "In a blender, blend together ice cream and milk.", new Guid("56344d33-e648-4008-a8fc-df24da3d51e3"), 1 },
                    { new Guid("31a801b9-0287-409a-b115-b1a5a7fec055"), "Pour into a glass and garnish with whipped topping, sprinkles, cacao powder and a cherry.", new Guid("56344d33-e648-4008-a8fc-df24da3d51e3"), 2 },
                    { new Guid("323b44b6-9b2c-47be-8164-c92e67fc7ea0"), "Heat a lightly oiled griddle or pan over medium-high heat. Pour or scoop the batter onto the griddle, using approximately 1/4 cup for each pancake; cook until bubbles form and the edges are dry, about 2 to 3 minutes. Flip and cook until browned on the other side. Repeat with remaining batter.", new Guid("23edcc6c-6c39-4ec2-9f2b-e7eb220e0231"), 2 },
                    { new Guid("4f151684-06d7-4ae6-81c0-651decd07f0a"), "In a blender, combine all ingredients and blend until smooth.", new Guid("9c1f4396-73f3-4d34-b942-0235d9de9228"), 1 },
                    { new Guid("524df98d-c787-48cd-9964-962efc975bec"), "Add the tuna, cilantro, red onion, celery, and jalapeños (if using). Stir everything together, breaking apart any large pieces of tuna as needed.", new Guid("892f98b6-6a36-45ff-8567-f6f229e8cb1b"), 2 },
                    { new Guid("59a79981-92a8-4f10-91c7-86916dcf859f"), "Heat a large metal or cast iron skillet over medium heat. Once hot, use a slotted spoon to scoop the tofu into the pan leaving most of the sauce behind. Cook for 3-4 minutes, stirring occasionally, until deep golden brown on all sides (see photo). Lower heat if browning too quickly. Remove from pan and set aside.", new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 7 },
                    { new Guid("6a06bb53-15f5-4605-873d-d4a36efb9b26"), "Transfer to serving glasses and refrigerate 30 minutes and up to 1 hour. Garnish with chocolate curls if using.", new Guid("f6099364-c21f-4ea2-abe7-2482fad2e92b"), 2 },
                    { new Guid("7ad04e69-1c59-48cc-a977-b990f72b4644"), "Scramble the block of tofu with your hands (see picture above) into small and bigger pieces.", new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), 1 },
                    { new Guid("8241d7e8-dbe8-48e3-b04f-596f1cb7853b"), "Divide between 2 cups and top with blackberries, if desired.", new Guid("9c1f4396-73f3-4d34-b942-0235d9de9228"), 2 },
                    { new Guid("983b31fd-481d-417b-8e98-ccbd053cdffd"), "Heat 1 tablespoon of oil on medium heat in a pan. Caramelize the chopped red onions", new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), 2 },
                    { new Guid("9a6c8a81-036a-47e7-ae4f-594c357f73ae"), "Add the scrambled tofu and stir for 1 minute.", new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), 3 },
                    { new Guid("9dfe54d1-0f9b-4deb-a7e1-0a6f51487b18"), "In a large pot over medium heat, cook bacon. When bacon is crisp, remove from pot with a slotted spoon. Add onion, celery, and peppers to pot and cook until soft, 6 minutes. Add garlic and cook until fragrant, 1 minute more.", new Guid("fee8e67f-6f07-49ac-aca8-2b5a45afd334"), 1 },
                    { new Guid("9f6a91ec-12f3-49bc-a134-352a1438cdc1"), "While rice and tofu are cooking, prepare sauce by adding all ingredients to a medium-size mixing bowl and whisking to combine. Taste and adjust flavor as needed, adding more tamari or soy sauce for saltiness, peanut butter for creaminess, brown sugar for sweetness, or chili garlic sauce for heat.", new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 5 },
                    { new Guid("a5ad88cc-c9ab-4099-b0d1-5fe1ed39c5f4"), "Toss the beans, potatoes and remaining salad ingredients, except the eggs, together in a large bowl with half the dressing. Arrange the eggs on top and drizzle over the remaining dressing.", new Guid("8e080386-e385-425f-a569-51b83adef1fe"), 3 },
                    { new Guid("a97d8682-82a5-47d4-a3df-49a14e8ca057"), "Push vegetables to one side of the pan and add beef. Cook, stirring occasionally, until no pink remains. Drain fat and return to heat.", new Guid("fee8e67f-6f07-49ac-aca8-2b5a45afd334"), 2 },
                    { new Guid("c4470b86-9b36-46ba-95e8-c2cb293567a3"), "Add the 1/2 cup of oat milk and stir until the tofu has soaked up most of it.", new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), 4 },
                    { new Guid("d92e49c8-c754-44ca-b400-b00da69671bc"), "Meanwhile boil the potatoes for 7 mins, add the beans and boil 5 mins more or until both are just tender, then drain. Boil 2 eggs for 8 minutes then shell and halve.", new Guid("8e080386-e385-425f-a569-51b83adef1fe"), 2 },
                    { new Guid("df35ca17-b4f3-49d3-a771-edac2757edbc"), "Stir all ingredients except optional chips to form a dough. Either shape into bars with your hands or smooth into a lined 8×8 pan, refrigerate until chilled, then cut into bars.", new Guid("6f1720f6-ad8f-48d8-ad08-6edc321e4d7b"), 1 },
                    { new Guid("e0efc42f-08b0-4093-94f7-e19fbcbc07a2"), "Mash the avocado and lime juice together with sea salt.", new Guid("892f98b6-6a36-45ff-8567-f6f229e8cb1b"), 1 },
                    { new Guid("e36cce47-1720-4233-9a4a-461280a94b44"), "In the meantime wrap tofu in a clean, absorbent towel and set something heavy on top (such as a cast iron skillet) to press out the liquid.", new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 2 },
                    { new Guid("e54ff64d-c0f2-4d14-8604-8b9f04c6b445"), "For the optional chocolate coating, spread the melted chocolate over the pan before chilling. (I usually stir 2 tsp oil into the melted chocolate for a smoother sauce, but it's not required.) Or you can dip the bars into the chocolate sauce individually and then chill to set.", new Guid("6f1720f6-ad8f-48d8-ad08-6edc321e4d7b"), 2 },
                    { new Guid("e68b9260-2c8c-41a4-8589-a968684fb57a"), "Once the tofu is done baking, add directly to the sauce and marinate for 5 minutes, stirring occasionally", new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 6 },
                    { new Guid("e96b1887-f227-4577-8d5c-6f359b68da20"), "Once the oven is preheated, dice tofu into 1/4-inch cubes and arrange on baking sheet. Bake for 26-30 minutes. You’re looking for golden brown edges and a texture that’s firm to the touch. The longer it bakes, the firmer and crispier it will become, so if you’re looking for softer tofu remove from the oven around the 26-28 minute mark. I prefer crispy tofu, so I bake mine the full 30 minutes. Set aside.", new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 3 },
                    { new Guid("f6c9014f-b374-49b7-bf28-039b04a6f4c9"), "In a food processor or blender, blend all ingredients except chocolate curls until smooth.", new Guid("f6099364-c21f-4ea2-abe7-2482fad2e92b"), 1 },
                    { new Guid("f9ac1a94-0364-4f76-a823-78f1ae600983"), "Adjust salt and jalapeños to taste if needed. Serve immediately.", new Guid("892f98b6-6a36-45ff-8567-f6f229e8cb1b"), 3 },
                    { new Guid("f9d7398f-5c6a-46d2-aead-cebaa6ebb68d"), "Add cooked rice, tofu, and remaining sauce and stir. Cook over medium-high heat for 3-4 minutes, stirring frequently.", new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 9 },
                    { new Guid("fe0f2b73-499b-487a-91ad-d693ff046408"), "Serve immediately with extra chili garlic sauce or sriracha for heat (optional). Crushed salted, roasted peanuts or cashews make a lovely additional garnish. Leftovers keep well in the refrigerator for 3-4 days, though best when fresh. Reheat in a skillet over medium heat or in the microwave.", new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 10 }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "IngredientId", "RecipeId", "Amount", "UnitId" },
                values: new object[,]
                {
                    { new Guid("016d40e9-2717-47ed-aea1-c5c17ea30cf3"), new Guid("fee8e67f-6f07-49ac-aca8-2b5a45afd334"), 2.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("08136d5c-60e5-4194-9519-71eed7e43c6b"), new Guid("fee8e67f-6f07-49ac-aca8-2b5a45afd334"), 2.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("083f7222-e473-4143-9cb7-e1af84851dd0"), new Guid("fee8e67f-6f07-49ac-aca8-2b5a45afd334"), 2.0, new Guid("6ca91187-59cf-49ef-bb4a-caf57af5253e") },
                    { new Guid("1396e93a-41d7-4984-a274-184b77e0afa6"), new Guid("fee8e67f-6f07-49ac-aca8-2b5a45afd334"), 1.0, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("14ae69a7-6687-4541-b7de-01127e92792b"), new Guid("23edcc6c-6c39-4ec2-9f2b-e7eb220e0231"), 0.25, new Guid("6ca91187-59cf-49ef-bb4a-caf57af5253e") },
                    { new Guid("14ae69a7-6687-4541-b7de-01127e92792b"), new Guid("6f1720f6-ad8f-48d8-ad08-6edc321e4d7b"), 0.5, new Guid("6ca91187-59cf-49ef-bb4a-caf57af5253e") }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "IngredientId", "RecipeId", "Amount", "UnitId" },
                values: new object[,]
                {
                    { new Guid("14ae69a7-6687-4541-b7de-01127e92792b"), new Guid("892f98b6-6a36-45ff-8567-f6f229e8cb1b"), 0.5, new Guid("6ca91187-59cf-49ef-bb4a-caf57af5253e") },
                    { new Guid("29d04ae2-aaff-445e-b5ab-88d1c63b4036"), new Guid("23edcc6c-6c39-4ec2-9f2b-e7eb220e0231"), 1.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("2be6e6be-2730-49b1-b3a6-d8bcc83ca23d"), new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 1.0, new Guid("6ca91187-59cf-49ef-bb4a-caf57af5253e") },
                    { new Guid("2cbbdaf1-0d36-482d-80e0-37d3423664c3"), new Guid("56344d33-e648-4008-a8fc-df24da3d51e3"), 1.5, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("31954772-5363-4681-9cdd-88e462fc7617"), new Guid("f6099364-c21f-4ea2-abe7-2482fad2e92b"), 0.75, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("357effcd-160c-4126-a470-01d5e0109216"), new Guid("9c1f4396-73f3-4d34-b942-0235d9de9228"), 150.0, new Guid("22796b4b-f15d-48ee-89e5-90e6495951b4") },
                    { new Guid("3ad0ee1f-bddc-4347-9b98-9f5a963a804f"), new Guid("23edcc6c-6c39-4ec2-9f2b-e7eb220e0231"), 1.0, new Guid("54722cb6-99f3-4631-b700-e2d310647848") },
                    { new Guid("3ad0ee1f-bddc-4347-9b98-9f5a963a804f"), new Guid("8e080386-e385-425f-a569-51b83adef1fe"), 2.0, new Guid("54722cb6-99f3-4631-b700-e2d310647848") },
                    { new Guid("4883e3d8-8418-4111-ae27-e7173bd11a2a"), new Guid("fee8e67f-6f07-49ac-aca8-2b5a45afd334"), 2.0, new Guid("54722cb6-99f3-4631-b700-e2d310647848") },
                    { new Guid("4df4f98a-8dbe-4a84-b58c-5857ec0c2fc4"), new Guid("23edcc6c-6c39-4ec2-9f2b-e7eb220e0231"), 1.5, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("514c7f84-86ca-4698-a64f-afcfc9862ba8"), new Guid("6f1720f6-ad8f-48d8-ad08-6edc321e4d7b"), 1.5, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("514c7f84-86ca-4698-a64f-afcfc9862ba8"), new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 1.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("55378261-d8d3-45f8-826f-6d70ebf6caac"), new Guid("892f98b6-6a36-45ff-8567-f6f229e8cb1b"), 140.0, new Guid("22796b4b-f15d-48ee-89e5-90e6495951b4") },
                    { new Guid("571e7109-ba66-4b22-ad12-71403baa12c5"), new Guid("8e080386-e385-425f-a569-51b83adef1fe"), 2.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("588ac87c-5e0c-408e-a03b-429344f90b48"), new Guid("9c1f4396-73f3-4d34-b942-0235d9de9228"), 150.0, new Guid("22796b4b-f15d-48ee-89e5-90e6495951b4") },
                    { new Guid("5b23662d-05ac-4f9f-9cea-21a74a199681"), new Guid("8e080386-e385-425f-a569-51b83adef1fe"), 6.0, new Guid("54722cb6-99f3-4631-b700-e2d310647848") },
                    { new Guid("5d9247a4-f9db-4e3d-b861-133bb1d4cbd0"), new Guid("892f98b6-6a36-45ff-8567-f6f229e8cb1b"), 3.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("5ef2c63d-11fe-414d-b2a6-744c00f0e63b"), new Guid("23edcc6c-6c39-4ec2-9f2b-e7eb220e0231"), 3.5, new Guid("6ca91187-59cf-49ef-bb4a-caf57af5253e") },
                    { new Guid("5eff5316-270d-4e15-b0ef-e383dbd3771a"), new Guid("8e080386-e385-425f-a569-51b83adef1fe"), 250.0, new Guid("22796b4b-f15d-48ee-89e5-90e6495951b4") },
                    { new Guid("5f928a6f-af9e-4dff-80af-3a2ef3e0deb1"), new Guid("23edcc6c-6c39-4ec2-9f2b-e7eb220e0231"), 1.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("5f928a6f-af9e-4dff-80af-3a2ef3e0deb1"), new Guid("6f1720f6-ad8f-48d8-ad08-6edc321e4d7b"), 0.25, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("5f928a6f-af9e-4dff-80af-3a2ef3e0deb1"), new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 2.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("60124aa8-c812-44b7-b766-65b4aca5c549"), new Guid("8e080386-e385-425f-a569-51b83adef1fe"), 0.0, new Guid("65bbef54-0532-4b1a-a32d-eed284c585ea") },
                    { new Guid("6210ff3a-3eae-41ab-9b32-f03f9b6ec22a"), new Guid("23edcc6c-6c39-4ec2-9f2b-e7eb220e0231"), 3.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("628442fa-5b33-4801-81ff-0e785ff2b276"), new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 1.0, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("6692d32b-cce3-45f7-8551-6daac00157d7"), new Guid("f6099364-c21f-4ea2-abe7-2482fad2e92b"), 0.5, new Guid("6ca91187-59cf-49ef-bb4a-caf57af5253e") },
                    { new Guid("72c4a267-5cdc-4b9b-9cb8-29046548d981"), new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 0.5, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("7314a433-7ed7-460e-b9c7-9d164c61f173"), new Guid("9c1f4396-73f3-4d34-b942-0235d9de9228"), 200.0, new Guid("22796b4b-f15d-48ee-89e5-90e6495951b4") },
                    { new Guid("745c7145-c67d-417a-bff5-f7d89dcfbc2d"), new Guid("9c1f4396-73f3-4d34-b942-0235d9de9228"), 250.0, new Guid("69d2b0cd-8239-4a2d-a8f5-fbb5cd8be3cc") },
                    { new Guid("75591f19-3a32-48df-9c8e-19b4b0f2920d"), new Guid("fee8e67f-6f07-49ac-aca8-2b5a45afd334"), 0.5, new Guid("54722cb6-99f3-4631-b700-e2d310647848") },
                    { new Guid("77e3f942-11f8-4777-8c22-bdd6a9a6bf20"), new Guid("892f98b6-6a36-45ff-8567-f6f229e8cb1b"), 2.0, new Guid("54722cb6-99f3-4631-b700-e2d310647848") },
                    { new Guid("77e3f942-11f8-4777-8c22-bdd6a9a6bf20"), new Guid("f6099364-c21f-4ea2-abe7-2482fad2e92b"), 2.0, new Guid("54722cb6-99f3-4631-b700-e2d310647848") },
                    { new Guid("7c3e5129-4674-4302-a9c7-445ad1003d0f"), new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 3.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("7d53c213-5fca-4f8d-8bb1-54cd80d1e013"), new Guid("8e080386-e385-425f-a569-51b83adef1fe"), 15.0, new Guid("22796b4b-f15d-48ee-89e5-90e6495951b4") },
                    { new Guid("81aacc33-28ff-4134-8e57-a3a389ebd6ba"), new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), 0.75, new Guid("6ca91187-59cf-49ef-bb4a-caf57af5253e") },
                    { new Guid("81aba039-743f-4d59-b213-285ba4fd3097"), new Guid("23edcc6c-6c39-4ec2-9f2b-e7eb220e0231"), 1.25, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("81aba039-743f-4d59-b213-285ba4fd3097"), new Guid("56344d33-e648-4008-a8fc-df24da3d51e3"), 1.5, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("857e4064-ec7f-4a3d-9bb3-98fdde8da19e"), new Guid("8e080386-e385-425f-a569-51b83adef1fe"), 1.0, new Guid("6ca91187-59cf-49ef-bb4a-caf57af5253e") },
                    { new Guid("8bd1d2e8-1d8c-49ba-ad3d-526dae7cbc30"), new Guid("fee8e67f-6f07-49ac-aca8-2b5a45afd334"), 2.0, new Guid("6ca91187-59cf-49ef-bb4a-caf57af5253e") },
                    { new Guid("8f27980d-9715-4ae0-9862-d9a31b463643"), new Guid("892f98b6-6a36-45ff-8567-f6f229e8cb1b"), 3.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("8f27980d-9715-4ae0-9862-d9a31b463643"), new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), 0.5, new Guid("54722cb6-99f3-4631-b700-e2d310647848") },
                    { new Guid("90fd3b57-8529-4ebb-8d20-cb050134659b"), new Guid("9c1f4396-73f3-4d34-b942-0235d9de9228"), 1.0, new Guid("54722cb6-99f3-4631-b700-e2d310647848") }
                });

            migrationBuilder.InsertData(
                table: "RecipeIngredients",
                columns: new[] { "IngredientId", "RecipeId", "Amount", "UnitId" },
                values: new object[,]
                {
                    { new Guid("97fd73c1-4fa4-4ee3-b74c-7f3f1a575e3d"), new Guid("f6099364-c21f-4ea2-abe7-2482fad2e92b"), 1.0, new Guid("6ca91187-59cf-49ef-bb4a-caf57af5253e") },
                    { new Guid("9ff9358d-76ee-4703-895b-d0a1371da3b1"), new Guid("56344d33-e648-4008-a8fc-df24da3d51e3"), 0.0, new Guid("65bbef54-0532-4b1a-a32d-eed284c585ea") },
                    { new Guid("ac4c89ec-3197-459c-b73e-0bae67fe7a45"), new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 1.0, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("ac4c89ec-3197-459c-b73e-0bae67fe7a45"), new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), 1.0, new Guid("58cae114-6f93-47e0-9fb0-b3a081c8f281") },
                    { new Guid("b1221915-f8a3-498e-995a-94601ea5aa39"), new Guid("8e080386-e385-425f-a569-51b83adef1fe"), 1.0, new Guid("54722cb6-99f3-4631-b700-e2d310647848") },
                    { new Guid("b2ea6a30-2803-4361-ac99-1a06fc4e0f30"), new Guid("6f1720f6-ad8f-48d8-ad08-6edc321e4d7b"), 35.0, new Guid("22796b4b-f15d-48ee-89e5-90e6495951b4") },
                    { new Guid("bdc470ec-50b2-4e17-a15f-05449117af4e"), new Guid("56344d33-e648-4008-a8fc-df24da3d51e3"), 0.0, new Guid("65bbef54-0532-4b1a-a32d-eed284c585ea") },
                    { new Guid("c40532ee-a38a-47e9-8898-3cb9e476f16e"), new Guid("f6099364-c21f-4ea2-abe7-2482fad2e92b"), 0.5, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("ca2199b7-f8dc-4e90-bc70-5f035379f293"), new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), 0.5, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("cae5e798-f7ac-4c7d-9dc6-ba39e90a8343"), new Guid("6f1720f6-ad8f-48d8-ad08-6edc321e4d7b"), 0.75, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("ccc5bd14-e3f0-45c5-9da2-d6bffb143a04"), new Guid("9c1f4396-73f3-4d34-b942-0235d9de9228"), 150.0, new Guid("22796b4b-f15d-48ee-89e5-90e6495951b4") },
                    { new Guid("d733c8f2-af08-43a8-bd07-66736c165214"), new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), 0.5, new Guid("54722cb6-99f3-4631-b700-e2d310647848") },
                    { new Guid("dc1acf67-02ce-4195-8501-313f9ca9ca96"), new Guid("56344d33-e648-4008-a8fc-df24da3d51e3"), 0.5, new Guid("6ca91187-59cf-49ef-bb4a-caf57af5253e") },
                    { new Guid("dc1acf67-02ce-4195-8501-313f9ca9ca96"), new Guid("f6099364-c21f-4ea2-abe7-2482fad2e92b"), 3.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("dcf97f85-7af4-49ff-a89a-27806742abff"), new Guid("892f98b6-6a36-45ff-8567-f6f229e8cb1b"), 1.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("ddb921d6-0db4-42d7-b8f9-79aefe6ab7a1"), new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), 1.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("e1ad2b89-4a1e-4bce-80c0-811ff9a4c336"), new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 0.5, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("ee40da14-1c7d-4986-b06a-d22cdcc84257"), new Guid("892f98b6-6a36-45ff-8567-f6f229e8cb1b"), 2.0, new Guid("872ecfad-ed7e-4905-8395-fcc6fbdd9518") },
                    { new Guid("f272bf89-f85e-4424-94de-a2e2677ca461"), new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 1.0, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("f2d486d4-3319-4352-b1a2-add18993ed23"), new Guid("fee8e67f-6f07-49ac-aca8-2b5a45afd334"), 0.5, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("f34d7d0f-4bf0-40bd-bf31-368403138aab"), new Guid("f6099364-c21f-4ea2-abe7-2482fad2e92b"), 0.25, new Guid("894cd115-5526-45a1-b0e2-2cd412d76a10") },
                    { new Guid("f4e5958b-a80f-445e-8c47-64b01bfb39db"), new Guid("8e080386-e385-425f-a569-51b83adef1fe"), 200.0, new Guid("22796b4b-f15d-48ee-89e5-90e6495951b4") },
                    { new Guid("fbc241d8-d78f-43d9-894b-d8884307b3f5"), new Guid("8e080386-e385-425f-a569-51b83adef1fe"), 1.0, new Guid("2d206a67-fdd4-4ee6-a56b-acd67111f8b1") },
                    { new Guid("fbc241d8-d78f-43d9-894b-d8884307b3f5"), new Guid("a5dd677b-e134-45c8-a5d8-f3dc65a528e2"), 4.0, new Guid("2d206a67-fdd4-4ee6-a56b-acd67111f8b1") }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "ApplicationUserId", "Comment", "CreationDate", "Rating", "RecipeId", "UpdateTimeStamp" },
                values: new object[,]
                {
                    { new Guid("145c080f-b26f-426e-ac4d-8ea6122ae106"), "00000000-0000-0000-0000-000000000003", "Great recipe!", new DateTime(2023, 7, 14, 9, 57, 6, 855, DateTimeKind.Local).AddTicks(2239), 4, new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), new DateTime(2023, 7, 14, 9, 57, 6, 855, DateTimeKind.Local).AddTicks(2275) },
                    { new Guid("c13854c3-313a-451d-9a1f-42ab1ffcc86c"), "00000000-0000-0000-0000-000000000002", "Definitely will eat this again!", new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, new Guid("9c1f4396-73f3-4d34-b942-0235d9de9228"), new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("c987942a-8972-4490-84fe-1e9390bf24e6"), "00000000-0000-0000-0000-000000000002", null, new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, new Guid("9c1f4396-73f3-4d34-b942-0235d9de9228"), new DateTime(2022, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { new Guid("fba3ced0-ba3a-47fa-8a3a-380e4a8256a2"), "00000000-0000-0000-0000-000000000003", "Didn't taste good", new DateTime(2022, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, new Guid("fbc9515e-7593-450c-b409-018ea0cd7603"), new DateTime(2022, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified) }
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
                name: "IX_FavoriteRecipes_ApplicationUserId",
                table: "FavoriteRecipes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_RecipeId",
                table: "Instructions",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_StepNumber_RecipeId",
                table: "Instructions",
                columns: new[] { "StepNumber", "RecipeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_RecipeId",
                table: "RecipeIngredients",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeIngredients_UnitId",
                table: "RecipeIngredients",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ApplicationUserId",
                table: "Recipes",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CategoryId",
                table: "Recipes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_DietId",
                table: "Recipes",
                column: "DietId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ApplicationUserId",
                table: "Reviews",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_RecipeId",
                table: "Reviews",
                column: "RecipeId");
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
                name: "FavoriteRecipes");

            migrationBuilder.DropTable(
                name: "Instructions");

            migrationBuilder.DropTable(
                name: "RecipeIngredients");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Diets");
        }
    }
}
