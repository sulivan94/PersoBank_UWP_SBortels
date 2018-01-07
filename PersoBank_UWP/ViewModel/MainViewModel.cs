using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Views;
using PersoBank_UWP.DataAccessObject;
using PersoBank_UWP.Model;
using PersoBank_UWP.Validations;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;

namespace PersoBank_UWP.ViewModel
{
    public class MainViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private UsersService _service = new UsersService();
        private DataValidation _userValidation = new DataValidation();
        private INavigationService _navigationService;

        public MainViewModel(INavigationService navigationService = null)
        {
            _navigationService = navigationService;
        }

        // ********** Propriétés **********

        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                if (_userName != null)
                {
                    RaisePropertyChanged("UserName");
                }
            }
        }

        private string _userPassword;
        public string UserPassword
        {
            get
            {
                return _userPassword;
            }
            set
            {
                _userPassword = value;
                if (_userPassword != null)
                {
                    RaisePropertyChanged("UserPassword");
                }
            }
        }

        private ICommand _goToHomePageCommand;
        public ICommand GoToHomePageCommandLogged
        {
            get
            {
                if(_goToHomePageCommand == null)
                {
                    _goToHomePageCommand = new RelayCommand(() => GoToHomePageLogged());
                }
                return _goToHomePageCommand;
            }
        }

        // ********* Méthodes **********

        private void GoToHomePageLogged()
        {
            var error = LogInUserAsync().Result;
            if(error == null)
            {
                GoToHomePage();
            }
            else
            {
                var msgDialog = new MessageDialog(error);
                msgDialog.ShowAsync();
            }
        }

        private void GoToHomePage()
        {
            _navigationService.NavigateTo("HomePage");
        }

        private async Task<string> LogInUserAsync()
        {
            string error = null;
            UserLogIn userLogIn = new UserLogIn()
            {
                UserName = this.UserName,
                Password = UserPassword
            };

            string validationMsg = _userValidation.UserLogInDataValidation(userLogIn);
            if (validationMsg == null)
            {
                error = await _service.LogInUser(userLogIn);
            }
            else
            {
                error = validationMsg;
            }
            return error;
        }
    }
}
