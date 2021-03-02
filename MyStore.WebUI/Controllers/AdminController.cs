using MyStore.Domain.Abstract;
using MyStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductsRepository repository;
        public AdminController(IProductsRepository repo)
        {
            repository = repo;
        }
        //

        // GET: /Admin/
        public ActionResult Index()
        {
            return View(repository.Products);
        }
        public ActionResult Create() {
            return View("Edit",new Product());
        }
        public ActionResult Edit(int ProductID) {
            Product product = repository.Products.FirstOrDefault(x=>x.ProductID == ProductID);
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid) { 
                repository.SaveProduct(product);
                TempData["message"] = string.Format("{0} has been saved successfully", product.Name);
                return RedirectToAction("Index", "Admin");
            }
            

            return View(product);
        }
        public ActionResult Delete(int ProductID) {
            var deletedProduct = repository.DeleteProduct(ProductID);
            if (deletedProduct != null) {
                TempData["message"] = string.Format("{0} has been deleted successfully", deletedProduct.Name);
            }
            return RedirectToAction("Index");
        
        }
       
	}
}