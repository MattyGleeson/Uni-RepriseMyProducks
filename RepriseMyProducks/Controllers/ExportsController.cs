using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace RepriseMyProducks.Controllers
{
    public class ExportsController : ApiController
    {
        private Producks.Model.StoreDb db = new Producks.Model.StoreDb();

        // GET: api/Brands
        [HttpGet]
        [Route("api/Brands")]
        public IEnumerable<Dtos.Brand> GetBrands()
        {
            return db.Brands
                     .AsEnumerable()
                     .Where(b => b.Active)
                     .Select(b => new Dtos.Brand
                     {
                        Id = b.Id,
                        Name = b.Name,
                        Active = b.Active
                     });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [HttpGet]
        [Route("api/Categories")]
        public IEnumerable<Dtos.Category> GetCategories()
        {
            return db.Categories
                .AsEnumerable()
                .Where(c => c.Active)
                .Select(c => new Dtos.Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    Active = c.Active
                });
        }

        // GET api/Products/3/1/0.50/9.99
        // GET api/Products?categoryId=1&brandId=2&minPrice=0.50
        [HttpGet]
        [Route("api/Products")]
        public IEnumerable<Dtos.Product> GetProducts(int? catId = null, int? brandId = null, double? minPrice = 0, double? maxPrice = double.MaxValue)
        {
            var products = db.Products.AsQueryable();

            if (catId != null)
                products = products.Where(p => p.CategoryId == catId).AsQueryable();

            if (brandId != null)
                products = products.Where(p => p.BrandId == brandId);

            products = products.Where(p => p.Price >= minPrice && p.Price <= maxPrice && p.Active);

            return products.AsEnumerable()
                .Select(p => new Dtos.Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price,
                    StockLevel = p.StockLevel,
                    Active = p.Active,
                    CategoryId = p.CategoryId,
                    BrandId = p.BrandId
                });
        }
    }
}