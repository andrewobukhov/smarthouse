using System.Web.Http;
using SmartHouseWeb.DAL;

namespace SmartHouseWeb.Controllers
{
    public class CommonApiController : ApiController
    {
        protected ShContext Context = new ShContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}