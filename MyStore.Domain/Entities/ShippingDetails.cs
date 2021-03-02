using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage="Please Enter a Name")]
        public string Name { set; get; }

        [Required(ErrorMessage="Please Enter The First Address Line")]
        [Display(Name="Line 1")]
        public string Line1 { set; get; }
        [Display(Name = "Line 2")]
        public string Line2 { set; get; }
        [Display(Name = "Line 3")]
        public string Line3 { set; get; }

        [Required(ErrorMessage="Please Enter a City Name")]
        public string City { set; get; }
        [Required(ErrorMessage = "Please Enter a State Name")]
        public string State { set; get; }
        public string Zip { set; get; }
        [Required(ErrorMessage = "Please Enter a Country Name")]
        public string Country { set; get; }
        public bool GiftWrap { set; get; }
    }
}
