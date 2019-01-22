using System.Linq;
using PandaWebApp.Models.Enums;
using PandaWebApp.ViewModels.Packages;

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
                    var pendingPackages = this.Db.Packages
                        .Where(x => x.UserId == user.Id && x.Status == Status.Pending)
                        .Select(x => new PendingAndDeliveredViewModel
                        {
                            Description = x.Description,
                            Id = x.Id

                        }).ToList();

                    var shippedPackages = this.Db.Packages
                        .Where(x => x.UserId == user.Id && x.Status == Status.Shipped)
                        .Select(x => new ShippedViewModel
                        {
                            Description = x.Description,
                            Id = x.Id

                        }).ToList();

                    var deliveredPackages = this.Db.Packages
                        .Where(x => x.UserId == user.Id && x.Status == Status.Delivered)
                        .Select(x => new PendingAndDeliveredViewModel
                        {
                            Description = x.Description,
                            Id = x.Id

                        }).ToList();

                    var model = new AllPackagesViewModel();
                    model.DeliveredPackages = deliveredPackages;
                    model.PendingPackages = pendingPackages;
                    model.ShippedPackages = shippedPackages;

               
                return this.View("/Home/LoggedInIndex",model);
                }
            }
        }
    
}
