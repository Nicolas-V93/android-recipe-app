using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentValidation;
using Imi.Project.Mobile.Helpers;
using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Models;
using Imi.Project.Mobile.ViewModels.Base;
using Syncfusion.DataSource.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Imi.Project.Mobile.ViewModels
{
    public class RecipeActionsViewModel : BaseViewModel
    {
        #region Fields and Properties

        private ObservableCollection<Diet> _diets;
        private ObservableCollection<Category> _categories;
        private ObservableCollection<Ingredient> _ingredients;
        private ObservableCollection<Instruction> _instructions;
        private ObservableCollection<Unit> _units;
        private Ingredient _ingredient;
        private Instruction _instruction;
        private bool _isIngredientFormShown;
        private bool _isInstructionFormShown;
        private Diet _selectedDiet;
        private Category _selectedCategory;
        private Recipe _selectedRecipe;
        private Unit _selectedUnit;
        private string _pageTitle;
        private string _photoPath;
        private ImageSource _selectedPhoto;
        private IValidator _recipeValidator;
        private IValidator _ingredientValidator;
        private IValidator _instructionsValidator;
        private readonly ICategoryService _categoryService;
        private readonly IDietService _dietService;
        private readonly IUnitService _unitService;
        private readonly IRecipeService _recipeService;
        private readonly ICloudinaryService _cloudinaryService;


        public IDictionary<string, string> ErrorMessages { get; set; } = new Dictionary<string, string>();
        public ObservableCollection<Diet> Diets
        {
            get { return _diets; }
            set
            {
                _diets = value;
                OnPropertyChanged(nameof(Diets));
            }
        }
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged(nameof(Categories));
            }
        }
        public ObservableCollection<Unit> Units
        {
            get { return _units; }
            set
            {
                _units = value;
                OnPropertyChanged(nameof(Units));
            }
        }
        public Diet SelectedDiet
        {
            get { return _selectedDiet; }
            set
            {
                _selectedDiet = value;
                OnPropertyChanged(nameof(SelectedDiet));
            }
        }
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public Unit SelectedUnit
        {
            get { return _selectedUnit; }
            set
            {
                _selectedUnit = value;
                OnPropertyChanged(nameof(SelectedUnit));
            }
        }
        public Recipe SelectedRecipe
        {
            get { return _selectedRecipe; }
            set
            {
                _selectedRecipe = value;
                OnPropertyChanged(nameof(SelectedRecipe));
            }
        }
        public string PageTitle
        {
            get { return _pageTitle; }
            set
            {
                _pageTitle = value;
                OnPropertyChanged(nameof(PageTitle));
            }
        }
        public string PhotoPath
        {
            get { return _photoPath; }
            set
            {
                _photoPath = value;
                OnPropertyChanged(nameof(PhotoPath));
            }
        }
        public ImageSource SelectedPhoto
        {
            get { return _selectedPhoto; }
            set
            {
                _selectedPhoto = value;
                OnPropertyChanged(nameof(SelectedPhoto));
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
        public Ingredient Ingredient
        {
            get { return _ingredient; }
            set
            {
                _ingredient = value;
                OnPropertyChanged(nameof(Ingredient));
            }
        }
        public bool IsIngredientFormShown
        {
            get { return _isIngredientFormShown; }
            set
            {
                _isIngredientFormShown = value;
                OnPropertyChanged(nameof(IsIngredientFormShown));
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
        public Instruction Instruction
        {
            get { return _instruction; }
            set
            {
                _instruction = value;
                OnPropertyChanged(nameof(Instruction));
            }
        }
        public bool IsInstructionFormShown
        {
            get { return _isInstructionFormShown; }
            set
            {
                _isInstructionFormShown = value; OnPropertyChanged(nameof(IsInstructionFormShown));
            }
        }

        #endregion

        #region Commands
        public ICommand SaveCommand => new Command(async () => await OnSave());
        public ICommand CancelCommand => new Command(async () => await OnCancel());
        public ICommand TakeImageCommand => new Command(async () => await OnTakeImage());
        public ICommand ChooseImageCommand => new Command(async () => await OnChooseImage());

        public ICommand ToggleIngredientFormCommand => new Command(() => IsIngredientFormShown = !IsIngredientFormShown);
        public ICommand HideIngredientFormCommand => new Command(ResetIngredientForm);
        public ICommand AddIngredientCommand => new Command<Ingredient>(OnAddIngredient);
        public ICommand RemoveIngredientCommand => new Command<Ingredient>(OnRemoveIngredient);

        public ICommand ToggleInstructionFormCommand => new Command(() => IsInstructionFormShown = !IsInstructionFormShown);
        public ICommand HideInstructionFormCommand => new Command(ResetInstructionForm);
        public ICommand AddInstructionCommand => new Command<Instruction>(OnAddInstruction);
        public ICommand RemoveInstructionCommand => new Command<Instruction>(OnRemoveInstruction);

        #endregion

        public RecipeActionsViewModel(INavigationService navigationService,
            ICategoryService categoryService,
            IDietService dietService,
            IUnitService unitService,
            IRecipeService recipeService,
            IDialogService dialogService,
            IUserSettingsService userSettingsService,
            ICloudinaryService cloudinaryService)
            : base(navigationService, dialogService, userSettingsService)
        {
            _categoryService = categoryService;
            _dietService = dietService;
            _unitService = unitService;
            _recipeService = recipeService;
            _cloudinaryService = cloudinaryService;

            Ingredients = new ObservableCollection<Ingredient>();
            Ingredient = new Ingredient();
            Instructions = new ObservableCollection<Instruction>();
            Instruction = new Instruction();

            _recipeValidator = new RecipeValidator();
            _ingredientValidator = new IngredientValidator();
            _instructionsValidator = new InstructionValidator(Instructions);
        }

        public async override Task InitializeAsync(object parameter)
        {
            PageTitle = parameter == null ? "Add a new recipe" : "Update recipe";

            Diets = (await _dietService.GetAllDiets()).ToObservableCollection();
            Categories = (await _categoryService.GetAllCategories()).ToObservableCollection();
            Units = (await _unitService.GetAllUnits()).ToObservableCollection();

            SelectedDiet = Diets.FirstOrDefault();
            SelectedCategory = Categories.FirstOrDefault();
            SelectedUnit = Units.FirstOrDefault();

            if (parameter != null)
            {
                SelectedRecipe = parameter as Recipe;
                SelectedDiet = Diets.FirstOrDefault(d => d.Name.ToLower() == SelectedRecipe.Diet.ToLower());
                SelectedCategory = Categories.FirstOrDefault(c => c.Name.ToLower() == SelectedRecipe.Category.ToLower());
            }
            else
            {
                SelectedRecipe = new Recipe();
            }
        }


        #region Private Methods
        private bool Validate<T>(T entity, IValidator validationScheme)
        {
            ErrorMessages.Clear();

            var validationContext = new ValidationContext<T>(entity);
            var validationResult = validationScheme.Validate(validationContext);

            foreach (var e in validationResult.Errors)
            {
                ErrorMessages[e.PropertyName] = e.ErrorMessage;
            }

            OnPropertyChanged(nameof(ErrorMessages));

            return validationResult.IsValid;
        }
        private async Task OnSave()
        {
            SelectedRecipe.Category = this.SelectedCategory?.Name;
            SelectedRecipe.Diet = this.SelectedDiet?.Name;

            if (!Validate(SelectedRecipe, _recipeValidator))
            {
                return;
            }

            bool success = false;
            bool isAddOperation = false;

            try
            {
                IsBusy = true;
                if (!string.IsNullOrEmpty(PhotoPath))
                {

                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(PhotoPath),
                        UseFilename = true,
                        UniqueFilename = false,
                        Overwrite = true,
                    };

                    var cloudinary = _cloudinaryService.GetCloudinaryInstance();
                    var uploadResult = await cloudinary.UploadAsync(uploadParams);
                    SelectedRecipe.ImgURL = uploadResult.SecureUrl.ToString();

                }

                if (SelectedRecipe.Id == Guid.Empty)
                {
                    isAddOperation = true;

                    var recipe = await _recipeService.AddRecipe(SelectedRecipe);
                    SelectedRecipe = recipe;
                    await PostIngredientsAsync(recipe);
                    await PostInstructionsAsync(recipe);

                    success = true;

                }
                else
                {
                    isAddOperation = false;

                    await _recipeService.UpdateRecipe(SelectedRecipe);

                    success = true;
                }

                MessagingCenter.Send(this, Constants.Messages.RecipesChangedMessage);
                await _navigationService.GoBackAsync();
            }
            catch (Exception ex)
            {
                await _dialogService.ShowDialog("There was an error processing your request. Please try again", "Error", "Ok");
            }
            finally
            {
                IsBusy = false;
                if (!success && isAddOperation)
                {
                    if (SelectedRecipe.Id != Guid.Empty)
                    {
                        await _recipeService.DeleteRecipe(SelectedRecipe.Id);
                    }
                }
            }
        }
        private async Task OnCancel()
        {
            await _navigationService.GoBackAsync();
        }
        private async Task OnTakeImage()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
                if (photo == null)
                {
                    return;
                }
                await LoadPhotoAsync(photo);
                SelectedPhoto = ImageSource.FromFile(photo.FullPath);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await _dialogService.ShowDialog(fnsEx.Message, "Error", "Ok");
            }
            catch (PermissionException pEx)
            {
                await _dialogService.ShowDialog(pEx.Message, "Error", "Ok");
            }
            catch (Exception ex)
            {
                await _dialogService.ShowDialog(ex.Message, "Error", "Ok");
            }
        }
        private async Task OnChooseImage()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                await LoadPhotoAsync(photo);
                SelectedPhoto = ImageSource.FromFile(photo.FullPath);
                Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await _dialogService.ShowDialog(fnsEx.Message, "Error", "Ok");
            }
            catch (PermissionException pEx)
            {
                await _dialogService.ShowDialog(pEx.Message, "Error", "Ok");
            }
            catch (Exception ex)
            {
                await _dialogService.ShowDialog(ex.Message, "Error", "Ok");
            }
        }
        private async Task LoadPhotoAsync(FileResult photo)
        {

            if (photo == null)
            {
                PhotoPath = null;
                return;
            }

            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);

            PhotoPath = newFile;
        }
        private void OnAddIngredient(Ingredient ingredient)
        {
            ingredient.Unit = SelectedUnit.Name;

            if (!Validate(ingredient, _ingredientValidator))
            {
                return;
            }

            Ingredient ing = new Ingredient
            {
                Unit = SelectedUnit.Name,
                Name = ingredient.Name,
                Amount = ingredient.Amount,
            };

            Ingredients.Add(ing);
            ResetIngredientForm();
        }
        private void OnRemoveIngredient(Ingredient ingredient)
        {
            Ingredients.Remove(ingredient);
        }
        private void ResetIngredientForm()
        {
            Ingredient.Name = string.Empty;
            Ingredient.Amount = 0;
            SelectedUnit = Units[0];
            IsIngredientFormShown = false;
        }
        private void OnAddInstruction(Instruction instruction)
        {

            if (!Validate(instruction, _instructionsValidator))
            {
                return;
            }

            Instruction instr = new Instruction
            {
                StepNumber = instruction.StepNumber,
                Description = instruction.Description
            };

            Instructions.Add(instr);
            ResetInstructionForm();
        }
        private void OnRemoveInstruction(Instruction instruction)
        {
            Instructions.Remove(instruction);
        }
        private void ResetInstructionForm()
        {
            Instruction.StepNumber = 0;
            Instruction.Description = string.Empty;
            IsInstructionFormShown = false;
        }
        private async Task PostIngredientsAsync(Recipe recipe)
        {
            List<Task<Ingredient>> ingredientTasks = new List<Task<Ingredient>>();
            foreach (var ingredient in Ingredients)
            {
                var task = _recipeService.AddIngredient(recipe.Id, ingredient);
                ingredientTasks.Add(task);
            }
            await Task.WhenAll(ingredientTasks);
        }
        private async Task PostInstructionsAsync(Recipe recipe)
        {
            List<Task<Instruction>> instructionTasks = new List<Task<Instruction>>();
            foreach (var instruction in Instructions)
            {
                var task = _recipeService.AddInstruction(recipe.Id, instruction);
                instructionTasks.Add(task);
            }
            await Task.WhenAll(instructionTasks);
        }
        #endregion
    }
}
