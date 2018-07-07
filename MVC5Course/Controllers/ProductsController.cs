using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using Omu.ValueInjecter;

namespace MVC5Course.Controllers
{
    public class ProductsController : Controller
    {
        private FabricsEntities1 db = new FabricsEntities1();

        // GET: Products
        public ActionResult Index()
        {
            var data = db.Product
                .OrderByDescending(c => c.ProductId)
                .Take(10).ToList();

            return View(data);
            //return View(db.Product.ToList()); 
        }

        public ActionResult Index2()
        {
            var data = db.Product
                .Where(c => c.Active == true)
                .OrderByDescending(c => c.ProductId)
                .Take(10)
                .Select(p => new ProductViewModel
                {
                    ProductName = p.ProductName,
                    ProductId = p.ProductId,
                    Price = p.Price,
                    Stock = p.Stock
                });

            return View(data);
        }

        public ActionResult AddNewProduct()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddNewProduct(ProductViewModel data)
        {
            //if (ModelState.IsValid)
            //{

            //    return RedirectToAction("Index");
            //}

            var product = new Product()
            {
                ProductId = data.ProductId,
                Active = true,
                ProductName = data.ProductName,
                Price = data.Price,
                Stock = data.Stock
            };

            db.Product.Add(product);
            db.SaveChanges();
            
            //return View(data);
            return RedirectToAction("Index2");
        }

        public ActionResult EditProduct(int id)
        {
            var data = db.Product.Find(id);

            return View(data);
        }

        public ActionResult DeleteProduct(int id)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index2");
            }

            var fineOne = db.Product.Find(id);
            db.Product.Remove(fineOne);

            db.SaveChanges();

            return RedirectToAction("Index2");
        }

        [HttpPost]
        public ActionResult EditProduct(int id, ProductViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }

            var one = db.Product.Find(id);
            var a = db.Product;
            var b = db.Product.AsQueryable();
            var c = db.Product.ToList();



            //使用 ValueInjecter 改善修改後的處理方式
            one.InjectFrom(data);

            //one.ProductName = data.ProductName;
            //one.Price = data.Price;
            //one.Stock = data.Stock;

            db.SaveChanges();

            return RedirectToAction("Index2");
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
