using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace UndercuttersRepo
{
    public class UndercuttersRepo
    {

        public async Task<IEnumerable<RepriseMyProducks.Dtos.Product>> GetAllProducts()
        {
            var undercutterProducts = new List<RepriseMyProducks.Dtos.Product>().AsEnumerable();

            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri("http://undercutters.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            HttpResponseMessage response = await client.GetAsync("api/product");
            if (response.IsSuccessStatusCode)
            {
                undercutterProducts = await response.Content.ReadAsAsync<IEnumerable<RepriseMyProducks.Dtos.Product>>();
            }
            else
            {
                Debug.WriteLine("Index received a bad response from the web service.");
            }

            return undercutterProducts;
        }
    }
}
