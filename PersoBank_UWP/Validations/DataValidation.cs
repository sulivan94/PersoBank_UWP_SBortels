using PersoBank_UWP.Model;
using PersoBank_UWP.Util;
using System.Text.RegularExpressions;
using Windows.ApplicationModel.Resources;

namespace PersoBank_UWP.Validations
{
    public class DataValidation
    {
        private ResourceLoader loader = new ResourceLoader();
        private Regex _regex = new Regex(@"[A-Z]?[a-zéèê]+[ -']?[[A-Z]?[a-z]+[ -']?]*[a-z]+$");

        public string UserLogInDataValidation(UserLogIn userLogIn)
        {
            if (userLogIn.UserName == null)
                return loader.GetString("loginUserNameValidation");

            if (userLogIn.Password == null)
                return loader.GetString("loginPasswordValidation");

            return null;
        }

        public string PlaceDataValidation(Place place)
        {
            // Validation du nom du lieu
            if (string.IsNullOrWhiteSpace(place.Name))
                return loader.GetString("placeValidationEmptyName");

            if (place.Name.Length < 4 || place.Name.Length > 100)
                return loader.GetString("placeValidationLengthName");

            if (!_regex.IsMatch(place.Name))
                return loader.GetString("placeValidationWrongFormatName");

            // Validation du montant moyen
            if (place.AverageAmount != null)
            {
                if (place.AverageAmount == GeneralConstants.WrongAmount)
                    return loader.GetString("placeValidationWrongFormatAmount");

                if (place.AverageAmount < 1 && place.AverageAmount > 99999)
                    return loader.GetString("placeValidationBeetween1And99999Amount");
            }

            // Validation du nom de rue
            if (!string.IsNullOrWhiteSpace(place.Street))
            {
                if (place.Street.Length < 2 || place.Street.Length > 100)
                    return loader.GetString("placeValidationLengthStreet");

                if (!_regex.IsMatch(place.Street))
                    return loader.GetString("placeValidationWrongFormatStreet");
            }

            // Validation du numéro de rue
            if (place.StreetNumber != null)
            {
                if (place.StreetNumber == GeneralConstants.WrongInteger)
                    return loader.GetString("placeValidationWrongFormatNumber");

                if (place.StreetNumber < 1 || place.StreetNumber > 9999)
                    return loader.GetString("placeValidationBeetween1And9999Number");
            }

            // Validation du nom de la ville
            if (string.IsNullOrWhiteSpace(place.City))
                return loader.GetString("placeValidationEmptyCity");

            if (place.City.Length < 2 || place.City.Length > 100)
                return loader.GetString("placeValidationLengthCity");

            if (!_regex.IsMatch(place.City))
                return loader.GetString("placeValidationWrongFormatCity");

            // Validation du code postal
            if (place.PostalCode != null)
            {
                if (place.PostalCode == GeneralConstants.WrongInteger)
                    return loader.GetString("placeValidationWrongFormatPostalCode");

                if (place.PostalCode < 1 || place.PostalCode > 9999)
                    return loader.GetString("placeValidationBeetween1000And9999PostalCode");
            }

            // Validation de la catégorie
            if (place.CategoryId == 0)
                return loader.GetString("placeValidationEmptyCategory");

            return null;
        }

        public string CategoryDataValidation(Category category)
        {
            // Validation du nom de la catégorie
            if (string.IsNullOrWhiteSpace(category.Label))
                return loader.GetString("categoryValidationEmptyLabel");

            if (category.Label.Length < 2 || category.Label.Length > 100)
                return loader.GetString("categoryValidationLengthLabel");

            if (!_regex.IsMatch(category.Label))
                return loader.GetString("categoryValidationWrongFormat");

            // Validation du montant moyen
            if (category.AverageAmount != null)
            {
                if (category.AverageAmount == GeneralConstants.WrongAmount)
                    return loader.GetString("categoryValidationWrongFormatAmount");

                if (category.AverageAmount < 1 && category.AverageAmount > 99999)
                    return loader.GetString("categoryValidationBeetween1And99999Amount");
            }

            return null;
        }
    }
}
