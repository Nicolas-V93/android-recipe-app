using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using Imi.Project.Mobile.Services.Mock;
using Imi.Project.Mobile.ViewModels;
using Moq;
using Syncfusion.ListView.XForms;

namespace Imi.Project.Mobile.Test
{
    public class RecipesViewModelTests
    {
        [Fact]
        public async Task InitializeAsync_WithRecipes_NotNull()
        {
            //Arrange
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new MockRecipeService();

            var recipesViewModel = new RecipesViewModel(mockRecipeService,
                mockNavigationService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object,
                mockAuthenticationService.Object);

            //Assert
            await recipesViewModel.InitializeAsync(null);

            //Act
            Assert.NotNull(recipesViewModel.Recipes);
        }

        [Fact]
        public async Task InitializeAsync_WithUserRecipes_NotNull()
        {
            //Arrange
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new MockRecipeService();

            var recipesViewModel = new RecipesViewModel(mockRecipeService,
                mockNavigationService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object,
                mockAuthenticationService.Object);

            //Assert
            await recipesViewModel.InitializeAsync(null);

            //Act
            Assert.NotNull(recipesViewModel.UserRecipes);
        }

        [Fact]
        public void RecipeTappedCommand_WithParameter_IsRecipe()
        {
            //Arrange
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new MockRecipeService();

            var recipesViewModel = new RecipesViewModel(mockRecipeService,
                mockNavigationService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object,
                mockAuthenticationService.Object);

            Recipe? expectedRecipe = new Recipe();

            var itemTappedEventArgs = new ItemTappedEventArgs(ItemType.Header, expectedRecipe, new Xamarin.Forms.Point());

            //Act
            recipesViewModel.RecipeTappedCommand.Execute(itemTappedEventArgs);

            //Assert
            Assert.IsType<Recipe>(expectedRecipe);
        }

        [Fact]
        public void EditRecipeCommand_WithParameter_IsRecipe()
        {
            //Arrange
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new MockRecipeService();

            var recipesViewModel = new RecipesViewModel(mockRecipeService,
                mockNavigationService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object,
                mockAuthenticationService.Object);

            Recipe? expectedRecipe = new Recipe();

            //Act
            recipesViewModel.EditRecipeCommand.Execute(expectedRecipe);

            //Assert
            Assert.IsType<Recipe>(expectedRecipe);
        }

        [Fact]
        public async Task DeleteRecipeCommand_WithRecipe_DeletedSuccesfully()
        {
            //Arrange
            var mockAuthenticationService = new Mock<IAuthenticationService>();
            var mockDialogService = new Mock<IDialogService>();
            var mockUserSettingsService = new Mock<IUserSettingsService>();
            var mockNavigationService = new Mock<INavigationService>();
            var mockRecipeService = new MockRecipeService();

            var recipeId = Guid.Parse("00000000-0000-0000-0000-000000000001");
            var recipes = await mockRecipeService.GetAllRecipesAsync();
            var recipeToDelete = recipes.FirstOrDefault(r => r.Id.Equals(recipeId));

            mockUserSettingsService.Setup(u => u.GetSetting(It.IsAny<string>())).Returns("00000000-0000-0000-0000-000000000001");

            var recipesViewModel = new RecipesViewModel(mockRecipeService,
                mockNavigationService.Object,
                mockDialogService.Object,
                mockUserSettingsService.Object,
                mockAuthenticationService.Object);

            mockDialogService
                    .Setup(d => d.ShowConfirmationDialog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(true);

            //Act
            recipesViewModel.DeleteRecipeCommand.Execute(recipeToDelete);

            //Assert
            mockDialogService.Verify(d => d.ShowConfirmationDialog(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            Assert.DoesNotContain(recipeToDelete, recipes);
        }

    }
}
