using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Producks.Model;
using System.Net.Http;
using System.Diagnostics;

namespace RepriseMyProducks.Controllers
{
    public class StoreController : Controller
    {

        private StoreDb db = new StoreDb();

        // GET: Store
        public ActionResult Index(string catName = null, string brandName = null)
        {
            ViewBag.Categories = new SelectList( db.Categories.AsEnumerable().Where(c => c.Active).Select(c => c.Name));
            ViewBag.Brands = new SelectList(db.Brands.AsEnumerable().Where(b => b.Active).Select(b => b.Name));

            var products = db.Products.AsEnumerable().Where(p => p.Active);
            var undercutterProducts = new List<Product>().AsEnumerable();

            HttpClient client = new HttpClient();
            client.BaseAddress = new System.Uri("http://undercutters.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");
            HttpResponseMessage response = client.GetAsync("api/product").Result;
            if (response.IsSuccessStatusCode)
            {
                undercutterProducts = response.Content.ReadAsAsync<IEnumerable<Product>>().Result;
            }
            else
            {
                Debug.WriteLine("Index received a bad response from the web service.");
            }

            products.Concat(undercutterProducts);


            if (catName != null && catName != "")
            {
                products = products.Where(p => p.Category.Name == catName);
            }

            if (brandName != null && brandName != "")
            {
                products = products.Where(p => p.Brand.Name == brandName);
            }


            return View(products);
        }

        public ActionResult View(int catId)
        {
            ViewBag.Shit = "hello";
            return View(db.Products.AsEnumerable()
                .Where(p => p.CategoryId == catId && p.Active)
                .Select(p => new VMs.Product
                {
                    Id = p.Id,
                    CategoryId = p.CategoryId,
                    BrandId = p.BrandId,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockLevel = p.StockLevel,
                    Active = p.Active,
                    CategoryName = p.Category.Name,
                    BrandName = p.Brand.Name
                }));
        }
    }
}