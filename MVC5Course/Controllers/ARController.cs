using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ARController : Controller
    {
        // GET: AR
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewTest()
        {
            string data = "提供給 ViewTest Page 的文字資料";
            return View((object)data);
        }

        public ActionResult PartialViewTest()
        {
            //※PartialView 不回傳 Layout Page
            string data = "提供給 PartialViewTest Page 的文字資料";
            return PartialView("ViewTest", (object)data);
        }

        public ActionResult FileTest(string dl)
        {
            return dl == "1" ?
            File(Server.MapPath("~/App_Data/fileTest.jpg"), "image/jpeg", "fileTest.jpg")
            : File(Server.MapPath("~/App_Data/fileTest.jpg"), "image/jpeg");
        }
    }
}