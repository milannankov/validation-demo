using System.Web.Mvc;

namespace kendo_asp_mvc.Controllers
{
    public partial class PersonController : Controller
    {
        public class ApiResponse
        {
            public bool Success
            {
                get;
                private set;
            }

            public object Errors
            {
                get;
                private set;
            }

            public object Result
            {
                get;
                private set;
            }
            public ApiResponse(object result, object errors = null)
            {
                this.Success = errors == null;

                this.Result = result;
                this.Errors = errors;
            }
        }
    }
}