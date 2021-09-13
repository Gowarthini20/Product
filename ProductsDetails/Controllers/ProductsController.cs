using ProductsDetails.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProductsDetails.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            IEnumerable<Product_Detail> products;
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Product").Result;
            products = response.Content.ReadAsAsync<IEnumerable<Product_Detail>>().Result;
            return View(products.Where(x=>x.IsActive== true).ToList());
        }
        public ActionResult Create(int id = 0)
        {

            return View(new Product_Detail());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Create(Product_Detail product)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.PostAsJsonAsync("Product", product).Result;
            TempData["successMessage"] = "Saved Successfully";
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.GetAsync("Product/" + id.ToString()).Result;
            return View(response.Content.ReadAsAsync<Product_Detail>().Result);
        }
        [HttpPost]
        public ActionResult Edit(Product_Detail product)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.PutAsJsonAsync("Product/" + product.ProductId, product).Result;
            TempData["successMessage"] = "Updated Successfully";
            return RedirectToAction("Index");
        }
        public ActionResult Delete(int id)
        {
            HttpResponseMessage response = GlobalVariable.WebApiClient.DeleteAsync("Product/" + id.ToString()).Result;
            TempData["successMessage"] = "Deletion is successful";
            return RedirectToAction("Index");

        }
    }
}