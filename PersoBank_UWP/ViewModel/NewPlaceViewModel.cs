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
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace PersoBank_UWP.ViewModel
{
    public class NewPlaceViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private ObservableCollection<Category> _categories = null;
        private Category _selectedCategory = new Category();
        private Place _newPlace = new Place();
        private DataValidation _dataValidation = new DataValidation();

        public NewPlaceViewModel(INavigationService navigationSevice = null)
        {
            _navigationService = navigationSevice;
        }

        public void OnNavigatedTo()
        {
            InitializeCategoriesComboBox();
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
                if(_selectedCategory != null)
                {
                    RaisePropertyChanged("SelectedCategory");
                }
            }
        }

        public Place NewPlace
        {
            get
            {
                return _newPlace;
            }
            set
            {
                _newPlace = value;
                if (_newPlace != null)
                {
                    RaisePropertyChanged("NewPlace");
                }
            }
        }

        private ICommand _returnToPlacesPage;
        public ICommand ReturnToPlacesPage
        {
            get
            {
                if(_returnToPlacesPage == null)
                {
                    _returnToPlacesPage = new RelayCommand(() => GoBack());
                }
                return _returnToPlacesPage;
            }
        }

        private ICommand _addPlaceCommand;
        public ICommand AddPlaceCommand
        {
            get
            {
                if(_addPlaceCommand == null)
                {
                    _addPlaceCommand = new RelayCommand(() => AddPlaceAsync());
                }
                return _addPlaceCommand;
            }
        }

        // ********** Méthodes **********

        private void GoBack()
        {
            GeneralConstants.GoBack(_navigationService);
        }

        private async Task InitializeCategoriesComboBox()
        {
            var service = new CategoriesService();
            var categories = await service.GetAllCategories();
            Categories = new ObservableCollection<Category>(categories);
        }

        public async Task AddPlaceAsync()
        {
            NewPlace.CategoryId = SelectedCategory.CategoryId;

            string error = _dataValidation.PlaceDataValidation(NewPlace);
            if (error == null)
            {
                var service = new PlacesService();
                error = await service.AddPlace(NewPlace);
            }
            ShowMessageDialog(error);
        }

        private void ShowMessageDialog(string error)
        {
            var msgDialog = new MessageDialog(String.Empty);
            if (error == null)
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                msgDialog.Content = string.Format(loader.GetString("addPlaceSuccess"), NewPlace.Name);
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
