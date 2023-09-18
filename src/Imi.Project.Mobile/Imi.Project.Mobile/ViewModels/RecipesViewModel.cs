using Imi.Project.Mobile.Helpers;
using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using Imi.Project.Mobile.ViewModels.Base;
using Syncfusion.DataSource.Extensions;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using ItemTappedEventArgs = Syncfusion.ListView.XForms.ItemTappedEventArgs;
using SelectionChangedEventArgs = Syncfusion.XForms.TabView.SelectionChangedEventArgs;

namespace Imi.Project.Mobile.ViewModels
{
    public class RecipesViewModel : BaseViewModel
    {
        private ObservableCollection<Recipe> _recipes;
        private ObservableCollection<Recipe> _userRecipes;
        private ObservableCollection<Recipe> _favorites;
        private int _selectedTabIndex;
        private string _currentUsername;
        private string _currentUserEmail;
        private string _tabTitle;
        private readonly IRecipeService _recipeService;
        private readonly IAuthenticationService _authenticationService;

        public ObservableCollection<Recipe> Recipes
        {
            get { return _recipes; }
            set
            {
                _recipes = value;
                OnPropertyChanged(nameof(Recipes));
            }
        }
        public ObservableCollection<Recipe> UserRecipes
        {
            get { return _userRecipes; }
            set
            {
                _userRecipes = value;
                OnPropertyChanged(nameof(UserRecipes));
            }
        }
        public ObservableCollection<Recipe> Favorites
        {
            get { return _favorites; }
            set
            {
                _favorites = value;
                OnPropertyChanged(nameof(Favorites));

            }
        }

        public int SelectedTabIndex
        {
            get => _selectedTabIndex;
            set
            {
                if (_selectedTabIndex != value)
                {
                    _selectedTabIndex = value;
                    OnPropertyChanged(nameof(SelectedTabIndex));
                }
            }
        }
        public string CurrentUsername
        {
            get { return _currentUsername; }
            set
            {
                _currentUsername = value;
                OnPropertyChanged(nameof(CurrentUsername));
            }
        }
        public string CurrentUserEmail
        {
            get { return _currentUserEmail; }
            set
            {
                _currentUserEmail = value;
                OnPropertyChanged(nameof(CurrentUserEmail));
            }
        }
        public string TabTitle
        {
            get { return _tabTitle; }
            set
            {
                _tabTitle = value;
                OnPropertyChanged(nameof(TabTitle));

            }
        }


        public ICommand RecipeTappedCommand => new Command<ItemTappedEventArgs>(OnRecipeTapped);
        public ICommand SelectionChangedCommand => new Command<SelectionChangedEventArgs>(TabViewSelectionChanged);
        public ICommand AddRecipeCommand => new Command(OnAddRecipe);
        public ICommand DeleteRecipeCommand => new Command(OnDeleteRecipe);
        public ICommand EditRecipeCommand => new Command(OnEditRecipe);
        public ICommand LogoutCommand => new Command(OnLogout);
        public ICommand ToggleBookmarkCommand => new Command(async (recipe) => await OnToggleBookmark(recipe));


        public RecipesViewModel(IRecipeService recipeService,
            INavigationService navigationService,
            IDialogService dialogService,
            IUserSettingsService userSettingsService,
            IAuthenticationService authenticationService)
            : base(navigationService, dialogService, userSettingsService)
        {

            _recipeService = recipeService;
            _authenticationService = authenticationService;
            MessagingCenter.Subscribe<RecipeActionsViewModel>
            (this, Constants.Messages.RecipesChangedMessage, RefreshRecipes);
        }

        public override async Task InitializeAsync(object parameter)
        {
            Recipes = (await _recipeService.GetAllRecipesAsync()).ToObservableCollection();
            UserRecipes = new ObservableCollection<Recipe>();
            Favorites = (await _recipeService.GetBookmarkedRecipes()).ToObservableCollection();
            LoadUserInfo();
            SetBookmarks();
            TabTitle = "Home";
        }

        private void SetBookmarks()
        {
            if (Favorites != null)
            {
                foreach (var recipe in Recipes)
                {
                    bool isBookmarked = Favorites.Any(favorite => favorite.Id == recipe.Id);
                    recipe.IsBookmarked = isBookmarked;
                }
            }
        }

        private async void OnRecipeTapped(ItemTappedEventArgs obj)
        {
            var recipe = obj.ItemData as Recipe;
            if (recipe != null)
            {
                await _navigationService.NavigateToAsync<RecipeDetailsViewModel>(recipe);
            }
        }

        public async void TabViewSelectionChanged(SelectionChangedEventArgs args)
        {
            var selectedIndex = args.Index;


            if (selectedIndex == 0)
            {
                TabTitle = "Home";
            }
            if (selectedIndex == 1)
            {
                TabTitle = "My Recipes";
                if (!UserRecipes.Any())
                {
                    UserRecipes = (await _recipeService.GetUserRecipesAsync()).ToObservableCollection();
                }
            }
            if (selectedIndex == 2)
            {
                TabTitle = "My Favorites";
                if (!Favorites.Any())
                {
                    Favorites = (await _recipeService.GetBookmarkedRecipes()).ToObservableCollection();
                }
            }
            if (selectedIndex == 3)
            {
                TabTitle = "Account";
            }

        }

        private async void OnAddRecipe()
        {
            await _navigationService.NavigateToAsync<RecipeActionsViewModel>();
        }

        private async void OnEditRecipe(object recipe)
        {
            var recipeToEdit = recipe as Recipe;
            if (recipeToEdit != null)
            {
                await _navigationService.NavigateToAsync<RecipeActionsViewModel>(recipeToEdit);
            }
        }

        private async void RefreshRecipes(object sender)
        {
            Recipes = new ObservableCollection<Recipe>(await _recipeService.GetAllRecipesAsync()).ToObservableCollection();
            UserRecipes = new ObservableCollection<Recipe>(await _recipeService.GetUserRecipesAsync()).ToObservableCollection();
            SetBookmarks();
        }

        private async void RefreshFavorites()
        {
            Favorites = new ObservableCollection<Recipe>(await _recipeService.GetBookmarkedRecipes()).ToObservableCollection();
        }

        private async void OnDeleteRecipe(object recipe)
        {
            var recipeToDelete = recipe as Recipe;
            if (recipeToDelete != null)
            {
                var confirmed = await _dialogService.ShowConfirmationDialog($"Are you sure you want to delete {recipeToDelete.Title} ?",
                    "Warning", "Yes", "Cancel");

                if (confirmed)
                {
                    await _recipeService.DeleteRecipe(recipeToDelete.Id);
                    RefreshRecipes(this);
                }
            }
        }

        private async void OnLogout()
        {
            if (_authenticationService.ClearToken())
            {
                await _navigationService.NavigateToAsync<LandingViewModel>();
            }
        }

        private void LoadUserInfo()
        {
            CurrentUsername = _userSettingsService.GetSetting(Constants.UsernameClaim);
            CurrentUserEmail = _userSettingsService.GetSetting(Constants.EmailClaim);
        }

        private async Task OnToggleBookmark(object recipe)
        {
            var rcp = recipe as Recipe;

            if (rcp.IsBookmarked)
            {
                await _recipeService.RemoveBookmark(rcp.Id);
            }
            else
            {
                await _recipeService.AddBookmark(rcp.Id);
            }

            RefreshFavorites();
            rcp.IsBookmarked = !rcp.IsBookmarked;
        }

    }

}
