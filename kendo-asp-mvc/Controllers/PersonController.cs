using kendo_asp_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kendo_asp_mvc.Controllers
{
    [RoutePrefix("person")]
    public class PersonController : Controller
    {
        private static List<PersonModel> models = new List<PersonModel>();

        static PersonController()
        {
            models.Add(new PersonModel() { Country = "BG", Email = "e", Id = 0, Name = "n1" });
            models.Add(new PersonModel() { Country = "BG", Email = "e", Id = 1, Name = "n2" });
            models.Add(new PersonModel() { Country = "BG", Email = "e", Id = 2, Name = "n3" });
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [Route("list")]
        [HttpGet]
        public JsonResult List()
        {
            return this.Json(models, JsonRequestBehavior.AllowGet);
        }

        [Route("create")]
        [HttpPost]
        public JsonResult Create(PersonModel model)
        {
            model.Id = models.Count;
            models.Add(model);

            return this.Json(model);
        }

        [Route("destroy")]
        [HttpPost]
        public JsonResult Destroy(int id)
        {
            var existing = models.FirstOrDefault(m => m.Id == id);

            if(existing != null)
            {
                models.Remove(existing);
            }

            return this.Json(existing);
        }

        [Route("update")]
        [HttpPost]
        public JsonResult Update(PersonModel model)
        {
            var existing = models.FirstOrDefault(m => m.Id == model.Id);

            if (existing != null)
            {
                models.Remove(existing);
            }

            models.Add(model);

            return this.Json(existing);
        }
    }
}
