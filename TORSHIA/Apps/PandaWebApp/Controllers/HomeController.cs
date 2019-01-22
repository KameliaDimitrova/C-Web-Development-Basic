using System;
using System.Linq;
using PandaWebApp.Models;
using PandaWebApp.ViewModels.Home;
using PandaWebApp.ViewModels.Tasks;

namespace PandaWebApp.Controllers
{
    using SIS.HTTP.Responses;
    using SIS.MvcFramework;

    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (user == null)
            {
                return this.View();
            }
            else
            {
                var viewModel = new LoggedInIndexViewModel
                {
                    Role = user.Role.ToString()
                };
               
                viewModel.Tasks = this.Db.Tasks.Where(x=>x.IsReported==false).Select(x => new IndexListOfTasks
                {
                    Id = x.Id,
                    Title = x.Title,
                    Level = x.AffectedSectors.Count
                   
                }).ToList();

                
                return this.View("/Home/LoggedInIndex", viewModel);
            }
        }
    
    }
}
