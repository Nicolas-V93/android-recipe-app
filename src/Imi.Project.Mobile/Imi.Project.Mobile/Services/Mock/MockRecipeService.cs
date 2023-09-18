using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.Services.Mock
{
    public class MockRecipeService : IRecipeService
    {

        private static Dictionary<string, Diet> _diets;
        public static Dictionary<string, Diet> Diets
        {
            get
            {
                if (_diets == null)
                {

                    _diets = new Dictionary<string, Diet>()
                    {
                        { "Anything", new Diet { Name = "Anything" } },
                        { "Vegan", new Diet { Name = "Vegan" } },
                        { "Vegetarian", new Diet { Name = "Vegetarian" } }
                    };

                }

                return _diets;
            }
        }

        private static Dictionary<string, Category> _categories;
        public static Dictionary<string, Category> Categories
        {
            get
            {
                if (_categories == null)
                {
                    _categories = new Dictionary<string, Category>()
                    {
                        { "Lunch", new Category { Name = "Lunch" } },
                        { "Breakfast", new Category { Name = "Breakfast" } },
                        { "Snack", new Category { Name = "Snack" } }
                    };

                }

                return _categories;
            }
        }

        private static Dictionary<string, User> _users;
        public static Dictionary<string, User> Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new Dictionary<string, User>()
                    {
                        { "DUMMY_USER", new User { UserName = "DUMMY_USER" } },
                        { "DUMMY_USER2", new User { UserName = "DUMMY_USER2" } },
                    };

                }

                return _users;
            }
        }

        private List<Recipe> mockRecipes = new List<Recipe>()
        {
            new Recipe
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Title = "Scrambled Tofu",
                    Category = Categories["Breakfast"].Name,
                    CookTime = 30,
                    PrepTime = 15,
                    Description = "A healthy vegan breakfast",
                    Diet = Diets["Vegan"].Name,
                    Servings = 2,
                    ImgURL = "https://simpleveganblog.com/wp-content/uploads/2022/07/tofu-scramble-1.jpg",
                    User = Users["DUMMY_USER"],
                },
            new Recipe
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Title = "Tripple berry smoothie",
                    Category = Categories["Breakfast"].Name,
                    CookTime = 10,
                    PrepTime = 5,
                    Description = "A smoothie containing 3 types of berries",
                    Diet = Diets["Vegan"].Name,
                    Servings = 2,
                    ImgURL = "https://cookingformysoul.com/wp-content/uploads/2022/05/mixed-berry-smoothie-2-min.jpg",
                    User = Users["DUMMY_USER"]
                },
                 new Recipe
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Title = "Tuna avocado salad",
                    Category = Categories["Breakfast"].Name,
                    CookTime = 5,
                    PrepTime = 5,
                    Description = "A fast and healthy lunch, packed with protein, for people in a hurry",
                    Diet = Diets["Vegan"].Name,
                    Servings = 2,
                    ImgURL = "https://www.wholesomeyum.com/wp-content/uploads/2019/05/wholesomeyum-avocado-tuna-salad-recipe-1.jpg",
                    User = Users["DUMMY_USER"]
                },
                new Recipe
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    Title = "Keto Chocolate Mousse",
                    Category = Categories["Lunch"].Name,
                    CookTime = 60,
                    PrepTime = 15,
                    Description = "This keto mousse gets its rich, creamy texture from avocados.",
                    Diet = Diets["Vegetarian"].Name,
                    Servings = 2,
                    ImgURL = "https://thebigmansworld.com/wp-content/uploads/2022/07/keto-chocolate-mousse.jpg",
                    User = Users["DUMMY_USER"]
                },
                new Recipe
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Title = "Paleo Chili",
                    Category = Categories["Lunch"].Name,
                    CookTime = 40,
                    PrepTime = 15,
                    Description = "A hearty bowl of chili is the perfect dinner for a blustery winter day.",
                    Diet = Diets["Vegetarian"].Name,
                    Servings = 2,
                    ImgURL = "https://www.laurafuentes.com/wp-content/uploads/2013/10/Paleo_chili_recipe-card_1-2.jpg",
                    User = Users["DUMMY_USER"]
                },
                new Recipe
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    Title = "Easy Vanilla Milkshake",
                    Category = Categories["Lunch"].Name,
                    CookTime = 5,
                    PrepTime = 5,
                    Description = "This easy milkshake recipe gives you the perfect ratio of milk to ice cream and is completely customizable! ",
                    Diet = Diets["Vegetarian"].Name,
                    Servings = 1,
                    ImgURL = "https://images.unsplash.com/photo-1568901839119-631418a3910d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxzZWFyY2h8Nnx8bWlsa3NoYWtlfGVufDB8fDB8fA%3D%3D&auto=format&fit=crop&w=500&q=60",
                    User = Users["DUMMY_USER"]
                 },
                new Recipe
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    Title = "Easy Vegan Fried Rice",
                    Category = Categories["Lunch"].Name,
                    CookTime = 60,
                    PrepTime = 15,
                    Description = "Easy, 10-ingredient vegan fried rice that’s loaded with vegetables, crispy baked tofu, and tons of flavor! " +
                    "A healthy, satisfying plant-based side dish or entrée.",
                    Diet = Diets["Vegetarian"].Name,
                    Servings = 4,
                    ImgURL = "https://shortgirltallorder.com/wp-content/uploads/2020/03/veggie-fried-rice-square-4.jpg",
                    User = Users["DUMMY_USER"]
                },
                new Recipe
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    Title = "Good Old-Fashioned Pancakes",
                    Category = Categories["Snack"].Name,
                    CookTime = 15,
                    PrepTime = 5,
                    Description = "I found this pancake recipe in my Grandma's recipe book. Judging from the weathered look of this recipe card, this was a family favorite.",
                    Diet = Diets["Anything"].Name,
                    Servings = 8,
                    ImgURL = "https://images.unsplash.com/photo-1565299543923-37dd37887442?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=781&q=80",
                    User = Users["DUMMY_USER"]
                },
                new Recipe
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    Title = "Egg Niçoise salad",
                    Category = Categories["Snack"].Name,
                    CookTime = 10,
                    PrepTime = 10,
                    Description = "A vegetarian Niçoise salad, that's packed with goodness - fibre, folate, iron, vitamin c and gluten-free too",
                    Diet = Diets["Anything"].Name,
                    Servings = 2,
                    ImgURL = "https://images.unsplash.com/photo-1512621776951-a57141f2eefd?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                    User = Users["DUMMY_USER2"]
                },
                new Recipe
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                    Title = "Easy Homemade Protein Bars",
                    Category = Categories["Snack"].Name,
                    CookTime = 5,
                    PrepTime = 5,
                    Description = "If you’re still buying protein bars at the store, this quick and simple vegan protein bar recipe might completely change your life…",
                    Diet = Diets["Anything"].Name,
                    Servings = 10,
                    ImgURL = "https://images.unsplash.com/photo-1622484212850-eb596d769edc?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1170&q=80",
                    User = Users["DUMMY_USER2"]
                },
        };



        public async Task<Recipe> AddRecipe(Recipe recipe)
        {
            mockRecipes.Add(recipe);
            return await Task.FromResult(recipe);
        }

        public async Task DeleteRecipe(Guid recipeId)
        {
            var oldrecipe = mockRecipes.FirstOrDefault(e => e.Id == recipeId);
            mockRecipes.Remove(oldrecipe);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesAsync()
        {
            return await Task.FromResult(mockRecipes);
        }

        public async Task<IEnumerable<Recipe>> GetUserRecipesAsync(Guid userId)
        {
            var recipes = mockRecipes.Where(r => r.User.Id.Equals(userId)).ToList();
            return await Task.FromResult(recipes);
        }

        public async Task<Recipe> UpdateRecipe(Recipe recipe)
        {
            var oldrecipe = mockRecipes.FirstOrDefault(e => e.Id == recipe.Id);
            mockRecipes.Remove(oldrecipe);
            mockRecipes.Add(recipe);
            return await Task.FromResult(recipe);
        }

        public async Task<IEnumerable<Ingredient>> GetRecipeIngredients(Guid recipeId)
        {
            var ingredients = new List<Ingredient>()
            {
                new Ingredient { Amount = 200, Name = "Celery", Unit = "grams"},
                new Ingredient { Amount = 150, Name = "Broccoli", Unit = "grams"}
            };

            return await Task.FromResult(ingredients);
        }

        public async Task<IEnumerable<Instruction>> GetRecipeInstructions(Guid recipeId)
        {
            var instructions = new List<Instruction>()
            {
                new Instruction { Description = "A random instruction", StepNumber = 1 },
                new Instruction { Description = "A second random instruction", StepNumber = 2 },

            };

            return await Task.FromResult(instructions);
        }

        public async Task<IEnumerable<Review>> GetRecipeReviews(Guid recipeId)
        {
            var reviews = new List<Review>()
            {
                new Review { Comment = "A random comment", CreationDate = DateTime.UtcNow, Rating = 4, User = null },
                new Review { Comment = "A second random comment", CreationDate = DateTime.UtcNow, Rating = 2, User = null },
            };

            return await Task.FromResult(reviews);
        }

        public async Task<IEnumerable<Recipe>> GetBookmarkedRecipes()
        {
            IEnumerable<Recipe> recipes = new List<Recipe>()
            {
                new Recipe { Title = "BookmarkedRecipe1"},
                new Recipe { Title = "BookmarkedRecipe2"},
            };

            return await Task.FromResult(recipes);
        }

        public async Task<IEnumerable<Recipe>> GetUserRecipesAsync()
        {
            IEnumerable<Recipe> recipes = new List<Recipe>()
            {
                new Recipe { Title = "UserRecipe1"},
                new Recipe { Title = "UserRecipe2"},
            };

            return await Task.FromResult(recipes);
        }

        public Task AddBookmark(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveBookmark(Guid recipeId)
        {
            throw new NotImplementedException();
        }

        public Task<Ingredient> AddIngredient(Guid recipeId, Ingredient ingredient)
        {
            throw new NotImplementedException();
        }

        public Task<Instruction> AddInstruction(Guid recipeId, Instruction instruction)
        {
            throw new NotImplementedException();
        }
    }
}
