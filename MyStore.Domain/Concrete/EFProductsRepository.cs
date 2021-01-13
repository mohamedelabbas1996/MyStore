using MyStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Concrete
{
    public  class EFProductsRepository : IProductsRepository
    {
        EFDbContext context = new EFDbContext();  
        public IEnumerable<Entities.Product> Products
        {
            get { return context.Products; }
        }
    }
}
