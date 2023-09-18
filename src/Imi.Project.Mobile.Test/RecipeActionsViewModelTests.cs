using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using Imi.Project.Mobile.Services.Mock;
using Imi.Project.Mobile.ViewModels;
using Moq;

namespace Imi.Project.Mobile.Test
{
    public class RecipeActionsViewModelTests
    {
        [Fact]
        public async Task InitializeAsync_WithDiets_NotNull()
        {
            //Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new Mock<IRecipeService>();
            var mockCloudinaryService = new Mock<ICloudinaryService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockUnitService = new Mock<IUnitService>();


            var mockDietService = new MockDietService();

            var recipeActionsViewModel = new RecipeActionsViewModel(mockNavigationService.Object,
                mockCategoryService.Object,
                mockDietService,
                mockUnitService.Object,
                mockRecipeService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object,
                mockCloudinaryService.Object);

            //Assert
            await recipeActionsViewModel.InitializeAsync(null);

            //Act
            Assert.NotNull(recipeActionsViewModel.Diets);
        }
        [Fact]
        public async Task InitializeAsync_WithCategories_NotNull()
        {
            //Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new Mock<IRecipeService>();
            var mockCloudinaryService = new Mock<ICloudinaryService>();
            var mockDietService = new Mock<IDietService>();
            var mockUnitService = new Mock<IUnitService>();

            var mockCategoryService = new MockCategoryService();

            var recipeActionsViewModel = new RecipeActionsViewModel(mockNavigationService.Object,
                mockCategoryService,
                mockDietService.Object,
                mockUnitService.Object,
                mockRecipeService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object,
                mockCloudinaryService.Object);

            //Assert
            await recipeActionsViewModel.InitializeAsync(null);

            //Act
            Assert.NotNull(recipeActionsViewModel.Categories);
        }
        [Fact]
        public async Task InitializeAsync_WithSelectedDiet_NotNull()
        {
            //Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new Mock<IRecipeService>();
            var mockCloudinaryService = new Mock<ICloudinaryService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockUnitService = new Mock<IUnitService>();


            var mockDietService = new MockDietService();

            var recipeActionsViewModel = new RecipeActionsViewModel(mockNavigationService.Object,
                mockCategoryService.Object,
                mockDietService,
                mockUnitService.Object,
                mockRecipeService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object,
                mockCloudinaryService.Object);

            //Assert
            await recipeActionsViewModel.InitializeAsync(null);

            //Act
            Assert.NotNull(recipeActionsViewModel.SelectedDiet);
        }
        [Fact]
        public async Task InitializeAsync_WithSelectedCategory_NotNull()
        {
            //Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new Mock<IRecipeService>();
            var mockCloudinaryService = new Mock<ICloudinaryService>();
            var mockDietService = new Mock<IDietService>();
            var mockUnitService = new Mock<IUnitService>();

            var mockCategoryService = new MockCategoryService();

            var recipeActionsViewModel = new RecipeActionsViewModel(mockNavigationService.Object,
                mockCategoryService,
                mockDietService.Object,
                mockUnitService.Object,
                mockRecipeService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object,
                mockCloudinaryService.Object);

            //Assert
            await recipeActionsViewModel.InitializeAsync(null);

            //Act
            Assert.NotNull(recipeActionsViewModel.SelectedCategory);
        }
        [Fact]
        public async Task InitializeAsync_WithParameterNotNull_CorrectSelectedCategory()
        {
            //Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new Mock<IRecipeService>();
            var mockCloudinaryService = new Mock<ICloudinaryService>();
            var mockDietService = new Mock<IDietService>();
            var mockUnitService = new Mock<IUnitService>();

            var mockCategoryService = new MockCategoryService();

            var recipe = new Recipe
            {
                Category = "Lunch",
                Diet = "Vegan"
            };

            var expectedCategory = "Lunch";

            var recipeActionsViewModel = new RecipeActionsViewModel(mockNavigationService.Object,
                mockCategoryService,
                mockDietService.Object,
                mockUnitService.Object,
                mockRecipeService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object,
                mockCloudinaryService.Object);

            //Assert
            await recipeActionsViewModel.InitializeAsync(recipe);

            //Act
            Assert.Equal(expectedCategory, recipeActionsViewModel.SelectedCategory.Name, StringComparer.OrdinalIgnoreCase);
        }
        [Fact]
        public async Task InitializeAsync_WithParameterNotNull_CorrectSelectedDiet()
        {
            //Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new Mock<IRecipeService>();
            var mockCloudinaryService = new Mock<ICloudinaryService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockUnitService = new Mock<IUnitService>();

            var mockDietService = new MockDietService();

            var recipe = new Recipe
            {
                Category = "Lunch",
                Diet = "Vegan"
            };

            var expectedDiet = "Vegan";

            var recipeActionsViewModel = new RecipeActionsViewModel(mockNavigationService.Object,
                mockCategoryService.Object,
                mockDietService,
                mockUnitService.Object,
                mockRecipeService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object,
                mockCloudinaryService.Object);

            //Assert
            await recipeActionsViewModel.InitializeAsync(recipe);

            //Act
            Assert.Equal(expectedDiet, recipeActionsViewModel.SelectedDiet.Name, StringComparer.OrdinalIgnoreCase);
        }

        [Theory]
        [InlineData("", "", 0, 0, 0, "", "")]
        [InlineData("Test", "Test", 1, 1, 32, "Vegan", "Lunch")]
        [InlineData("Test", "Test", 1, 1, 1, null, "Lunch")]
        [InlineData("Test", "Test", 1, 1, 1, "Vegan", null)]
        [InlineData("Test", "Test", 1, 1, 1, null, null)]
        [InlineData("", "", 1, 1, 1, null, null)]
        public void OnSave_WithInvalidData_Return(string title,
          string description,
          int cookTime,
          int prepTime,
          int servings,
          string diet,
          string category)
        {
            //Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new Mock<IRecipeService>();
            var mockCloudinaryService = new Mock<ICloudinaryService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockDietService = new Mock<IDietService>();
            var mockUnitService = new Mock<IUnitService>();

            mockUserSettingsService.Setup(u => u.GetSetting(It.IsAny<string>())).Returns("00000000-0000-0000-0000-000000000002");

            var recipeActionsViewModel = new RecipeActionsViewModel(mockNavigationService.Object,
                mockCategoryService.Object,
                mockDietService.Object,
                mockUnitService.Object,
                mockRecipeService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object,
                mockCloudinaryService.Object);

            recipeActionsViewModel.SelectedCategory = new Category { Name = category };
            recipeActionsViewModel.SelectedDiet = new Diet { Name = diet };

            recipeActionsViewModel.SelectedRecipe = new Recipe
            {
                Title = title,
                Description = description,
                CookTime = cookTime,
                PrepTime = prepTime,
                Servings = servings,
                Diet = diet,
                Category = category,
            };

            //Act
            recipeActionsViewModel.SaveCommand.Execute(null);

            //Assert
            mockRecipeService.Verify(
                service => service.UpdateRecipe(It.IsAny<Recipe>()),
                Times.Never
            );

            mockRecipeService.Verify(
                service => service.AddRecipe(It.IsAny<Recipe>()),
                Times.Never
            );
        }

        [Theory]
        [InlineData("Test", "Test", 1, 1, 1, "Vegan", "Lunch")]
        [InlineData("Test", "Test", 999, 999, 30, "Vegan", "Lunch")]
        public void OnSave_WithValidData_UpdateRecipeExecuted(string title,
          string description,
          int cookTime,
          int prepTime,
          int servings,
          string diet,
          string category)
        {
            //Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new Mock<IRecipeService>();
            var mockCloudinaryService = new Mock<ICloudinaryService>();
            var mockCategoryService = new Mock<ICategoryService>();
            var mockDietService = new Mock<IDietService>();
            var mockUnitService = new Mock<IUnitService>();

            IEnumerable<Recipe> recipes = new List<Recipe>()
            {
                new Recipe { Title = "BookmarkedRecipe1"},
                new Recipe { Title = "BookmarkedRecipe2"},
                new Recipe { Title = "BookmarkedRecipe3"},
            };

            mockRecipeService.Setup(r => r.GetBookmarkedRecipes()).ReturnsAsync(recipes);

            var recipeActionsViewModel = new RecipeActionsViewModel(mockNavigationService.Object,
                mockCategoryService.Object,
                mockDietService.Object,
                mockUnitService.Object,
                mockRecipeService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object,
                mockCloudinaryService.Object);

            recipeActionsViewModel.SelectedCategory = new Category { Name = category };
            recipeActionsViewModel.SelectedDiet = new Diet { Name = diet };

            recipeActionsViewModel.SelectedRecipe = new Recipe
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                Title = title,
                Description = description,
                CookTime = cookTime,
                PrepTime = prepTime,
                Servings = servings,
                Diet = diet,
                Category = category,
            };

            //Act
            recipeActionsViewModel.SaveCommand.Execute(null);

            //Assert
            mockRecipeService.Verify(
                service => service.UpdateRecipe(It.IsAny<Recipe>()),
                Times.Once
            );
        }


    }

}
