using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using PersoBank_UWP.DataAccessObject;
using PersoBank_UWP.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PersoBank_UWP.ViewModel
{
    public class CategoriesViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private ObservableCollection<Category> _categories = null;
        private ICommand _editCategoryCommand;
        private ICommand _addCategoryCommand;
        private Category _selectedCategory;

        public CategoriesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedTo()
        {
            InitializeCategoriesAsync();
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
                _categories = value;
                if(_categories != null)
                {
                    RaisePropertyChanged("Categories");
                }
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

        public ICommand EditCategoryCommand
        {
            get
            {
                if(this._editCategoryCommand == null)
                {
                    _editCategoryCommand = new RelayCommand(() => EditCategory());
                }
                return _editCategoryCommand;
            }
        }

        public ICommand AddCategoryCommand
        {
            get
            {
                if(this._addCategoryCommand == null)
                {
                    _addCategoryCommand = new RelayCommand(() => AddCategory());
                }
                return _addCategoryCommand;
            }
        }

        // ********** Méthodes **********

        private void EditCategory()
        {
            if (CanExecute())
            {
                _navigationService.NavigateTo("UpdateCategoryPage", SelectedCategory);
            }
        }

        private void AddCategory()
        {
            _navigationService.NavigateTo("NewCategoryPage");
        }

        private bool CanExecute()
        {
            return (SelectedCategory != null);
        }

        public async Task InitializeCategoriesAsync()
        {
            var service = new CategoriesService();
            var categories = await service.GetAllCategories();
            Categories = new ObservableCollection<Category>(categories);
        }
    }
}
