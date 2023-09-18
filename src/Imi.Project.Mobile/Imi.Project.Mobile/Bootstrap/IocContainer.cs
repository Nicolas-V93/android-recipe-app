using Autofac;
using Imi.Project.Mobile.Interfaces;
using Imi.Project.Mobile.Repositories;
using Imi.Project.Mobile.Services;
using Imi.Project.Mobile.ViewModels;
using System;

namespace Imi.Project.Mobile.Bootstrap
{
    public class IocContainer
    {
        private static IContainer _container;

        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();

            //View Models
            builder.RegisterType<RecipesViewModel>();
            builder.RegisterType<RecipeDetailsViewModel>();
            builder.RegisterType<RecipeActionsViewModel>();
            builder.RegisterType<LandingViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<RegisterViewModel>();

            //Services
            builder.RegisterType<RecipeService>().As<IRecipeService>();
            builder.RegisterType<CategoryService>().As<ICategoryService>();
            builder.RegisterType<DietService>().As<IDietService>();
            builder.RegisterType<UnitService>().As<IUnitService>();

            builder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            builder.RegisterType<DialogService>().As<IDialogService>().SingleInstance();
            builder.RegisterType<CloudinaryService>().As<ICloudinaryService>().SingleInstance();

            builder.RegisterType<AuthenticationService>().As<IAuthenticationService>();
            builder.RegisterType<UserSettingsService>().As<IUserSettingsService>();

            builder.RegisterType<BaseRepository>().As<IBaseRepository>();

            _container = builder.Build();

        }

        public static object Resolve(Type type)
        {
            return _container.Resolve(type);
        }

        public static T Resolve<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
