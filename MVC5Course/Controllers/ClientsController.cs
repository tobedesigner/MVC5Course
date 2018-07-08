using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;

namespace MVC5Course.Controllers
{
    [RoutePrefix("Client")]
    public class ClientsController : Controller
    {
        //private FabricsEntities1 db = new FabricsEntities1();
        //ClientRepository repo = new ClientRepository();
        ClientRepository repo = RepositoryHelper.GetClientRepository();

        [Route("")]
        public ActionResult Index()
        {
            //var client = db.Client.Include(c => c.Occupation);
            //return View(client.OrderByDescending(c => c.ClientId).Take(100).ToList());
            //改用 Repository 實作
            var client = repo.All();
            return View(client.OrderByDescending(c => c.ClientId).Take(100).ToList());
        }

        [Route("Detail/{id}")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Client client = db.Client.Find(id);
            //改用 Repository 實作
            Client client = repo.All().FirstOrDefault(c => c.ClientId == id);

            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        [Route("Create")]
        public ActionResult Create()
        {
            //改用 Repository 實作
            var occupRepo = RepositoryHelper.GetOccupationRepository();
            ViewBag.OccupationId = new SelectList(occupRepo.All(), "OccupationId", "OccupationName");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender," +
            "DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode," +
            "Longitude,Latitude,Notes, IdNumber")] Client client)
        {
            if (ModelState.IsValid)
            {
                //db.Client.Add(client);
                //db.SaveChanges();
                //改用 Repository 實作
                repo.Add(client);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            var occuRepo = RepositoryHelper.GetOccupationRepository();
            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        [Route("Edit/{id}")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Client client = db.Client.Find(id);
            //改用 Repository 實作
            Client client = repo.All().FirstOrDefault(c => c.ClientId == id);
            if (client == null)
            {
                return HttpNotFound();
            }
            var occuRepo = RepositoryHelper.GetOccupationRepository();
            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ClientId,FirstName,MiddleName,LastName,Gender,DateOfBirth,CreditRating,XCode,OccupationId,TelephoneNumber,Street1,Street2,City,ZipCode,Longitude,Latitude,Notes, IdNumber")] Client client)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(client).State = EntityState.Modified;
                //db.SaveChanges();
                //改用 Repository 實作
                var db = repo.UnitOfWork.Context;
                db.Entry(client).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            var occuRepo = RepositoryHelper.GetOccupationRepository();
            ViewBag.OccupationId = new SelectList(occuRepo.All(), "OccupationId", "OccupationName", client.OccupationId);
            return View(client);
        }

        // GET: Clients/Delete/5
        [Route("Destroy/{id}")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Client client = db.Client.Find(id);
            //改用 Repository 實作
            Client client = repo.All().FirstOrDefault(c => c.ClientId == id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Client client = db.Client.Find(id);
            //db.Client.Remove(client);
            //db.SaveChanges();
            //改用 Repository 實作
            Client client = repo.All().FirstOrDefault(c => c.ClientId == id);
            repo.Delete(client);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
                //改用 Repository 實作
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Search(string keyword)
        {
            //var data = db.Client.Take(100).AsQueryable();
            //改用 Repository 實作
            var data = repo.All().Take(100).AsQueryable();
            if (!string.IsNullOrEmpty(keyword))
            {   
                data = data.Where(c => c.FirstName.Contains(keyword));

                return View("Index", data);
            }

            return View("Index", data);
        }
    }
}
