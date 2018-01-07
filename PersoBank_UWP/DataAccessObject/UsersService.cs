using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PersoBank_UWP.Model;
using PersoBank_UWP.Util;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static PersoBank_UWP.Util.ApiConstants;

namespace PersoBank_UWP.DataAccessObject
{
    public class UsersService
    {
        public async Task<string> LogInUser(UserLogIn userLogIn)
        {
            string error = null;
            // Un utilisateur correspond au login introduit -> le login est correct...
            var user = GetUserByUserName(userLogIn.UserName).Result;
            if(user != null)
            {
                HttpClient client = new HttpClient();
                var content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("UserName", userLogIn.UserName),
                    new KeyValuePair<string, string>("Password", userLogIn.Password),
                    new KeyValuePair<string, string>("grant_type", "password")
                });
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(FormatToken));
                // ... on tente donc de récupérer un token, c'est-à-dire de vérifier que le couple login/password est exact
                var result = client.PostAsync(UrlTokenApi, content).Result;
                if(result.IsSuccessStatusCode)
                {
                    // Si l'utilisateur a entré des identifiants corrects, on stocke le token envoyé par l'api
                    var resultContent = await result.Content.ReadAsStringAsync();
                    var resultObject = JObject.Parse(resultContent);

                    Token = resultObject[("token_type")].Value<string>() + " " + resultObject[("access_token")].Value<string>();
                    UserName = user.UserName;
                    UserId = user.UserId;
                
                    return error;
                }               
                if(!((int)result.StatusCode == ApiConstants.BadRequest))
                {
                    // Une erreur s'est produite pendant la tentative de récupération du token
                    return error = ServiceManager.AnalyzeRequestResult(result);
                }
            }
            // Les identifiants saisis par l'utilisateur sont incorrects
            var loader = new Windows.ApplicationModel.Resources.ResourceLoader();
            return loader.GetString("connectionWrongId");
        }

        public async Task<User> GetUserByUserName(string userName)
        {
            var client = new HttpClient();
            var jsonUserName = JsonConvert.SerializeObject(userName);
            var byteContent = new ByteArrayContent(Encoding.UTF8.GetBytes(jsonUserName));
            byteContent.Headers.ContentType = new MediaTypeHeaderValue(FormatJson);

            var resultUser = client.PostAsync(UserApiConstants.GetByUserName, byteContent).Result;
            // Si l'utilisateur n'a pas été trouvé
            if((int)resultUser.StatusCode == ApiConstants.NotFound)
            {
                return null;
            }
            // S'il a été trouvé, on désérialise le contenu avant d'envoyer le résultat à la fonction appelante
            var user = await resultUser.Content.ReadAsStringAsync();
            var userObject = JsonConvert.DeserializeObject<User>(user);
            return userObject;
        }

        // Récupération du nombre de nouveaux inscrits
        public async Task<int> GetUserRegisteredAfterDate(int nbJours)
        {
            HttpClient client = ServiceManager.getHttpClient();

            DateTime date = DateTime.Now.AddDays(-nbJours);
            var content = new StringContent(JsonConvert.SerializeObject(date), Encoding.UTF8, FormatJson);
            var result = await client.PostAsync(new Uri(UserApiConstants.GetLastRegistered), content);

            var nbString = result.Content.ReadAsStringAsync().Result;
            return Convert.ToInt32(nbString);
        }
    }
}
