using PandaWebApp.Data;

namespace PandaWebApp.Controllers
{
    using SIS.MvcFramework;

    public class BaseController : Controller
    {
        protected ApplicationDbContext Db { get; }
        protected BaseController()
        {
            this.Db = new ApplicationDbContext();
        }
    }
}
