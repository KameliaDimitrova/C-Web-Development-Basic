using PandaWebApp.Data;

namespace PandaWebApp.Controllers
{
    using SIS.MvcFramework;

    public abstract class BaseController : Controller
    {
        protected ApplicationDbContext Db { get; }

        protected BaseController()
        {
            this.Db = new ApplicationDbContext();
        }
    }
}
