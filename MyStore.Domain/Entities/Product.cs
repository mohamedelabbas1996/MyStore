using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MyStore.Domain.Entities
{
   public  class Product
    {
       [HiddenInput(DisplayValue= false)]   
       public int ProductID { set; get; }
       [Required(ErrorMessage="Please enter a valid product name")]
       public string Name { set; get; }
       [Required(ErrorMessage = "Please enter a valid product category")]
       public string Category { set; get; }
       [Required]
       [Range(0.01,double.MaxValue,ErrorMessage="Please enter a valid price")]
       public decimal Price { set; get; }
       [Required(ErrorMessage = "Please enter a valid product description")]

       public string Description { set; get; }
       public string ImageMimeType { set; get; }
       public byte[] Imagedata { set; get; }

    }
}
