
using GalaSoft.MvvmLight.Views;
using System.Collections.ObjectModel;

namespace PersoBank_UWP.Util
{
    public static class ApplicationConstants
    {
    }

    public static class GeneralConstants
    {
        public const decimal WrongAmount = -0.123456789m;
        public const int WrongInteger = -123456789;
        public static ObservableCollection<int> NbDays = new ObservableCollection<int>() { 7, 15, 30, 90, 180, 365 };

        public static void GoBack(INavigationService navigationService)
        {
            navigationService.GoBack();
        }
    }

    public static class ApiConstants
    {
        public const int BadRequest = 400;
        public const int Unauthorized = 401;
        public const int NotFound = 404;
        public const int InternalServerError = 500;
        public const int ServiceUnavailable = 503;

        public static string Token;
        public static string UserName;
        public static string UserId;

        public const string Authorization = "Authorization";
        public const string FormatJson = "application/json";
        public const string FormatToken = "application/x-www-form-urlencoded";
        public const string UsernameToken = "Username=";
        public const string PasswordToken = "&Password=";
        public const string GrantTypeToken = "&grant_type=password";
        internal const string UrlApi = "http://persobank.azurewebsites.net";
        internal const string UrlBase = UrlApi + "/api";
        public const string UrlTokenApi = UrlApi + "/token";

        public class UserApiConstants
        {
            private const string UserUrlBase = UrlBase + "/Users";
            public const string GetByUserName = UserUrlBase + "/GetByUserName";
            public const string GetLastRegistered = UserUrlBase + "/GetLastRegistered";
        }

        public class CategoryApiConstants
        {
            private const string CategoryUrlBase = UrlBase + "/Categories";
            public const string GetAllCategories = CategoryUrlBase;
            public const string AddCategory = CategoryUrlBase;
            public const string UpdateCategory = CategoryUrlBase + "/{0}";
            public const string BestCategory = CategoryUrlBase + "/BestCategory";
            public const string AverageVariation = CategoryUrlBase + "/AverageVariation/{0}";
        }

        public class PlaceApiConstants
        {
            private const string PlaceUrlBase = UrlBase + "/Places";
            public const string GetAllPlaces = PlaceUrlBase;
            public const string AddPlace = PlaceUrlBase;
            public const string UpdatePlace = PlaceUrlBase + "/{0}";
            public const string DeletePlace = PlaceUrlBase + "/{0}";
            public const string BestPlace = PlaceUrlBase + "/BestPlace";
            public const string AverageVariation = PlaceUrlBase + "/AverageVariation/{0}";
        }
    }
}
