using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Services.Mock;
using Imi.Project.Mobile.ViewModels;
using Moq;

namespace Imi.Project.Mobile.Test
{
    public class RecipeDetailsViewModelTests
    {
        [Fact]
        public async Task InitializeAsync_WithIngredients_NotNull()
        {
            //Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new MockRecipeService();

            var recipes = await mockRecipeService.GetAllRecipesAsync();
            var recipe = recipes.FirstOrDefault();

            var recipeDetailsViewModel = new RecipeDetailsViewModel(mockRecipeService,
                mockNavigationService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object);

            //Assert
            await recipeDetailsViewModel.InitializeAsync(recipe);

            //Act
            Assert.NotNull(recipeDetailsViewModel.Ingredients);
        }

        [Fact]
        public async Task InitializeAsync_WithInstructions_NotNull()
        {
            //Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new MockRecipeService();

            var recipes = await mockRecipeService.GetAllRecipesAsync();
            var recipe = recipes.FirstOrDefault();

            var recipeDetailsViewModel = new RecipeDetailsViewModel(mockRecipeService,
                mockNavigationService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object);

            //Assert
            await recipeDetailsViewModel.InitializeAsync(recipe);

            //Act
            Assert.NotNull(recipeDetailsViewModel.Instructions);
        }

        [Fact]
        public async Task InitializeAsync_WithReviews_NotNull()
        {
            //Arrange
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new MockRecipeService();

            var recipes = await mockRecipeService.GetAllRecipesAsync();
            var recipe = recipes.FirstOrDefault();

            var recipeDetailsViewModel = new RecipeDetailsViewModel(mockRecipeService,
                mockNavigationService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object);

            //Assert
            await recipeDetailsViewModel.InitializeAsync(recipe);

            //Act
            Assert.NotNull(recipeDetailsViewModel.Reviews);
        }
    }
}
