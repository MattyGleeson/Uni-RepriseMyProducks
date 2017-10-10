using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepriseMyProducks.VMs
{
    public class StorePage
    {
        public virtual string Title { get; set; }

        public virtual IEnumerable<Producks.Model.Category> Categories { get; set; }

        public virtual IEnumerable<Producks.Model.Brand> Brands { get; set; }
    }
}