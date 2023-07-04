﻿using BlazorWebAssemblyIndentityDemo.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWebAssemblyIndentityDemo.Domain.Entities
{
    public class ProductCategory : BaseAuditableEntity
    {
        public ProductCategory()
        {
            Products = new HashSet<Product>();    
        }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}