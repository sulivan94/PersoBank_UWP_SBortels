using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using PersoBank_UWP.DataAccessObject;
using PersoBank_UWP.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Windows.ApplicationModel.Resources;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace PersoBank_UWP.ViewModel
{
    public class ExpensesGestionViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private ICommand _researchCommand;
        private ICommand _editModelCommand;
        private List<Place> places = new List<Place>();
        private List<Category> categories = new List<Category>();

        public ExpensesGestionViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedTo()
        {
            ExpensesModels.Clear();
        }

        // ********** Propriétés **********

        private int _pourcentageAverageVariation = 10;
        public int PourcentageAverageVariation
        {
            get
            {
                return _pourcentageAverageVariation;
            }
            set
            {
                _pourcentageAverageVariation = value;
                if(_pourcentageAverageVariation != 0)
                {
                    RaisePropertyChanged("PourcentageAverageVariation");
                }
            }
        }

        private bool _isCategory = true;
        public bool IsCategory
        {
            get
            {
                return _isCategory;
            }
            set
            {
                _isCategory = value;
                if (_isCategory)
                {
                    RaisePropertyChanged("IsCategory");
                }
            }
        }

        private ObservableCollection<ExpensesGestionModel> _expensesGestionModels = new ObservableCollection<ExpensesGestionModel>();
        public ObservableCollection<ExpensesGestionModel> ExpensesModels
        {
            get
            {
                return _expensesGestionModels;
            }
            set
            {
                _expensesGestionModels = value;
                if(_expensesGestionModels != null)
                {
                    RaisePropertyChanged("ExpensesModels");
                }
            }
        }

        private ExpensesGestionModel _selectedGestionModel = null;
        public ExpensesGestionModel SelectedGestionModel
        {
            get
            {
                return _selectedGestionModel;
            }
            set
            {
                _selectedGestionModel = value;
                if(_selectedGestionModel != null)
                {
                    RaisePropertyChanged("SelectedGestionModel");
                }
            }
        }

        public ICommand ResearchCommand
        {
            get
            {
                if(_researchCommand == null)
                {
                    _researchCommand = new RelayCommand(() => Research());
                }
                return _researchCommand;
            }
        }

        public ICommand EditModelCommand
        {
            get
            {
                if (_editModelCommand == null)
                {
                    _editModelCommand = new RelayCommand(() => EditModel());
                }
                return _editModelCommand;
            }
        }

        // ********** Méthodes **********

        private void UpdateListModel()
        {
            if (PourcentageAverageVariation > 0)
            {
                if (_isCategory)
                    GetCategoriesByAverageVariationPourcentage();
                else
                    GetPlacesByAverageVariationPourcentage();
            }
            else
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                var msgDialog = new MessageDialog(loader.GetString("expensesGestionPourcentageError"));
                msgDialog.ShowAsync();
            }
        }

        private void Research()
        {
            UpdateListModel();
        }

        private void EditModel()
        {
            if (CanExecute())
            {
                if(IsCategory)
                {
                    Category category = GetSelectedCategory();
                    _navigationService.NavigateTo("UpdateCategoryPage", category);
                }
                else
                {
                    Place place = GetSelectedPlace();
                    _navigationService.NavigateTo("UpdatePlacePage", place);
                }
            }
        }

        private bool CanExecute()
        {
            return (SelectedGestionModel != null);
        }

        private Place GetSelectedPlace()
        {
            foreach (Place place in places)
            {
                if (place.PlaceId == SelectedGestionModel.IdModel)
                {
                    return place;
                }
            }
            return null;
        }

        private Category GetSelectedCategory()
        {
            foreach (Category category in categories)
            {
                if(category.CategoryId == SelectedGestionModel.IdModel)
                {
                    return category;
                }
            }
            return null;
        }

        private async Task GetCategoriesByAverageVariationPourcentage()
        {
            ObservableCollection<ExpensesGestionModel> listGestionModel = new ObservableCollection<ExpensesGestionModel>();
            ExpensesGestionModel gestionModel;
            var service = new CategoriesService();

            categories = await service.GetCategoriesByAverageVariationPourcentage(PourcentageAverageVariation);
            foreach(Category category in categories)
            {
                gestionModel = new ExpensesGestionModel()
                {
                    IdModel = category.CategoryId,
                    Average = category.AverageAmount,
                    Name = category.Label,
                    IsCategory = true
                };
                listGestionModel.Add(gestionModel);
            }
            ExpensesModels = listGestionModel;
           
            if(listGestionModel.Count == 0)
            {
                var loader = new ResourceLoader();
                ShowMessageDialog(loader.GetString("expensesGestionCategoryError"));
            }
        }

        private async Task GetPlacesByAverageVariationPourcentage()
        {
            ObservableCollection<ExpensesGestionModel> listGestionModel = new ObservableCollection<ExpensesGestionModel>();
            ExpensesGestionModel gestionModel;
            var service = new PlacesService();

            places = await service.GetPlacesByAverageVariationPourcentage(PourcentageAverageVariation);
            foreach (Place place in places)
            {
                gestionModel = new ExpensesGestionModel()
                {
                    IdModel = place.PlaceId,
                    Average = place.AverageAmount,
                    Name = place.Name,
                    IsCategory = false
                };
                listGestionModel.Add(gestionModel);
            }
            ExpensesModels = listGestionModel;

            if (listGestionModel.Count == 0)
            {
                var loader = new ResourceLoader();
                ShowMessageDialog(loader.GetString("expensesGestionPlaceError"));
            }
        }

        private void ShowMessageDialog(string error)
        {
            var msgDialog = new MessageDialog(error);
            msgDialog.ShowAsync();
        }
    }
}
