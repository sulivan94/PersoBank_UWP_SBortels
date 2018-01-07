using Newtonsoft.Json;
using PersoBank_UWP.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static PersoBank_UWP.Util.ApiConstants;

namespace PersoBank_UWP.DataAccessObject
{
    public class PlacesService
    {
        public async Task<List<Place>> GetAllPlaces()
        {
            var client = ServiceManager.getHttpClient();

            var places = await client.GetStringAsync(new Uri(PlaceApiConstants.GetAllPlaces));
            return JsonConvert.DeserializeObject<List<Place>>(places);
        }

        public async Task<string> AddPlace(Place place)
        {
            var client = ServiceManager.getHttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(place), Encoding.UTF8, FormatJson);
            var result = await client.PostAsync(new Uri(PlaceApiConstants.AddPlace), content);
            var error = ServiceManager.AnalyzeRequestResult(result);
            return error;
        }

        public async Task<string> UpdatePlace(Place place)
        {
            var client = ServiceManager.getHttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(place), Encoding.UTF8, FormatJson);
            var result = await client.PutAsync(string.Format(PlaceApiConstants.UpdatePlace, place.PlaceId), content);
            var error = ServiceManager.AnalyzeRequestResult(result);
            return error;
        }

        public async Task<string> DeletePlace(int placeId)
        {
            var client = ServiceManager.getHttpClient();

            var result = await client.DeleteAsync(string.Format(PlaceApiConstants.DeletePlace, placeId));
            var error = ServiceManager.AnalyzeRequestResult(result);
            return error;
        }

        public async Task<BestPlace> GetPlaceWithHighestNumberOfTransactions(int nbJours)
        {
            var client = ServiceManager.getHttpClient();

            DateTime date = DateTime.Now.AddDays(-nbJours);
            var content = new StringContent(JsonConvert.SerializeObject(date), Encoding.UTF8, FormatJson);
            var result = await client.PostAsync(new Uri(PlaceApiConstants.BestPlace), content);
            var bestPlaceString = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<BestPlace>(bestPlaceString);
        }

        public async Task<List<Place>> GetPlacesByAverageVariationPourcentage(int variationPourcentage)
        {
            var client = ServiceManager.getHttpClient();

            var places = await client.GetStringAsync(string.Format(PlaceApiConstants.AverageVariation, variationPourcentage));
            return JsonConvert.DeserializeObject<List<Place>>(places);
        }
    }
}
