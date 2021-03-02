using Moq;
using MyStore.Domain.Abstract;
using MyStore.Domain.Concrete;
using MyStore.Domain.Entities;
using Ninject;
using System.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver: IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel _kernel)
        {
            kernel = _kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType); 
        }
        public void AddBindings() {
            /*
              Mock<IProductsRepository> mock = new Mock<IProductsRepository>();
            mock.Setup(m => m.Products).Returns(new List<Product>
            {
                new Product {Name ="Chess Board", Price= 45},
                new Product {Name ="Tennis Ball",Price = 22 },
                 new Product {Name ="Football",Price = 12 },
                  new Product {Name ="Cooking Pan ",Price = 42 }



            });
              */
            kernel.Bind<IProductsRepository>().To<EFProductsRepository>();

            EmailSettings settings = new EmailSettings
            {
                WriteAsFile =false

            };
            kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>().WithConstructorArgument("settings",settings);
        
        }
    }
}