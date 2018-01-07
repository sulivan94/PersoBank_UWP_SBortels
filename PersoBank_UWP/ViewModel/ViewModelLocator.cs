using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using PersoBank_UWP.View;

namespace PersoBank_UWP.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<HomeViewModel>();
            SimpleIoc.Default.Register<CategoriesViewModel>();
            SimpleIoc.Default.Register<PlacesViewModel>();
            SimpleIoc.Default.Register<NewCategoryViewModel>();
            SimpleIoc.Default.Register<NewPlaceViewModel>();
            SimpleIoc.Default.Register<UpdateCategoryViewModel>();
            SimpleIoc.Default.Register<UpdatePlaceViewModel>();
            SimpleIoc.Default.Register<ExpensesGestionViewModel>();

            NavigationService navigationService = new NavigationService();
            SimpleIoc.Default.Unregister<INavigationService>();
            SimpleIoc.Default.Register<INavigationService>(() => navigationService);
            navigationService.Configure("ConnectionPage", typeof(MainPage));
            navigationService.Configure("HomePage", typeof(HomePage));
            navigationService.Configure("CategoriesPage", typeof(CategoriesPage));
            navigationService.Configure("PlacesPage", typeof(PlacesPage));
            navigationService.Configure("UpdateCategoryPage", typeof(UpdateCategoryPage));
            navigationService.Configure("UpdatePlacePage", typeof(UpdatePlacePage));
            navigationService.Configure("NewCategoryPage", typeof(NewCategoryPage));
            navigationService.Configure("NewPlacePage", typeof(NewPlacePage));
            navigationService.Configure("ExpensesGestionPage", typeof(ExpensesGestionPage));
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public HomeViewModel Home
        {
            get
            {
                return ServiceLocator.Current.GetInstance<HomeViewModel>();
            }
        }

        public CategoriesViewModel CategoriesVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<CategoriesViewModel>();
            }
        }

        public PlacesViewModel PlacesVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlacesViewModel>();
            }
        }

        public UpdateCategoryViewModel UpdateCategoryVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UpdateCategoryViewModel>();
            }
        }

        public UpdatePlaceViewModel UpdatePlaceVM
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UpdatePlaceViewModel>();
            }
        }

        public NewCategoryViewModel NewCategoryVM
        {
            get
            {
                return new NewCategoryViewModel(SimpleIoc.Default.GetInstance<INavigationService>());
            }
        }

        public NewPlaceViewModel NewPlaceVM
        {
            get
            {
                return new NewPlaceViewModel(SimpleIoc.Default.GetInstance<INavigationService>());
            }
        }

        public ExpensesGestionViewModel ExpensesGestionVM
        {
            get
            {
                return new ExpensesGestionViewModel(SimpleIoc.Default.GetInstance<INavigationService>());
            }
        }
    }
}
