using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using PersoBank_UWP.DataAccessObject;
using PersoBank_UWP.Model;
using PersoBank_UWP.Util;
using PersoBank_UWP.Validations;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace PersoBank_UWP.ViewModel
{
    public class UpdatePlaceViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private Category _selectedCategory;
        private ObservableCollection<Category> _categories = null;
        private TransactionsDetail _transactionsDetail = new TransactionsDetail();
        private DataValidation _dataValidation = new DataValidation();

        public Place CurrentPlace { get; set; }

        public UpdatePlaceViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrentPlace = (Place)e.Parameter;
            InitializeCategoriesComboBox();
            GetTransactionsDetailsByPlace();
        }

        // ********** Propriétés **********

        public ObservableCollection<Category> Categories
        {
            get
            {
                return _categories;
            }
            set
            {
                if (_categories == value)
                {
                    return;
                }
                _categories = value;
                RaisePropertyChanged("Categories");
            }
        }

        public Category SelectedCategory
        {
            get
            {
                return _selectedCategory;
            }
            set
            {
                _selectedCategory = value;
                if (_selectedCategory != null)
                {
                    RaisePropertyChanged("SelectedCategory");
                }
            }
        }

        public TransactionsDetail PlaceTransactionsDetail
        {
            get
            {
                return _transactionsDetail;
            }
            set
            {
                if(_transactionsDetail == value)
                {
                    return;
                }
                _transactionsDetail = value;
                RaisePropertyChanged("PlaceTransactionsDetail");
            }
        }

        private ICommand _returnToPlacesPage;
        public ICommand ReturnToPlacesPage
        {
            get
            {
                if (_returnToPlacesPage == null)
                {
                    _returnToPlacesPage = new RelayCommand(() => GoBack());
                }
                return _returnToPlacesPage;
            }
        }

        private ICommand _updatePlaceCommand;
        public ICommand UpdatePlaceCommand
        {
            get
            {
                if(_updatePlaceCommand == null)
                {
                    _updatePlaceCommand = new RelayCommand(() => UpdatePlaceAsync());
                }
                return _updatePlaceCommand;
            }
        }

        // ********** Méthodes **********

        private void GoBack()
        {
            GeneralConstants.GoBack(_navigationService);
        }

        private async Task GetTransactionsDetailsByPlace()
        {
            var service = new TransactionsService();
            PlaceTransactionsDetail = await service.GetTransactionsDetailsByPlace(CurrentPlace.PlaceId);
        }

        private async Task InitializeCategoriesComboBox()
        {
            var service = new CategoriesService();
            var categories = await service.GetAllCategories();
            Categories = new ObservableCollection<Category>(categories);

            SelectedCategory = Categories.First(c => c.CategoryId == CurrentPlace.CategoryId);
        }

        public async Task UpdatePlaceAsync()
        {
            string error = _dataValidation.PlaceDataValidation(CurrentPlace);
            if (error == null)
            {
                var service = new PlacesService();
                error = await service.UpdatePlace(CurrentPlace);
            }
            ShowMessageDialog(error);
        }

        private void ShowMessageDialog(string error)
        {
            var msgDialog = new MessageDialog(String.Empty);
            if (error == null)
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                msgDialog.Content = string.Format(loader.GetString("modifyPlaceSuccess"), CurrentPlace.Name);
                GoBack();
            }
            else
            {
                msgDialog.Content = error;
            }
            msgDialog.ShowAsync();
        }
    }
}
