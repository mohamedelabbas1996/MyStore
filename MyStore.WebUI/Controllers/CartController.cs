using MyStore.Domain.Abstract;
using MyStore.Domain.Entities;
using MyStore.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyStore.WebUI.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private IOrderProcessor orderProcessor; 
        private IProductsRepository repository;
        public CartController(IProductsRepository _repo, IOrderProcessor proc) {
            repository = _repo;
            orderProcessor = proc;
        
        }

        public RedirectToRouteResult AddToCart(Cart cart,int productID,string returnUrl) {
        Product product = repository.Products.Where(p=>p.ProductID == productID).FirstOrDefault();
            if (product != null){
            
            cart.AddItem(product,1);
            }
            return RedirectToAction("Index",new{ returnUrl});
        }
        public RedirectToRouteResult RemoveFromCart(Cart cart,int productID, string returnUrl) { 
        



        Product product = repository.Products.FirstOrDefault(p=>p.ProductID == productID);

            if (product!=null) cart.RemoveItem(product);

            return RedirectToAction("Index", new { returnUrl });

        }
        public PartialViewResult Summary(Cart cart) {
            return PartialView(cart);
        }
        private Cart GetCart() {
            Cart cart = (Cart)Session["Cart"];
            if (cart != null) {

                return cart;
            }
            cart = new Cart();
            Session["Cart"] = cart;
            return cart;


        
        }



        //
        // GET: /Cart/
        public ActionResult Index(Cart cart ,string returnUrl)
        {
            CartIndexViewModel viewModel = new CartIndexViewModel
            {
                Cart = cart,
                returnUrl = returnUrl
            };

            return View(viewModel);
        }
        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            if (cart.Lines.Count() == 0) {
                ModelState.AddModelError("", "Your Cart is empty, please select something ");
            
            }
            if (ModelState.IsValid)
            {
                //orderProcessor.ProessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Complete");
            }
            else {
                return View(shippingDetails);
            }
           


          
        }
        public ViewResult Checkout() {
            return View(new ShippingDetails());
        }
	}
}