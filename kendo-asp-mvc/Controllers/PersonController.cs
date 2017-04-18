using kendo_asp_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace kendo_asp_mvc.Controllers
{
    [RoutePrefix("person")]
    public partial class PersonController : Controller
    {
        private static List<PersonModel> models = new List<PersonModel>();
        private static int nextIndex = 1;

        static PersonController()
        {
            models.Add(new PersonModel() { Country = "BG", Email = "e", Id = 0, Name = "n1" });
            //models.Add(new PersonModel() { Country = "BG", Email = "e", Id = 1, Name = "n2" });
            //models.Add(new PersonModel() { Country = "BG", Email = "e", Id = 2, Name = "n3" });
        }

        public static int GetNextIndex()
        {
            nextIndex++;

            return nextIndex;
        }

        public ActionResult Index()
        {
            return this.View();
        }

        [Route("list")]
        [HttpGet]
        public JsonResult List()
        {
            var response = new ApiResponse(models);

            return this.Json(response, JsonRequestBehavior.AllowGet);
        }

        [Route("create")]
        [HttpPost]
        public JsonResult Create(PersonModel model)
        {
            if(this.ModelState.IsValid)
            {
                model.Id = GetNextIndex();
                models.Add(model);
            }

            // Can use MVC's DataSourceResult
            //var errors = this.ModelState.ToDictionary(k => k.Key, k => k.Value.Errors.Select(e => e.ErrorMessage));
            var errors = this.ModelState
                .Where(kvp => kvp.Value.Errors.Any())
                .Select(kvp => new {
                field = kvp.Key,
                error = kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
            }).ToList();

            var response = new ApiResponse(model, errors.Count > 0 ? errors : null);

            //    this.ModelState.Keys.Select(key => new { field = key, error = this.ModelState[key].Errors.FirstOrDefault() })
            // Must decide how errors are returned?
            // 200 status code or 400?
            // Kendo is configurable and can detects errors from response;
            //Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return this.Json(response);
        }

        [Route("destroy")]
        [HttpPost]
        public JsonResult Destroy(PersonModel model)
        {
            var existing = models.FirstOrDefault(m => m.Id == model.Id);

            if(existing != null)
            {
                models.Remove(existing);
            }

            var response = new ApiResponse(model);

            return this.Json(response);
        }

        [Route("update")]
        [HttpPost]
        public JsonResult Update(PersonModel model)
        {
            var existing = models.FirstOrDefault(m => m.Id == model.Id);

            if (existing == null)
            {
                return null;
            }

            if (this.ModelState.IsValid)
            {
                models.Remove(existing);
                models.Add(model);
            }

            //var errors = this.ModelState.ToDictionary(k => k.Key, k => k.Value.Errors.Select(e => e.ErrorMessage));
            var errors = this.ModelState
                .Where(kvp => kvp.Value.Errors.Any())
                .Select(kvp => new {
                    field = kvp.Key,
                    error = kvp.Value.Errors.Select(e => e.ErrorMessage).FirstOrDefault()
                }).ToList();

            var response = new ApiResponse(model, errors.Count > 0 ? errors : null);

            return this.Json(response);
        }
    }
}