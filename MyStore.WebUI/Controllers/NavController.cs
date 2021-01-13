using MyStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IProductsRepository repository;
        public NavController(IProductsRepository _repo)
        {
            repository = _repo;
        }
        //
        // GET: /Nav/
        public PartialViewResult Menu(string CurrentCategory = null)
        {
            ViewBag.SelectedCategory = CurrentCategory; 
            var categories = repository.Products.Select(p => p.Category).Distinct().OrderBy(x=>x);
            return PartialView(categories);
        }
	}
}