using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ProductsDetails.Models;

namespace ProductsDetails.Controllers
{
    public class ProductController : ApiController
    {
        private ProductEntities db = new ProductEntities();

        // GET: api/Product
        public IQueryable<Product_Detail> GetProduct_Detail()

        { 
            return db.Product_Detail;
        }

        // GET: api/Product/5
        [ResponseType(typeof(Product_Detail))]
        public IHttpActionResult GetProduct_Detail(int id)
        {
            Product_Detail product_Detail = db.Product_Detail.Find(id);
            if (product_Detail == null)
            {
                return NotFound();
            }

            return Ok(product_Detail);
        }

        // PUT: api/Product/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProduct_Detail(int id, Product_Detail product_Detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != product_Detail.ProductId)
            {
                return BadRequest();
            }

            db.Entry(product_Detail).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Product_DetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Product
        [ResponseType(typeof(Product_Detail))]
        public IHttpActionResult PostProduct_Detail(Product_Detail product_Detail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Product_Detail.Add(product_Detail);
            product_Detail.IsActive = true;
            db.SaveChanges();
            //try
            //{
            //    db.SaveChanges();
            //}
            //catch (DbUpdateException)
            //{
            //    if (Product_DetailExists(product_Detail.ProductId))
            //    {
            //        return Conflict();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return CreatedAtRoute("DefaultApi", new { id = product_Detail.ProductId }, product_Detail);
        }

        // DELETE: api/Product/5
        [ResponseType(typeof(Product_Detail))]
        public IHttpActionResult DeleteProduct_Detail(int id)
        {
            Product_Detail product_Detail = db.Product_Detail.Find(id);
            if (product_Detail == null)
            {
                return NotFound();
            }
            product_Detail.IsActive = false;
            //db.Product_Detail.Remove(product_Detail);
            db.SaveChanges();

            return Ok(product_Detail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Product_DetailExists(int id)
        {
            return db.Product_Detail.Count(e => e.ProductId == id) > 0;
        }
    }
}