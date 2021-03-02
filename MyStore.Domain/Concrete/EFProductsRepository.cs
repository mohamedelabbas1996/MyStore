using MyStore.Domain.Abstract;
using MyStore.Domain.Entities;
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
       


        void IProductsRepository.SaveProduct(Product product)
        {
            if (product.ProductID == 0)
            {
                context.Products.Add(product);

            }
            else
            {
                var dbProduct = context.Products.Find(product.ProductID);
                if (dbProduct != null)
                {
                    dbProduct.Name = product.Name;
                    dbProduct.Category = product.Category;
                    dbProduct.Price = product.Price;
                    dbProduct.Description = product.Description;

                }

            }
            context.SaveChanges();
        }


        public Product DeleteProduct(int ProductID)
        {
            var dbProduct = context.Products.Find(ProductID);
            if (dbProduct != null)
            {
                context.Products.Remove(dbProduct);
                context.SaveChanges();

            }
            return dbProduct;
                 
            }
    }
}
