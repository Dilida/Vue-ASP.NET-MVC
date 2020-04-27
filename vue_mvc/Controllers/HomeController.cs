using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace vue_mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var mvcName = typeof(Controller).Assembly.GetName();
            var isMono = Type.GetType("Mono.Runtime") != null;

            ViewData["Version"] = mvcName.Version.Major + "." + mvcName.Version.Minor;
            ViewData["Runtime"] = isMono ? "Mono" : ".NET";
            ViewData["ControllData"] = "this is data from controller";
            ViewData["VueData"] = "this is data from vue";

            return View();
        }

        public ActionResult Vuetest()
        {
            return View(
               new Models.BindingModel()
               {
                   message = "This is message from model",
                   alert = "Server side message!",
                   guid = Guid.NewGuid()
               });
        }

    }
}
