using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class ReturnHomeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //目標：若是 local 則轉頁到首頁
            if (filterContext.RequestContext.HttpContext.Request.IsLocal)
            {
                filterContext.Result = new RedirectResult("/");
            }

            base.OnActionExecuting(filterContext);
        }
    }
}