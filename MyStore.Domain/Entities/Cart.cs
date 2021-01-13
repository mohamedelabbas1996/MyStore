﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities
{
   public  class Cart
    {
       private List<CartLine> lineCollection = new List<CartLine>();
       public void AddItem(Product product, int quantity) {
           CartLine line = lineCollection.Where(p => p.Product.ProductID == product.ProductID).FirstOrDefault();
           if (line == null)
           {
               lineCollection.Add(new CartLine { Product = product, Quantity = quantity }); 
           }
           else {
               line.Quantity += quantity;
           
           }
       
       }
       public void RemoveItem(Product product)
       {
           lineCollection.RemoveAll(p => p.Product.ProductID == product.ProductID);

       }
       public decimal ComputeTotalValue() {
           return lineCollection.Sum(x=>x.Product.Price * x.Quantity);
       }
       public void Clear() {
           lineCollection.Clear();
       }

       public IEnumerable<CartLine> Lines { get { return lineCollection; } }


       public class CartLine
       {
           public Product Product { set; get; }
           public int Quantity { set; get; }

       }
    }
}
