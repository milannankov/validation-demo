using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kendo_asp_mvc.Controllers
{
    public class KendoController : Controller
    {
        // GET: Kendo
        public ActionResult All()
        {
            return View();
        }

        public ActionResult Grid()
        {
            return this.View();
        }
    }
}