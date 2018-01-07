using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using PersoBank_UWP.DataAccessObject;
using PersoBank_UWP.Model;
using PersoBank_UWP.Util;
using PersoBank_UWP.Validations;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Navigation;

namespace PersoBank_UWP.ViewModel
{
    public class UpdateCategoryViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private TransactionsDetail _transactionsDetail = new TransactionsDetail();
        private Category _currentCategory = new Category();
        private DataValidation _dataValidation = new DataValidation();

        public UpdateCategoryViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
            CurrentCategory = (Category)e.Parameter;

            GetTransactionsDetailsByCategory();
        }

        // ********** Propriétés **********

        public Category CurrentCategory { get; set; }

        public TransactionsDetail CategoryTransactionsDetail
        {
            get
            {
                return _transactionsDetail;
            }
            set
            {
                _transactionsDetail = value;
                if (_transactionsDetail != null)
                {
                    RaisePropertyChanged("CategoryTransactionsDetail");
                }
            }
        }

        private ICommand _returnToCategoriesPage;
        public ICommand ReturnToCategoriesPage
        {
            get
            {
                if (_returnToCategoriesPage == null)
                {
                    _returnToCategoriesPage = new RelayCommand(() => GoBack());
                }
                return _returnToCategoriesPage;
            }
        }

        private ICommand _updateCategoryCommand;
        public ICommand UpdateCategoryCommand
        {
            get
            {
                if(_updateCategoryCommand == null)
                {
                    _updateCategoryCommand = new RelayCommand(() => UpdateCategoryAsync());
                }
                return _updateCategoryCommand;
            }
        }

        // ********** Méthodes **********

        private void GoBack()
        {
            GeneralConstants.GoBack(_navigationService);
        }

        private async Task GetTransactionsDetailsByCategory()
        {
            var service = new TransactionsService();
            CategoryTransactionsDetail = await service.GetTransactionsDetailsByCategory(CurrentCategory.CategoryId);
        }

        public async Task UpdateCategoryAsync()
        {
            string error = _dataValidation.CategoryDataValidation(CurrentCategory);
            if (error == null)
            {
                var service = new CategoriesService();
                error = await service.UpdateCategory(CurrentCategory);
            }
            ShowMessageDialog(error);
        }

        private void ShowMessageDialog(string error)
        {
            var msgDialog = new MessageDialog(String.Empty);
            if (error == null)
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                msgDialog.Content = string.Format(loader.GetString("modifyCategorySuccess"), CurrentCategory.Label);
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
