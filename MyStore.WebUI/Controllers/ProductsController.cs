using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyStore.Domain.Abstract;
using MyStore.WebUI.Models;

namespace MyStore.WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private IProductsRepository repository;
        public int PAGE_SIZE = 4;
        public ProductsController(IProductsRepository _repo)
        {
            repository = _repo;

        }
        //
        // GET: /Products/
        public ViewResult List(string category, int page =1 )
        {
            ProductsListViewModel productsListVM = new ProductsListViewModel
            {
                Products = repository.Products.Where(p=>category == null || p.Category==category).OrderBy(x => x.ProductID).
                Skip((page - 1) * PAGE_SIZE).
                Take(PAGE_SIZE),

                PagingInfo = new PageInfo
                {
                    CurrentPage = page ,
                    ItemsPerPage = PAGE_SIZE,
                    TotalItems = repository.Products.Where(p => category == null || p.Category == category).Count()
                    ,
                   
                },
                CurrentCategory = category

            };
            ViewBag.CurrentCategory = category; 
            return View(productsListVM);
        }
	}
}