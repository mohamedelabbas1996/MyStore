using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyStore.WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { set; get; }
        public PageInfo PagingInfo { set; get; }
        public string CurrentCategory { set; get; } 
    }
}