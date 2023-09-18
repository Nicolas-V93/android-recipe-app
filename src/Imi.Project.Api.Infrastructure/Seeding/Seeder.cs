using Imi.Project.Api.Core.Entities;
using Imi.Project.Api.Core.Enums;
using Imi.Project.Api.Core.Helpers.CustomClaimTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Imi.Project.Api.Infrastructure.Seeding
{
    public static class Seeder
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Roles

            const string AdminRoleId = "00000000-0000-0000-0000-000000000001";
            const string AdminRoleName = "Admin";

            const string UserRoleId = "00000000-0000-0000-0000-000000000002";
            const string UserRoleName = "User";

            var adminRole = new IdentityRole { Id = AdminRoleId, Name = AdminRoleName, NormalizedName = AdminRoleName.ToUpper() };
            var userRole = new IdentityRole { Id = UserRoleId, Name = UserRoleName, NormalizedName = UserRoleName.ToUpper() };

            IEnumerable<IdentityRole> roles = new List<IdentityRole>
            {
                adminRole, userRole
            };

            #endregion

            #region Users

            IPasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();


            // Admin
            const string AdminUserId = "00000000-0000-0000-0000-000000000001";
            const string AdminUserName = "ImiAdmin";
            const string AdminEmail = "admin@imi.be";
            const string AdminUserPassword = "Test123?";

            ApplicationUser adminApplicationUser = new ApplicationUser
            {
                Id = AdminUserId,
                UserName = AdminUserName,
                NormalizedUserName = AdminUserName.ToUpper(),
                Email = AdminEmail,
                NormalizedEmail = AdminEmail.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = "VVPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                ConcurrencyStamp = "c8554266-b401-4519-9aeb-a9283053fc58",
                HasApprovedTerms = null
            };
            adminApplicationUser.PasswordHash = passwordHasher.HashPassword(adminApplicationUser, AdminUserPassword);


            // ImiUser
            const string ImiUserId = "00000000-0000-0000-0000-000000000002";
            const string ImiUserName = "ImiUser";
            const string ImiUserEmail = "user@imi.be";
            const string ImiUserPassword = "Test123?";

            ApplicationUser imiUser = new ApplicationUser
            {
                Id = ImiUserId,
                UserName = ImiUserName,
                NormalizedUserName = ImiUserName.ToUpper(),
                Email = ImiUserEmail,
                NormalizedEmail = ImiUserEmail.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = "WWPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                ConcurrencyStamp = "2b3ba136-4654-49c1-8329-1e266a1f7453",
                HasApprovedTerms = true
            };
            imiUser.PasswordHash = passwordHasher.HashPassword(imiUser, ImiUserPassword);

            // ImiRefuser
            const string ImiRefuserId = "00000000-0000-0000-0000-000000000003";
            const string ImiRefuserName = "ImiRefuser";
            const string ImiRefuserEmail = "refuser@imi.be";
            const string ImiRefuserPassword = "Test123?";

            ApplicationUser imiRefuser = new ApplicationUser
            {
                Id = ImiRefuserId,
                UserName = ImiRefuserName,
                NormalizedUserName = ImiRefuserName.ToUpper(),
                Email = ImiRefuserEmail,
                NormalizedEmail = ImiRefuserEmail.ToUpper(),
                EmailConfirmed = true,
                SecurityStamp = "YYPCRDAS3MJWQD5CSW2GWPRADBXEZINA",
                ConcurrencyStamp = "fad66526-654c-4000-b7b3-30349c41f171",
                HasApprovedTerms = false
            };
            imiRefuser.PasswordHash = passwordHasher.HashPassword(imiRefuser, ImiRefuserPassword);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = AdminRoleId, UserId = AdminUserId });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = UserRoleId, UserId = ImiUserId });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { RoleId = UserRoleId, UserId = ImiRefuserId });


            IEnumerable<ApplicationUser> users = new List<ApplicationUser>()
            { adminApplicationUser, imiUser, imiRefuser };
            #endregion

            #region claims
            var adminClaims = new List<IdentityUserClaim<string>>
            {
                new IdentityUserClaim<string> { Id=1, UserId = AdminUserId, ClaimType = CustomClaimType.Sub, ClaimValue = AdminUserId },
                new IdentityUserClaim<string> { Id=2, UserId = AdminUserId, ClaimType = ClaimTypes.Name, ClaimValue = AdminUserName },
                new IdentityUserClaim<string> { Id=3, UserId = AdminUserId, ClaimType = ClaimTypes.Email, ClaimValue = AdminEmail },
                new IdentityUserClaim<string> { Id=4, UserId = AdminUserId, ClaimType = CustomClaimType.HasApprovedTerms, ClaimValue = adminApplicationUser.HasApprovedTerms.ToString() },
                new IdentityUserClaim<string> { Id=5, UserId = AdminUserId, ClaimType = ClaimTypes.Role, ClaimValue = AdminRoleName },
            };
            var imiUserClaims = new List<IdentityUserClaim<string>>()
            {
                new IdentityUserClaim<string> { Id=6, UserId = ImiUserId, ClaimType = CustomClaimType.Sub, ClaimValue = ImiUserId },
                new IdentityUserClaim<string> { Id=7, UserId = ImiUserId, ClaimType = ClaimTypes.Name, ClaimValue = ImiUserName },
                new IdentityUserClaim<string> { Id=8, UserId = ImiUserId, ClaimType = ClaimTypes.Email, ClaimValue = ImiUserEmail },
                new IdentityUserClaim<string> { Id=9, UserId = ImiUserId, ClaimType = CustomClaimType.HasApprovedTerms, ClaimValue = imiUser.HasApprovedTerms.ToString() },
                new IdentityUserClaim<string> { Id=10, UserId = ImiUserId, ClaimType = ClaimTypes.Role, ClaimValue = UserRoleName },
            };
            var imiRefuserClaims = new List<IdentityUserClaim<string>>()
            {
                new IdentityUserClaim<string> { Id=11, UserId = ImiRefuserId, ClaimType = CustomClaimType.Sub, ClaimValue = ImiRefuserId },
                new IdentityUserClaim<string> { Id=12, UserId = ImiRefuserId, ClaimType = ClaimTypes.Name, ClaimValue = ImiRefuserName },
                new IdentityUserClaim<string> { Id=13, UserId = ImiRefuserId, ClaimType = ClaimTypes.Email, ClaimValue = ImiRefuserEmail },
                new IdentityUserClaim<string> { Id=14, UserId = ImiRefuserId, ClaimType = CustomClaimType.HasApprovedTerms, ClaimValue = imiRefuser.HasApprovedTerms.ToString() },
                new IdentityUserClaim<string> { Id=15, UserId = ImiRefuserId, ClaimType = ClaimTypes.Role, ClaimValue = UserRoleName },
            };

            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(adminClaims);
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(imiUserClaims);
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(imiRefuserClaims);

            #endregion



            IEnumerable<Category> categories = new List<Category>()
            {
                new Category { Id = Guid.NewGuid(), Name = "breakfast"},
                new Category { Id = Guid.NewGuid(), Name = "lunch"},
                new Category { Id = Guid.NewGuid(), Name = "dinner"},
                new Category { Id = Guid.NewGuid(), Name = "snack"},
                new Category { Id = Guid.NewGuid(), Name = "side dish"},
                new Category { Id = Guid.NewGuid(), Name = "beverage"},
                new Category { Id = Guid.NewGuid(), Name = "dessert"}
            };
            IEnumerable<Diet> diets = new List<Diet>()
            {
                new Diet { Id = Guid.NewGuid(), Name = "anything"},
                new Diet { Id = Guid.NewGuid(), Name = "vegetarian"},
                new Diet { Id = Guid.NewGuid(), Name = "vegan"},
                new Diet { Id = Guid.NewGuid(), Name = "paleo"},
                new Diet { Id = Guid.NewGuid(), Name = "keto"},
            };

            var grams = new Unit { Id = Guid.NewGuid(), Name = UnitType.grams };
            var ml = new Unit { Id = Guid.NewGuid(), Name = UnitType.ml };
            var tbsp = new Unit { Id = Guid.NewGuid(), Name = UnitType.tbsp };
            var tsp = new Unit { Id = Guid.NewGuid(), Name = UnitType.tsp };
            var piece = new Unit { Id = Guid.NewGuid(), Name = UnitType.piece };
            var block = new Unit { Id = Guid.NewGuid(), Name = UnitType.block };
            var cup = new Unit { Id = Guid.NewGuid(), Name = UnitType.cup };
            var garnish = new Unit { Id = Guid.NewGuid(), Name = UnitType.garnish };
            var clove = new Unit { Id = Guid.NewGuid(), Name = UnitType.clove };

            IEnumerable<Unit> units = new List<Unit>()
            {
               grams, ml, tbsp, tsp, piece, block, cup, garnish, clove
            };


            IEnumerable<Ingredient> ingredients = new List<Ingredient>()
            {
                // General ingredients
                new Ingredient { Id = Guid.NewGuid(), Name = "red onion"},
                new Ingredient { Id = Guid.NewGuid(), Name = "salt"},
                new Ingredient { Id = Guid.NewGuid(), Name = "pepper"},
                new Ingredient { Id = Guid.NewGuid(), Name = "avocado"},
                new Ingredient { Id = Guid.NewGuid(), Name = "vanilla"},
                new Ingredient { Id = Guid.NewGuid(), Name = "kosher salt"},
                new Ingredient { Id = Guid.NewGuid(), Name = "unsweetened cacao powder"},
                new Ingredient { Id = Guid.NewGuid(), Name = "bell pepper"},
                new Ingredient { Id = Guid.NewGuid(), Name = "garlic"},
                new Ingredient { Id = Guid.NewGuid(), Name = "chili powder"},
                new Ingredient { Id = Guid.NewGuid(), Name = "ground cumin"},
                new Ingredient { Id = Guid.NewGuid(), Name = "dried oregano"},
                new Ingredient { Id = Guid.NewGuid(), Name = "smoked paprika"},

                // Ingredients for Tofu Scramble
                // https://thegreenloot.com/vegan-scrambled-eggs-tofu/
                new Ingredient { Id = Guid.NewGuid(), Name = "firm tofu"},
                new Ingredient { Id = Guid.NewGuid(), Name = "oat milk"},
                new Ingredient { Id = Guid.NewGuid(), Name = "turmeric"},
                new Ingredient { Id = Guid.NewGuid(), Name = "nutritional yeast"},

                // Ingredients for Smoothie
                // https://www.delish.com/cooking/recipe-ideas/a24892347/how-to-make-a-smoothie/
                new Ingredient { Id = Guid.NewGuid(), Name = "banana"},
                new Ingredient { Id = Guid.NewGuid(), Name = "almond milk"},
                new Ingredient { Id = Guid.NewGuid(), Name = "greek yogurt"},
                new Ingredient { Id = Guid.NewGuid(), Name = "strawberries"},
                new Ingredient { Id = Guid.NewGuid(), Name = "raspberries"},
                new Ingredient { Id = Guid.NewGuid(), Name = "blackberries"},

                // Ingredients for Tuna Avocado Salad
                // https://www.wholesomeyum.com/avocado-tuna-salad-recipe/
                new Ingredient { Id = Guid.NewGuid(), Name = "tuna"},
                new Ingredient { Id = Guid.NewGuid(), Name = "lime"},
                new Ingredient { Id = Guid.NewGuid(), Name = "celery"},
                new Ingredient { Id = Guid.NewGuid(), Name = "jalapenos"},

                // Ingredients for Keto Chocolate Mousse
                // https://www.delish.com/cooking/recipe-ideas/recipes/a57631/paleo-chocolate-mousse-recipe/
                new Ingredient { Id = Guid.NewGuid(), Name = "heavy cream"},
                new Ingredient { Id = Guid.NewGuid(), Name = "keto chocolate chips"},
                new Ingredient { Id = Guid.NewGuid(), Name = "honey"},

                // Ingredients for Paleo Chili
                // https://www.delish.com/cooking/recipe-ideas/a25351108/paleo-chili-recipe/
                new Ingredient { Id = Guid.NewGuid(), Name = "bacon"},
                new Ingredient { Id = Guid.NewGuid(), Name = "yellow onion"},
                new Ingredient { Id = Guid.NewGuid(), Name = "celery stalks"},
                new Ingredient { Id = Guid.NewGuid(), Name = "lean ground beef"},

                // Ingredients for Vanilla Milkshake
                // https://www.delish.com/cooking/recipe-ideas/a20760804/easy-milkshake-recipe/
                new Ingredient { Id = Guid.NewGuid(), Name = "vanilla ice cream"},
                new Ingredient { Id = Guid.NewGuid(), Name = "milk"},
                new Ingredient { Id = Guid.NewGuid(), Name = "whipped topping"},
                new Ingredient { Id = Guid.NewGuid(), Name = "sprinkles"},

                // Ingredients for Easy Vegan Fried Rice
                // https://minimalistbaker.com/easy-vegan-fried-rice/
                new Ingredient { Id = Guid.NewGuid(), Name = "brown rice"},
                new Ingredient { Id = Guid.NewGuid(), Name = "green onion"},
                new Ingredient { Id = Guid.NewGuid(), Name = "peas"},
                new Ingredient { Id = Guid.NewGuid(), Name = "carrots"},
                new Ingredient { Id = Guid.NewGuid(), Name = "soy sauce"},
                new Ingredient { Id = Guid.NewGuid(), Name = "peanut butter"},
                new Ingredient { Id = Guid.NewGuid(), Name = "maple syrup"},
                new Ingredient { Id = Guid.NewGuid(), Name = "sesame oil"},

                // Ingredients for Good old-fashioned pancakes
                // https://www.allrecipes.com/recipe/21014/good-old-fashioned-pancakes/
                new Ingredient { Id = Guid.NewGuid(), Name = "all-purpose flour"},
                new Ingredient { Id = Guid.NewGuid(), Name = "baking powder"},
                new Ingredient { Id = Guid.NewGuid(), Name = "white sugar"},
                new Ingredient { Id = Guid.NewGuid(), Name = "butter"},
                new Ingredient { Id = Guid.NewGuid(), Name = "egg"},

                // Ingredients for Egg Niçoise salad
                // https://www.allrecipes.com/recipe/21014/good-old-fashioned-pancakes/
                new Ingredient { Id = Guid.NewGuid(), Name = "rapeseed oil"},
                new Ingredient { Id = Guid.NewGuid(), Name = "lemon"},
                new Ingredient { Id = Guid.NewGuid(), Name = "balsemic vinegar"},
                new Ingredient { Id = Guid.NewGuid(), Name = "basil"},
                new Ingredient { Id = Guid.NewGuid(), Name = "potatoe"},
                new Ingredient { Id = Guid.NewGuid(), Name = "green beans"},
                new Ingredient { Id = Guid.NewGuid(), Name = "cherry tomato"},
                new Ingredient { Id = Guid.NewGuid(), Name = "romaine lettuce"},
                new Ingredient { Id = Guid.NewGuid(), Name = "kalamata olive"},

                // Ingredient for Protein Bars
                // https://chocolatecoveredkatie.com/protein-bars-recipe/
                new Ingredient { Id = Guid.NewGuid(), Name = "protein powder"},
                new Ingredient { Id = Guid.NewGuid(), Name = "chocolate chips"},

            };


            var recipe1 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Scrambled Tofu",
                ApplicationUserId = ImiUserId,
                CategoryId = categories.Where(c => c.Name.Equals("breakfast")).Select(c => c.Id).FirstOrDefault(),
                CookTime = 30,
                PrepTime = 15,
                Description = "A healthy vegan breakfast",
                DietId = diets.Where(d => d.Name.Equals("vegan")).Select(c => c.Id).FirstOrDefault(),
                Servings = 2,
                ImgURL = "https://simpleveganblog.com/wp-content/uploads/2022/07/tofu-scramble-1.jpg",
            };
            var recipe2 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Tripple berry smoothie",
                ApplicationUserId = ImiRefuserId,
                CategoryId = categories.Where(c => c.Name.Equals("beverage")).Select(c => c.Id).FirstOrDefault(),
                CookTime = 10,
                PrepTime = 5,
                Description = "A smoothie containing 3 types of berries",
                DietId = diets.Where(d => d.Name.Equals("anything")).Select(c => c.Id).FirstOrDefault(),
                Servings = 2,
                ImgURL = "https://cookingformysoul.com/wp-content/uploads/2022/05/mixed-berry-smoothie-2-min.jpg",
            };
            var recipe3 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Tuna avocado salad",
                ApplicationUserId = ImiUserId,
                CategoryId = categories.Where(c => c.Name.Equals("lunch")).Select(c => c.Id).FirstOrDefault(),
                CookTime = 5,
                PrepTime = 5,
                Description = "A fast and healthy lunch, packed with protein, for people in a hurry",
                DietId = diets.Where(d => d.Name.Equals("anything")).Select(c => c.Id).FirstOrDefault(),
                Servings = 2,
                ImgURL = "https://www.wholesomeyum.com/wp-content/uploads/2019/05/wholesomeyum-avocado-tuna-salad-recipe-1.jpg",
            };
            var recipe4 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Keto Chocolate Mousse",
                ApplicationUserId = ImiUserId,
                CategoryId = categories.Where(c => c.Name.Equals("dessert")).Select(c => c.Id).FirstOrDefault(),
                CookTime = 60,
                PrepTime = 15,
                Description = "This keto mousse gets its rich, creamy texture from avocados.",
                DietId = diets.Where(d => d.Name.Equals("keto")).Select(c => c.Id).FirstOrDefault(),
                Servings = 2,
                ImgURL = "https://thebigmansworld.com/wp-content/uploads/2022/07/keto-chocolate-mousse.jpg",
            };
            var recipe5 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Paleo Chili",
                ApplicationUserId = ImiUserId,
                CategoryId = categories.Where(c => c.Name.Equals("dinner")).Select(c => c.Id).FirstOrDefault(),
                CookTime = 40,
                PrepTime = 15,
                Description = "A hearty bowl of chili is the perfect dinner for a blustery winter day.",
                DietId = diets.Where(d => d.Name.Equals("paleo")).Select(c => c.Id).FirstOrDefault(),
                Servings = 2,
                ImgURL = "https://www.laurafuentes.com/wp-content/uploads/2013/10/Paleo_chili_recipe-card_1-2.jpg",
            };
            var recipe6 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Easy Vanilla Milkshake",
                ApplicationUserId = ImiUserId,
                CategoryId = categories.Where(c => c.Name.Equals("beverage")).Select(c => c.Id).FirstOrDefault(),
                CookTime = 5,
                PrepTime = 5,
                Description = "This easy milkshake recipe gives you the perfect ratio of milk to ice cream and is completely customizable! ",
                DietId = diets.Where(d => d.Name.Equals("anything")).Select(c => c.Id).FirstOrDefault(),
                Servings = 1,
                ImgURL = "https://images.unsplash.com/photo-1568901839119-631418a3910d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Nnx8bWlsa3NoYWtlfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60",
            };
            var recipe7 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Easy Vegan Fried Rice",
                ApplicationUserId = ImiUserId,
                CategoryId = categories.Where(c => c.Name.Equals("lunch")).Select(c => c.Id).FirstOrDefault(),
                CookTime = 60,
                PrepTime = 15,
                Description = "Easy, 10-ingredient vegan fried rice that’s loaded with vegetables, crispy baked tofu, and tons of flavor! " +
                "A healthy, satisfying plant-based side dish or entrée.",
                DietId = diets.Where(d => d.Name.Equals("vegan")).Select(c => c.Id).FirstOrDefault(),
                Servings = 4,
                ImgURL = "https://shortgirltallorder.com/wp-content/uploads/2020/03/veggie-fried-rice-square-4.jpg"
            };
            var recipe8 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Good Old-Fashioned Pancakes",
                ApplicationUserId = ImiUserId,
                CategoryId = categories.Where(c => c.Name.Equals("dessert")).Select(c => c.Id).FirstOrDefault(),
                CookTime = 15,
                PrepTime = 5,
                Description = "I found this pancake recipe in my Grandma's recipe book. Judging from the weathered look of this recipe card, this was a family favorite.",
                DietId = diets.Where(d => d.Name.Equals("anything")).Select(c => c.Id).FirstOrDefault(),
                Servings = 8,
                ImgURL = "https://images.unsplash.com/photo-1565299543923-37dd37887442?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=781&q=80"
            };
            var recipe9 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Egg Niçoise salad",
                ApplicationUserId = ImiRefuserId,
                CategoryId = categories.Where(c => c.Name.Equals("side dish")).Select(c => c.Id).FirstOrDefault(),
                CookTime = 10,
                PrepTime = 10,
                Description = "A vegetarian Niçoise salad, that's packed with goodness - fibre, folate, iron, vitamin c and gluten-free too",
                DietId = diets.Where(d => d.Name.Equals("vegetarian")).Select(c => c.Id).FirstOrDefault(),
                Servings = 2,
                ImgURL = "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"
            };
            var recipe10 = new Recipe
            {
                Id = Guid.NewGuid(),
                Title = "Easy Homemade Protein Bars",
                ApplicationUserId = ImiRefuserId,
                CategoryId = categories.Where(c => c.Name.Equals("snack")).Select(c => c.Id).FirstOrDefault(),
                CookTime = 5,
                PrepTime = 5,
                Description = "If you’re still buying protein bars at the store, this quick and simple vegan protein bar recipe might completely change your life…",
                DietId = diets.Where(d => d.Name.Equals("anything")).Select(c => c.Id).FirstOrDefault(),
                Servings = 10,
                ImgURL = "https://images.unsplash.com/photo-1622484212850-eb596d769edc?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80"
            };

            IEnumerable<Recipe> recipes = new List<Recipe>()
            {
                recipe1, recipe2, recipe3, recipe4, recipe5, recipe6, recipe7, recipe8, recipe9, recipe10
            };

            IEnumerable<Instruction> instructions = new List<Instruction>()
            {
                #region Scrambled Tofu
                new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe1.Id,
                    StepNumber = 1,
                    Description = "Scramble the block of tofu with your hands (see picture above) into small and bigger pieces."},

                new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe1.Id,
                    StepNumber = 2,
                    Description = "Heat 1 tablespoon of oil on medium heat in a pan. Caramelize the chopped red onions"},

                 new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe1.Id,
                    StepNumber = 3,
                    Description = "Add the scrambled tofu and stir for 1 minute."},

                 new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe1.Id,
                    StepNumber = 4,
                    Description = "Add the 1/2 cup of oat milk and stir until the tofu has soaked up most of it."},

                 new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe1.Id,
                    StepNumber = 5,
                    Description = "When there is only a small amount of milk remaining, add all of the remaining ingredients and stir for another 3-4 minutes on low to medium heat."},

                 new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe1.Id,
                    StepNumber = 6,
                    Description = "Serve with fresh bread."},
                #endregion

                #region Tripple Berry Smoothie
                new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe2.Id,
                    StepNumber = 1,
                    Description = "In a blender, combine all ingredients and blend until smooth."},

                new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe2.Id,
                    StepNumber = 2,
                    Description = "Divide between 2 cups and top with blackberries, if desired."},
                #endregion

                #region Tuna Avocado Salad
                new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe3.Id,
                    StepNumber = 1,
                    Description = "Mash the avocado and lime juice together with sea salt."},

                new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe3.Id,
                    StepNumber = 2,
                    Description = "Add the tuna, cilantro, red onion, celery, and jalapeños (if using). Stir everything together, breaking apart any large pieces of tuna as needed."},

                new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe3.Id,
                    StepNumber = 3,
                    Description = "Adjust salt and jalapeños to taste if needed. Serve immediately."},
                #endregion

                #region Keto Chocolate Mousse
                 new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe4.Id,
                    StepNumber = 1,
                    Description = "In a food processor or blender, blend all ingredients except chocolate curls until smooth."},

                 new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe4.Id,
                    StepNumber = 2,
                    Description = "Transfer to serving glasses and refrigerate 30 minutes and up to 1 hour. Garnish with chocolate curls if using."},
                #endregion

                #region Paleo Chili
                 new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe5.Id,
                    StepNumber = 1,
                    Description = "In a large pot over medium heat, cook bacon. When bacon is crisp, remove from pot with a slotted spoon. " +
                    "Add onion, celery, and peppers to pot and cook until soft, 6 minutes. " +
                    "Add garlic and cook until fragrant, 1 minute more."},

                 new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe5.Id,
                    StepNumber = 2,
                    Description = "Push vegetables to one side of the pan and add beef. Cook, stirring occasionally, until no pink remains. Drain fat and return to heat."},

                 new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe5.Id,
                    StepNumber = 3,
                    Description = "Add chili powder, cumin, oregano, and paprika and season with salt and pepper. " +
                    "Stir to combine and cook 2 minutes more. Add tomatoes and broth and bring to a simmer. " +
                    "Let cook 10 to 15 more minutes, until chili has thickened slightly. "},

                 new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe5.Id,
                    StepNumber = 4,
                    Description = "Ladle into bowls and top with reserved bacon, jalapeños, cilantro, and avocado."},
                #endregion

                #region Vanilla Milkshake

                  new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe6.Id,
                    StepNumber = 1,
                    Description = "In a blender, blend together ice cream and milk."},

                  new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe6.Id,
                    StepNumber = 2,
                    Description = "Pour into a glass and garnish with whipped topping, sprinkles, cacao powder and a cherry."},

                #endregion

                #region Easy Vegan Fried Rice

                  new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe7.Id,
                    StepNumber = 1,
                    Description = "Preheat oven to 400 degrees F (204 C) and line a baking sheet with parchment paper (or lightly grease with non-stick spray)."},

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe7.Id,
                    StepNumber = 2,
                    Description = "In the meantime wrap tofu in a clean, absorbent towel and set something heavy on top (such as a cast iron skillet) to press out the liquid."},

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe7.Id,
                    StepNumber = 3,
                    Description = "Once the oven is preheated, dice tofu into 1/4-inch cubes and arrange on baking sheet. Bake for 26-30 minutes. You’re looking for golden brown edges and a texture that’s firm to the touch. " +
                    "The longer it bakes, the firmer and crispier it will become, so if you’re looking for softer tofu remove from the oven around the 26-28 minute mark. " +
                    "I prefer crispy tofu, so I bake mine the full 30 minutes. Set aside."},

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe7.Id,
                    StepNumber = 4,
                    Description = "While the tofu bakes prepare your rice by bringing 12 cups of water to a boil in a large pot. Once boiling, add rinsed rice and stir. " +
                    "Boil on high uncovered for 30 minutes, then strain for 10 seconds and return to pot removed from the heat. Cover with a lid and let steam for 10 minutes*."},

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe7.Id,
                    StepNumber = 5,
                    Description = "While rice and tofu are cooking, prepare sauce by adding all ingredients to a medium-size mixing bowl and whisking to combine. " +
                    "Taste and adjust flavor as needed, adding more tamari or soy sauce for saltiness, peanut butter for creaminess, brown sugar for sweetness, or chili garlic sauce for heat."},

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe7.Id,
                    StepNumber = 6,
                    Description = "Once the tofu is done baking, add directly to the sauce and marinate for 5 minutes, stirring occasionally"},

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe7.Id,
                    StepNumber = 7,
                    Description = "Heat a large metal or cast iron skillet over medium heat. " +
                    "Once hot, use a slotted spoon to scoop the tofu into the pan leaving most of the sauce behind. Cook for 3-4 minutes, stirring occasionally, until deep golden brown on all sides (see photo). " +
                    "Lower heat if browning too quickly. Remove from pan and set aside."},

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe7.Id,
                    StepNumber = 8,
                    Description = "To the still hot pan add garlic, green onion, peas and carrots. Sauté for 3-4 minutes, stirring occasionally, and season with 1 Tbsp (15 ml) tamari or soy sauce"},

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe7.Id,
                    StepNumber = 9,
                    Description = "Add cooked rice, tofu, and remaining sauce and stir. Cook over medium-high heat for 3-4 minutes, stirring frequently."},

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe7.Id,
                    StepNumber = 10,
                    Description = "Serve immediately with extra chili garlic sauce or sriracha for heat (optional). " +
                    "Crushed salted, roasted peanuts or cashews make a lovely additional garnish. " +
                    "Leftovers keep well in the refrigerator for 3-4 days, though best when fresh. Reheat in a skillet over medium heat or in the microwave."},



                #endregion

                #region Goold old-fashioned pancakes

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe8.Id,
                    StepNumber = 1,
                    Description = "Sift flour, baking powder, sugar, and salt together in a large bowl. Make a well in the center and add milk, melted butter, and egg; mix until smooth."},

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe8.Id,
                    StepNumber = 2,
                    Description = "Heat a lightly oiled griddle or pan over medium-high heat. " +
                    "Pour or scoop the batter onto the griddle, using approximately 1/4 cup for each pancake; cook until bubbles form and the edges are dry, about 2 to 3 minutes. " +
                    "Flip and cook until browned on the other side. Repeat with remaining batter."},
                #endregion

                #region Egg Niçoise salad

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe9.Id,
                    StepNumber = 1,
                    Description = "Mix the dressing ingredients together in a small bowl with 1 tbsp water."},

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe9.Id,
                    StepNumber = 2,
                    Description = "Meanwhile boil the potatoes for 7 mins, add the beans and boil 5 mins more or until both are just tender, then drain. " +
                    "Boil 2 eggs for 8 minutes then shell and halve."},

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe9.Id,
                    StepNumber = 3,
                    Description = "Toss the beans, potatoes and remaining salad ingredients, except the eggs, together in a large bowl with half the dressing. " +
                    "Arrange the eggs on top and drizzle over the remaining dressing."},

                #endregion

                #region Protein Bar

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe10.Id,
                    StepNumber = 1,
                    Description = "Stir all ingredients except optional chips to form a dough. " +
                    "Either shape into bars with your hands or smooth into a lined 8×8 pan, refrigerate until chilled, then cut into bars."},

                   new Instruction {
                    Id = Guid.NewGuid(),
                    RecipeId = recipe10.Id,
                    StepNumber = 2,
                    Description = "For the optional chocolate coating, spread the melted chocolate over the pan before chilling. " +
                    "(I usually stir 2 tsp oil into the melted chocolate for a smoother sauce, but it's not required.) " +
                    "Or you can dip the bars into the chocolate sauce individually and then chill to set."},

                #endregion
            };

            IEnumerable<RecipeIngredient> recipeIngredients = new List<RecipeIngredient>()
            {
                #region Scrambled Tofu

                new RecipeIngredient {
                    RecipeId = recipe1.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("firm tofu")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1,
                    UnitId = block.Id
                },

                new RecipeIngredient {
                    RecipeId = recipe1.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("red onion")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.5D,
                    UnitId = piece.Id
                },

                 new RecipeIngredient {
                    RecipeId = recipe1.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("bell pepper")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.5D,
                    UnitId = piece.Id
                 },

                  new RecipeIngredient {
                    RecipeId = recipe1.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("oat milk")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.5D,
                    UnitId = cup.Id
                  },

                  new RecipeIngredient {
                    RecipeId = recipe1.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("turmeric")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.75D,
                    UnitId = tsp.Id
                  },

                  new RecipeIngredient {
                    RecipeId = recipe1.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("nutritional yeast")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = tbsp.Id
                  },


                #endregion

                #region Tripple Berry Smoothie

                  new RecipeIngredient {
                    RecipeId = recipe2.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("banana")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = piece.Id
                  },

                  new RecipeIngredient {
                    RecipeId = recipe2.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("almond milk")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 250D,
                    UnitId = ml.Id
                  },

                  new RecipeIngredient {
                    RecipeId = recipe2.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("greek yogurt")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 200D,
                    UnitId = grams.Id
                  },

                  new RecipeIngredient {
                    RecipeId = recipe2.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("strawberries")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 150D,
                    UnitId = grams.Id
                  },

                  new RecipeIngredient {
                    RecipeId = recipe2.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("blackberries")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 150D,
                    UnitId = grams.Id
                  },

                  new RecipeIngredient {
                    RecipeId = recipe2.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("raspberries")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 150D,
                    UnitId = grams.Id
                  },



                #endregion

                #region Tuna Avocado Salad

                  new RecipeIngredient {
                    RecipeId = recipe3.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("avocado")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 2D,
                    UnitId = piece.Id
                  },

                  new RecipeIngredient {
                    RecipeId = recipe3.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("tuna")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 140D,
                    UnitId = grams.Id
                  },

                  new RecipeIngredient {
                    RecipeId = recipe3.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("lime")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 2D,
                    UnitId = tbsp.Id
                  },

                  new RecipeIngredient {
                    RecipeId = recipe3.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("salt")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.5D,
                    UnitId = tsp.Id
                  },

                  new RecipeIngredient {
                    RecipeId = recipe3.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("celery")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 3D,
                    UnitId = tbsp.Id
                  },

                  new RecipeIngredient {
                    RecipeId = recipe3.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("jalapenos")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = tbsp.Id
                  },

                  new RecipeIngredient {
                    RecipeId = recipe3.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("red onion")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 3D,
                    UnitId = tbsp.Id
                  },


                #endregion

                #region Keto Chocolate Mousse

                  new RecipeIngredient {
                    RecipeId = recipe4.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("avocado")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 2D,
                    UnitId = piece.Id
                  },
                  new RecipeIngredient {
                    RecipeId = recipe4.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("heavy cream")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.75D,
                    UnitId = cup.Id
                  },
                  new RecipeIngredient {
                    RecipeId = recipe4.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("keto chocolate chips")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.5D,
                    UnitId = cup.Id
                  },
                  new RecipeIngredient {
                    RecipeId = recipe4.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("honey")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.25D,
                    UnitId = cup.Id
                  },
                  new RecipeIngredient {
                    RecipeId = recipe4.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("unsweetened cacao powder")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 3D,
                    UnitId = tbsp.Id
                  },
                  new RecipeIngredient {
                    RecipeId = recipe4.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("vanilla")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1,
                    UnitId = tsp.Id
                  },
                  new RecipeIngredient {
                    RecipeId = recipe4.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("kosher salt")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.5D,
                    UnitId = tsp.Id
                  },
                #endregion

                #region Paleo Chili
                    new RecipeIngredient {
                    RecipeId = recipe5.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("bacon")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = cup.Id
                  },
                    new RecipeIngredient {
                    RecipeId = recipe5.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("yellow onion")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.5D,
                    UnitId = piece.Id
                  },
                    new RecipeIngredient {
                    RecipeId = recipe5.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("celery stalks")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 2D,
                    UnitId = piece.Id
                  },
                    new RecipeIngredient {
                    RecipeId = recipe5.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("lean ground beef")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.5D,
                    UnitId = cup.Id
                  },
                    new RecipeIngredient {
                    RecipeId = recipe5.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("chili powder")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 2D,
                    UnitId = tbsp.Id
                  },
                    new RecipeIngredient {
                    RecipeId = recipe5.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("ground cumin")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 2D,
                    UnitId = tsp.Id
                  },
                    new RecipeIngredient {
                    RecipeId = recipe5.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("dried oregano")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 2D,
                    UnitId = tsp.Id
                  },
                    new RecipeIngredient {
                    RecipeId = recipe5.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("smoked paprika")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 2D,
                    UnitId = tbsp.Id
                  },
                #endregion

                #region Vanilla Milkshake
                    
                    new RecipeIngredient {
                    RecipeId = recipe6.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("vanilla ice cream")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1.5D,
                    UnitId = cup.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe6.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("milk")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1.5D,
                    UnitId = cup.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe6.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("sprinkles")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0D,
                    UnitId = garnish.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe6.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("whipped topping")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0D,
                    UnitId = garnish.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe6.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("unsweetened cacao powder")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.5,
                    UnitId = tsp.Id
                  },

                #endregion

                #region Easy Vegan Fried Rice

                    new RecipeIngredient {
                    RecipeId = recipe7.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("firm tofu")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = cup.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe7.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("brown rice")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = cup.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe7.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("garlic")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 4D,
                    UnitId = clove.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe7.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("green onion")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = cup.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe7.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("peas")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.5D,
                    UnitId = cup.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe7.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("carrots")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.5D,
                    UnitId = cup.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe7.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("soy sauce")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 3D,
                    UnitId = tbsp.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe7.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("maple syrup")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 2D,
                    UnitId = tbsp.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe7.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("peanut butter")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = tbsp.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe7.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("sesame oil")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = tsp.Id
                  },

                #endregion

                #region Goold old-fashioned pancakes

                    new RecipeIngredient {
                    RecipeId = recipe8.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("all-purpose flour")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1.5D,
                    UnitId = cup.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe8.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("baking powder")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 3.5D,
                    UnitId = tsp.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe8.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("white sugar")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = tbsp.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe8.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("salt")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.25D,
                    UnitId = tsp.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe8.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("milk")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1.25D,
                    UnitId = cup.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe8.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("butter")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 3D,
                    UnitId = tbsp.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe8.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("egg")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = piece.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe8.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("maple syrup")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1,
                    UnitId = tbsp.Id
                  },

                #endregion

                #region Egg Niçoise salad

                    new RecipeIngredient {
                    RecipeId = recipe9.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("rapeseed oil")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 2D,
                    UnitId = tbsp.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe9.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("lemon")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = piece.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe9.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("balsemic vinegar")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = tsp.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe9.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("garlic")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1D,
                    UnitId = clove.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe9.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("basil")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0D,
                    UnitId = garnish.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe9.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("egg")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 2D,
                    UnitId = piece.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe9.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("potatoe")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 250D,
                    UnitId = grams.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe9.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("green beans")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 200D,
                    UnitId = grams.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe9.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("romaine lettuce")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 15D,
                    UnitId = grams.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe9.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("kalamata olive")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 6D,
                    UnitId = piece.Id
                  },
                #endregion

                #region Protein Bar

                    new RecipeIngredient {
                    RecipeId = recipe10.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("peanut butter")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 1.5D,
                    UnitId = cup.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe10.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("protein powder")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.75D,
                    UnitId = cup.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe10.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("maple syrup")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.25D,
                    UnitId = cup.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe10.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("salt")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 0.5D,
                    UnitId = tsp.Id
                  },

                    new RecipeIngredient {
                    RecipeId = recipe10.Id,
                    IngredientId = ingredients.Where(i => i.Name.Equals("chocolate chips")).Select(i => i.Id).FirstOrDefault(),
                    Amount = 35D,
                    UnitId = grams.Id
                  },

                #endregion
            };

            IEnumerable<Review> reviews = new List<Review>()
            {

                #region Reviews Recipe 1
                new Review {
                    Id = Guid.NewGuid(),
                    Comment = "Great recipe!",
                    Rating = 4,
                    CreationDate = DateTime.Now,
                    UpdateTimeStamp = DateTime.Now,
                    RecipeId = recipe1.Id,
                    ApplicationUserId = ImiRefuserId,
                },

                new Review {
                    Id = Guid.NewGuid(),
                    Comment = "Didn't taste good",
                    Rating = 2,
                    CreationDate = new DateTime(2022,01,25),
                    UpdateTimeStamp = new DateTime(2022,01,25),
                    RecipeId = recipe1.Id,
                    ApplicationUserId = ImiRefuserId,
                },
                #endregion

                #region Reviews Recipe 2
                new Review {
                    Id = Guid.NewGuid(),
                    Rating = 3,
                    CreationDate = new DateTime(2022,07,20),
                    UpdateTimeStamp = new DateTime(2022,07,20),
                    RecipeId = recipe2.Id,
                    ApplicationUserId = ImiUserId,
                },

                new Review {
                    Id = Guid.NewGuid(),
                    Rating = 5,
                    Comment = "Definitely will eat this again!",
                    CreationDate = new DateTime(2022,07,20),
                    UpdateTimeStamp = new DateTime(2022,07,20),
                    RecipeId = recipe2.Id,
                    ApplicationUserId = ImiUserId,
                },
                #endregion
            };

            IEnumerable<FavoriteRecipe> favorites = new List<FavoriteRecipe>()
            {
                new FavoriteRecipe
                {
                    ApplicationUserId = ImiUserId,
                    RecipeId = recipe3.Id
                },

                new FavoriteRecipe
                {
                    ApplicationUserId = ImiUserId,
                    RecipeId = recipe9.Id
                },

                new FavoriteRecipe
                {
                    ApplicationUserId = ImiRefuserId,
                    RecipeId = recipe1.Id
                },

            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
            modelBuilder.Entity<ApplicationUser>().HasData(users);
            modelBuilder.Entity<Category>().HasData(categories);
            modelBuilder.Entity<Diet>().HasData(diets);
            modelBuilder.Entity<Unit>().HasData(units);
            modelBuilder.Entity<Ingredient>().HasData(ingredients);
            modelBuilder.Entity<Recipe>().HasData(recipes);
            modelBuilder.Entity<Instruction>().HasData(instructions);
            modelBuilder.Entity<RecipeIngredient>().HasData(recipeIngredients);
            modelBuilder.Entity<Review>().HasData(reviews);
            //modelBuilder.Entity<FavoriteRecipe>().HasData(favorites);
        }
    }
}
