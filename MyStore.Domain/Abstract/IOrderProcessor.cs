using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyStore.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProessOrder(Cart cart, ShippingDetails shippingDetails);
    }
}
