using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using Imi.Project.Mobile.ViewModels.Base;
using Syncfusion.DataSource.Extensions;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Imi.Project.Mobile.ViewModels
{
    public class RecipeDetailsViewModel : BaseViewModel
    {
        private Recipe _selectedRecipe;
        private ObservableCollection<Ingredient> _ingredients;
        private ObservableCollection<Instruction> _instructions;
        private ObservableCollection<Review> _reviews;
        private readonly IRecipeService _recipeService;

        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged(nameof(SelectedRecipe));
            }
        }
        public ObservableCollection<Ingredient> Ingredients
        {
            get { return _ingredients; }
            set
            {
                _ingredients = value;
                OnPropertyChanged(nameof(Ingredients));
            }
        }
        public ObservableCollection<Instruction> Instructions
        {
            get { return _instructions; }
            set
            {
                _instructions = value;
                OnPropertyChanged(nameof(Instructions));
            }
        }


        public ObservableCollection<Review> Reviews
        {
            get { return _reviews; }
            set
            {
                _reviews = value;
                OnPropertyChanged(nameof(Reviews));
            }
        }



        public RecipeDetailsViewModel(IRecipeService recipeService,
            INavigationService navigationService,
            IDialogService dialogService,
            IUserSettingsService userSettingsService)
            : base(navigationService, dialogService, userSettingsService)
        {
            _recipeService = recipeService;
        }

        public override async Task InitializeAsync(object data)
        {
            SelectedRecipe = (Recipe)data;

            if (SelectedRecipe != null)
            {
                Ingredients = (await _recipeService.GetRecipeIngredients(SelectedRecipe.Id)).ToObservableCollection();
                Instructions = (await _recipeService.GetRecipeInstructions(SelectedRecipe.Id)).ToObservableCollection();
                Reviews = (await _recipeService.GetRecipeReviews(SelectedRecipe.Id)).ToObservableCollection();
            }
        }
    }
}
