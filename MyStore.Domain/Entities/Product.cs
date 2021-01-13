using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities
{
   public  class Product
    {
       public int ProductID { set; get; }
       public string Name { set; get; }
       public string Category { set; get; }
       public decimal Price { set; get; }
       public string Description { set; get; }

    }
}
