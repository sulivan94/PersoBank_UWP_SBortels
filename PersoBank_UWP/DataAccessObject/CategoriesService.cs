using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PersoBank_UWP.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static PersoBank_UWP.Util.ApiConstants;

namespace PersoBank_UWP.DataAccessObject
{
    public class CategoriesService
    {
        public async Task<List<Category>> GetAllCategories()
        {
            var client = ServiceManager.getHttpClient();

            var categories = await client.GetStringAsync(new Uri(CategoryApiConstants.GetAllCategories));
            return JsonConvert.DeserializeObject<List<Category>>(categories);
        }

        public async Task<string> AddCategory(Category category)
        {
            var client = ServiceManager.getHttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, FormatJson);
            var result = await client.PostAsync(new Uri(CategoryApiConstants.AddCategory), content);
            var error = ServiceManager.AnalyzeRequestResult(result);
            return error;
        }

        public async Task<string> UpdateCategory(Category category)
        {
            var client = ServiceManager.getHttpClient();

            var content = new StringContent(JsonConvert.SerializeObject(category), Encoding.UTF8, FormatJson);
            var result = await client.PutAsync(string.Format(CategoryApiConstants.UpdateCategory, category.CategoryId), content);
            var error = ServiceManager.AnalyzeRequestResult(result);
            return error;
        }

        public async Task<BestCategory> GetCategoryWithHighestNumberOfTransactions(int nbJours)
        {
            var client = ServiceManager.getHttpClient();

            DateTime date = DateTime.Now.AddDays(-nbJours);
            var content = new StringContent(JsonConvert.SerializeObject(date), Encoding.UTF8, FormatJson);

            var result = await client.PostAsync(new Uri(CategoryApiConstants.BestCategory), content);
            var bestCategoryString = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<BestCategory>(bestCategoryString);
        }

        public async Task<List<Category>> GetCategoriesByAverageVariationPourcentage(int variationPourcentage)
        {
            var client = ServiceManager.getHttpClient();

            var categories = await client.GetStringAsync(string.Format(CategoryApiConstants.AverageVariation, variationPourcentage));
            return JsonConvert.DeserializeObject<List<Category>>(categories);
        }
    }
}
