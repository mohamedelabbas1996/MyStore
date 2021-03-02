using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyStore.Domain.Abstract;
using Moq;
using MyStore.Domain.Entities;
using System.Collections.Generic;
using MyStore.WebUI.Controllers;
using System.Linq;
using System.Web.WebPages.Html;
using MyStore.WebUI.Models;
using System.Web.Mvc;
using MyStore.WebUI.HtmlHelpers;
using Microsoft.CSharp;
namespace MyStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IProductsRepository> repository = new Mock<IProductsRepository>();
            repository.Setup(m => m.Products).Returns(new List<Product> { 
            
            new Product{ProductID = 1, Name="P1"},
            new Product{ProductID = 1, Name="P2"},
            new Product{ProductID = 1, Name="P3"},
            new Product{ProductID = 1, Name="P4"},
            new Product{ProductID = 1, Name="P5"},
            new Product{ProductID = 1, Name="P6"},
            
            });

            //Act
            ProductsController productsController = new ProductsController(repository.Object);
            productsController.PAGE_SIZE = 3;


            //Assert
            ProductsListViewModel result = (ProductsListViewModel)productsController.List(null, 2).Model;

            Product[] productsArray = result.Products.ToArray();
            Assert.IsTrue(productsArray.Length == 3);
            Assert.AreEqual(productsArray[0].Name, "P4");
            Assert.AreEqual(productsArray[1].Name, "P5");




        }
        [TestMethod]
        public void Can_Generate_PageLinks()
        {

            //Arrange

            System.Web.Mvc.HtmlHelper myHelper = null;

            PageInfo pageInfo = new PageInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemsPerPage = 10

            };

            Func<int, string> pageUrl = i => "Page" + i;


            //Act
            MvcHtmlString result = myHelper.PageLinks(pageInfo, pageUrl);
            //Assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
+ @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
+ @"<a class=""btn btn-default"" href=""Page3"">3</a>",
result.ToString());


        }
        [TestMethod]
        public void Can_Send_Pagination_ViewModel()
        {
            //Arrange
            Mock<IProductsRepository> repository = new Mock<IProductsRepository>();
            repository.Setup(m => m.Products).Returns(new List<Product> { 
            
            new Product { ProductID =1,Name = "P1", Price = 55},
             new Product {  ProductID =2,Name = "P2", Price = 55},
              new Product {  ProductID =3,Name = "P3", Price = 55},
              new Product {  ProductID =4,Name = "P4", Price = 55}

            
            });
            //Act
            ProductsController controller = new ProductsController(repository.Object);
            controller.PAGE_SIZE = 3;
            ProductsListViewModel productsVM = (ProductsListViewModel)controller.List(null, 2).Model;


            //Assert
            Assert.AreEqual(productsVM.PagingInfo.CurrentPage, 2);
            Assert.AreEqual(productsVM.PagingInfo.TotalItems, 4);
            Assert.AreEqual(productsVM.PagingInfo.ItemsPerPage, 3);
            Assert.AreEqual(productsVM.PagingInfo.TotalPages, 2);



        }
        [TestMethod]
        public void Can_Filter_Products()
        {
            //Arrange
            Mock<IProductsRepository> repository = new Mock<IProductsRepository>();
            repository.Setup(p => p.Products).Returns(new List<Product>()
            {
                new Product{ProductID = 2, Name="P1", Category="Cat1"},
                new Product{ProductID = 2, Name="P2", Category="Cat2"},
                new Product{ProductID = 2, Name="P3", Category="Cat1"},
                new Product{ProductID = 2, Name="P4", Category="Cat2"},
                new Product{ProductID = 2, Name="P5", Category="Cat1"},
                new Product{ProductID = 2, Name="P6", Category="Cat3"},


            });

            //Act
            ProductsController controller = new ProductsController(repository.Object);
            controller.PAGE_SIZE = 3;
            ProductsListViewModel result = (ProductsListViewModel)controller.List("Cat1", 1).Model;
            Product[] ProdArray = result.Products.ToArray();
            //Assert
            Assert.AreEqual(ProdArray.Length, 3);
            Assert.AreEqual(ProdArray[0].Name, "P1");
            Assert.AreEqual(ProdArray[1].Name, "P3");
            Assert.AreEqual(ProdArray[2].Name, "P5");
        }
        [TestMethod]
        public void Can_Navigate_To_Categories()
        {
            //Arrange
            Mock<IProductsRepository> repo = new Mock<IProductsRepository>();
            repo.Setup(m => m.Products).Returns(new List<Product>(){
            
            new Product(){ProductID =1 , Name="P1", Category = "Cat1"},
            new Product(){ProductID =2 , Name="P2", Category = "Cat1"},
            new Product(){ProductID =3 , Name="P3", Category = "Cat2"},
            new Product(){ProductID =4 , Name="P4", Category = "Cat1"},
            new Product(){ProductID =5 , Name="P5", Category = "Cat3"},
            new Product(){ProductID =6 , Name="P6", Category = "Cat3"},
            new Product(){ProductID =7 , Name="P7", Category = "Cat2"},
            
            
            });



            //Act
            NavController controller = new NavController(repo.Object);


            var categories = (IEnumerable<string>)controller.Menu().Model;
            string[] categoriesArr = categories.ToArray();


            //Assert 


            Assert.AreEqual(categoriesArr[0], "Cat1");
            Assert.AreEqual(categoriesArr[1], "Cat2");
            Assert.AreEqual(categoriesArr[2], "Cat3");
            Assert.AreEqual(categoriesArr.Length, 3);



        }
        [TestMethod]
        public void Can_Indicate_Selected_Category()
        {
            //Arrange
            Mock<IProductsRepository> repo = new Mock<IProductsRepository>();

            repo.Setup(p => p.Products).Returns(new List<Product>()
            {
                new Product(){ ProductID =1 , Name ="P1",Category = "Cat1"},
                 new Product(){ ProductID =2 , Name ="P2",Category = "Cat2"},
                  new Product(){ ProductID =3 , Name ="P3",Category = "Cat3"},
                   new Product(){ ProductID =4 , Name ="P4",Category = "Cat1"},
                    new Product(){ ProductID =5 , Name ="P5",Category = "Cat2"},
                     new Product(){ ProductID =6 , Name ="P6",Category = "Cat3"},

            });



            //Act
            string categoryToSelect = "Cat1";
            NavController controller = new NavController(repo.Object);
            string selectedCategory = controller.Menu(categoryToSelect).ViewBag.CurrentCategory;

            //Assert
            Assert.AreEqual(selectedCategory, categoryToSelect);

        }
        [TestMethod]
        public void Can_Generate_Category_Specific_Count()
        {
            //Arrange
            Mock<IProductsRepository> repo = new Mock<IProductsRepository>();
            repo.Setup(m => m.Products).Returns(new List<Product>() { 
            
            new Product(){ProductID =1 , Name="P1", Category="Cat1"}, 
            new Product(){ProductID =2 , Name="P2", Category="Cat2"},
            new Product(){ProductID =3 , Name="P3", Category="Cat1"},
            new Product(){ProductID =4 , Name="P4", Category="Cat2"},
            new Product(){ProductID =5 , Name="P5", Category="Cat3"},
            new Product(){ProductID =6 , Name="P6", Category="Cat1"},
            
            });


            //Act
            ProductsController controller = new ProductsController(repo.Object);
            int resAll = ((ProductsListViewModel)controller.List(null).Model).PagingInfo.TotalItems;
            int res1 = ((ProductsListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((ProductsListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((ProductsListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;


            //Assert
            Assert.AreEqual(resAll, 6);
            Assert.AreEqual(res1, 3);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 1);
        }
        [TestMethod]
        public void Cannot_Checkout_Empty_Cart()
        {
            //Arrange



            //Act
            //Assert


        }
        [TestMethod]
        public void Can_Make_Order() { }
        [TestMethod]
        public void Cannot_Send_Invalid_Shipping_Details() { }



    }

}
