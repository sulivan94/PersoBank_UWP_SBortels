using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using PersoBank_UWP.DataAccessObject;
using PersoBank_UWP.Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace PersoBank_UWP.ViewModel
{
    public class PlacesViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private INavigationService _navigationService;
        private ObservableCollection<Place> _places;
        private Place _selectedPlace;

        private ICommand _editPlaceCommand;
        private ICommand _addPlaceCommand;
        private ICommand _deletePlaceCommand;

        public PlacesViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public void OnNavigatedTo()
        {
            InitializePlacesAsync();
        }

        // ********** Propriétés **********

        public Place SelectedPlace
        {
            get
            {
                return _selectedPlace;
            }
            set
            {
                _selectedPlace = value;
                if (_selectedPlace != null)
                {
                    RaisePropertyChanged("SelectedPlace");
                }
            }
        }

        public ObservableCollection<Place> Places
        {
            get
            {
                return _places;
            }
            set
            {
                if(_places == value)
                {
                    return;
                }
                _places = value;
                RaisePropertyChanged("Places");
            }
        }

        public async Task InitializePlacesAsync()
        {
            var service = new PlacesService();
            var places = await service.GetAllPlaces();
            Places = new ObservableCollection<Place>(places);
        }

        // ************** Ajout **************

        public ICommand AddPlaceCommand
        {
            get
            {
                if (this._addPlaceCommand == null)
                {
                    _addPlaceCommand = new RelayCommand(() => AddPlace());
                }
                return _addPlaceCommand;
            }
        }

        private void AddPlace()
        {
            _navigationService.NavigateTo("NewPlacePage");
        }

        // *********** Modification ***********

        public ICommand EditPlaceCommand
        {
            get
            {
                if (this._editPlaceCommand == null)
                {
                    _editPlaceCommand = new RelayCommand(() => EditPlace());
                }
                return _editPlaceCommand;
            }
        }

        private void EditPlace()
        {
            if (CanExecute())
            {
                _navigationService.NavigateTo("UpdatePlacePage", SelectedPlace);
            }
        }

        // *********** Suppression ***********

        public ICommand DeletePlaceCommand
        {
            get
            {
                if(this._deletePlaceCommand == null)
                {
                    _deletePlaceCommand = new RelayCommand(() => DeletePlace());
                }
                return _deletePlaceCommand;
            }
        }

        private async Task DeletePlace()
        {
            var msgDialog = new MessageDialog(String.Empty);
            if (CanExecute())
            {
                var service = new PlacesService();
                var error = await service.DeletePlace(SelectedPlace.PlaceId);

                ShowMessageDialog(error);            
            }
        }

        private bool CanExecute()
        {
            return (SelectedPlace != null);
        }

        private void ShowMessageDialog(string error)
        {
            var msgDialog = new MessageDialog(String.Empty);
            if (error == null)
            {
                var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
                msgDialog.Content = string.Format(loader.GetString("deletePlaceSuccess"), SelectedPlace.Name);
                OnNavigatedTo();
            }
            else
            {
                msgDialog.Content = error;
            }
            msgDialog.ShowAsync();
        }
    }
}
