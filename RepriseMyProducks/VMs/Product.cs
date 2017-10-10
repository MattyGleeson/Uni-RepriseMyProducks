using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RepriseMyProducks.VMs
{
    public class Product
    {
        [HiddenInput]
        public virtual int Id { get; set; }
        public virtual int CategoryId { get; set; }
        public virtual int BrandId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual double Price { get; set; }
        public virtual int StockLevel { get; set; }

        [DisplayName("In Stock")]
        public virtual string StockStatus
        {
            get
            {
                return StockLevel > 0 ? "In Stock" : "Out of Stock";
            }
        }

        public virtual bool InStock
        {
            get
            {
                return StockLevel > 0 ? true : false;
            }
        }
        public virtual bool Active { get; set; }
        
        [DisplayName("Category")]
        public virtual string CategoryName { get; set; }

        [DisplayName("Brand")]
        public virtual string BrandName { get; set; }
    }
}