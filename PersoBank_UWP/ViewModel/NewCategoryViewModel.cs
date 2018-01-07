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

namespace PersoBank_UWP.ViewModel
{
    public class NewCategoryViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private Category _newCategory = new Category();
        private DataValidation _dataValidation = new DataValidation();

        public NewCategoryViewModel(INavigationService navigationSevice = null)
        {
            _navigationService = navigationSevice;
        }

        // ********** Propriétés **********

        public Category NewCategory
        {
            get
            {
                return _newCategory;
            }
            set
            {
                _newCategory = value;
                if (_newCategory != null)
                {
                    RaisePropertyChanged("SelectedCategory");
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

        private ICommand _addCategoryCommand;
        public ICommand AddCategoryCommand
        {
            get
            {
                if(_addCategoryCommand == null)
                {
                    _addCategoryCommand = new RelayCommand(() => AddCategoryAsync());
                }
                return _addCategoryCommand;
            }
        }

        // ********** Méthodes **********

        private void GoBack()
        {
            GeneralConstants.GoBack(_navigationService);
        }

        private void ShowMessageDialog(string error)
        {
            var msgDialog = new MessageDialog(String.Empty);
            if (error == null)
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                msgDialog.Content = string.Format(loader.GetString("addCategorySuccess"), NewCategory.Label);
                GoBack();
            }
            else
            {
                msgDialog.Content = error;
            }
            msgDialog.ShowAsync();
        }

        public async Task AddCategoryAsync()
        {
            string error = _dataValidation.CategoryDataValidation(NewCategory);
            if (error == null)
            {
                var service = new CategoriesService();
                error = await service.AddCategory(NewCategory);
            }
            ShowMessageDialog(error);
        }
    }
}
