using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using PersoBank_UWP.DataAccessObject;
using PersoBank_UWP.Model;
using PersoBank_UWP.Util;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersoBank_UWP.ViewModel
{
    public class HomeViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;

        // ********** Constructeur **********

        public HomeViewModel(INavigationService navigationService = null)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedTo()
        {
            InitializeDashBoardAsync();
        }

        // ********** Propriétés **********

        private ObservableCollection<int> _nbDays = GeneralConstants.NbDays;
        public ObservableCollection<int> NbDays
        {
            get
            {
                return _nbDays;
            }
            set
            {
                _nbDays = value;
                if (_nbDays != null)
                {
                    RaisePropertyChanged("NbDays");
                }
            }
        }

        private int _nbDaysSelected = 30;
        public int NbDaysSelected
        {
            get
            {
                return _nbDaysSelected;
            }
            set
            {
                _nbDaysSelected = value;
                RaisePropertyChanged("NbDaysSelected");
                InitializeDashBoardAsync();
            }
        }

        private int _nbLastRegistered = 0;
        public int NbLastRegistered
        {
            get
            {
                return _nbLastRegistered;
            }
            set
            {
                _nbLastRegistered = value;
                RaisePropertyChanged("NbLastRegistered");
            }
        }

        private BestCategory _bestCategory = null;
        public BestCategory BestCategory
        {
            get
            {
                return _bestCategory;
            }
            set
            {
                _bestCategory = value;
                if(_bestCategory != null)
                {
                    RaisePropertyChanged("BestCategory");
                }
            }
        }

        private BestPlace _bestPlace = null;
        public BestPlace BestPlace
        {
            get
            {
                return _bestPlace;
            }
            set
            {
                _bestPlace = value;
                if (_bestPlace != null)
                {
                    RaisePropertyChanged("BestPlace");
                }
            }
        }

        private ICommand _goToCategoriesPageCommand;
        public ICommand GoToCategoriesPageCommand
        {
            get
            {
                if(_goToCategoriesPageCommand == null)
                {
                    _goToCategoriesPageCommand = new RelayCommand(() => GoToCategoriesPage());
                }
                return _goToCategoriesPageCommand;
            }
        }

        private ICommand _goToPlacesPageCommand;
        public ICommand GoToPlacesPageCommand
        {
            get
            {
                if(_goToPlacesPageCommand == null)
                {
                    _goToPlacesPageCommand = new RelayCommand(() => GoToPlacesPage());
                }
                return _goToPlacesPageCommand;
            }
        }

        private ICommand _goToUpdateAveragePageCommand;
        public ICommand GoToUpdateAveragePageCommand
        {
            get
            {
                if(_goToUpdateAveragePageCommand == null)
                {
                    _goToUpdateAveragePageCommand = new RelayCommand(() => GoToUpdateAveragePage());
                }
                return _goToUpdateAveragePageCommand;
            }
        }

        // ********** Méthodes **********

        private void GoToCategoriesPage()
        {
            _navigationService.NavigateTo("CategoriesPage");
        }

        private void GoToPlacesPage()
        {
            _navigationService.NavigateTo("PlacesPage");
        }

        private void GoToUpdateAveragePage()
        {
            _navigationService.NavigateTo("ExpensesGestionPage");
        }

        private async Task InitializeDashBoardAsync()
        {
            GetLastRegisteredUsers();
            GetBestCategory();
            GetBestPlace();
        }

        private async Task GetLastRegisteredUsers()
        {
            var service = new UsersService();
            NbLastRegistered = await service.GetUserRegisteredAfterDate(Convert.ToInt32(NbDaysSelected));
        }

        private async Task GetBestCategory()
        {
            var service = new CategoriesService();
            BestCategory = await service.GetCategoryWithHighestNumberOfTransactions(NbDaysSelected);
        }

        private async Task GetBestPlace()
        {
            var service = new PlacesService();
            BestPlace = await service.GetPlaceWithHighestNumberOfTransactions(NbDaysSelected);
        }
    }
}
