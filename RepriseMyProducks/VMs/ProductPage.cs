using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepriseMyProducks.VMs
{
    public class ProductPage
    {
        public virtual string Title { get; set; }

        public virtual IEnumerable<VMs.Product> Products { get; set; }
    }
}