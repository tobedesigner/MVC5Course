using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class MBController : Controller
    {
        ClientRepository CRepo;

        public MBController()
        {
            CRepo = RepositoryHelper.GetClientRepository();
        }

        //介紹資料繫結
        public ActionResult Index()
        {
            var data = "Hello World";
            //ViewData.Model = "Hello World";
            return View(data); //此寫法可以有誤，會讓程式誤以為要開啟 "Hello World" 這個 View
        }

        public ActionResult ViewBagDemo()
        {
            ViewBag.Text = "Hi";
            return View();
        }

        public ActionResult ViewDataDemo()
        {
            ViewData["_Data"] = "_DataResult";
            ViewData["Clients"] = CRepo.All().Take(10).ToList();
            return View();
        }

        public ActionResult TempDataSave()
        {
            TempData["SaveAtSession"] = "資料只能使用一次";
            return RedirectToAction("TempDataDemo");
        }

        public ActionResult TempDataDemo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult FormTest(FormCollection form)
        {
            //不會產生 ModelState， FormCollection 不管前端傳什麼都吃
            //使用方式 ex: form[""]
            if (ModelState.IsValid)
            {

                return RedirectToAction("Index");
            }

            return View();
        }
    }
}