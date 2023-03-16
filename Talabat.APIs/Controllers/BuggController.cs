using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Repository.Data;

namespace Talabat.APIs.Controllers
{
    public class BuggController : GenericController
    {
        public StoreContext Context { get; }

        public BuggController(StoreContext context)
        {
            Context = context;
        }

        [HttpGet("servererorr")]
        public ActionResult GetServerError()
        {
            var product = Context.products.Find(100);
            var productReturn = product.ToString();

            return Ok(productReturn);
        }
    }
}
