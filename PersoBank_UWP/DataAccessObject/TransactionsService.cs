using Newtonsoft.Json;
using PersoBank_UWP.Model;
using System;
using System.Threading.Tasks;

namespace PersoBank_UWP.DataAccessObject
{
    public class TransactionsService
    {
        const string urlBase = "http://localhost:61975/api/";

        public async Task<TransactionsDetail> GetTransactionsDetailsByCategory(int categoryId)
        {
            var client = ServiceManager.getHttpClient();

            var transactionsDetail = await client.GetStringAsync(new Uri(urlBase + "categories/" + categoryId + "/transactionsDetail"));
            return JsonConvert.DeserializeObject<TransactionsDetail>(transactionsDetail);
        }

        public async Task<TransactionsDetail> GetTransactionsDetailsByPlace(int placeId)
        {
            var client = ServiceManager.getHttpClient();

            var transactionsDetail = await client.GetStringAsync(new Uri(urlBase + "places/" + placeId + "/transactionsDetail"));
            return JsonConvert.DeserializeObject<TransactionsDetail>(transactionsDetail);
        }
    }
}