using System.Linq;
using ExamWeb.Models;
using ExamWeb.Models.Enums;
using ExamWeb.ViewModels.Home;

namespace ExamWeb.Controllers
{
    using SIS.HTTP.Responses;
    using SIS.MvcFramework;

    public class HomeController : BaseController
    {
        public IHttpResponse Index()
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (user != null)
            {
                var viewModel = new LoggedInIndexViewModel();
                viewModel.UserRole = user.Role.ToString();
                var activeOrders = this.Db.Orders.Where(x => x.Status == Status.Active);
                var model = activeOrders
                    .Select(x => new OrdersIndexViewModel
                    {
                        Name = x.Product.Name,
                        Price = x.Product.Price,
                        Quantity = x.Quantity,
                    }).ToList();

                viewModel.Orders = model;
                viewModel.Total = activeOrders
                    .Select(y => y.Product.Price * y.Quantity).Sum();
                return this.View("/Home/LoggedInIndex", viewModel);
            }
            return this.View();
        }

        [Authorize]
        [HttpPost("/Home/Index")]
        public IHttpResponse Index(ProductMakeOrderViewModel model)
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);

            var product = this.Db.Products.FirstOrDefault(x => x.Barcode == model.Barcode);
            if (product == null)
            {
                return this.BadRequestError("There is no product with that Barcode! Try again!");
            }

            var order = new Order
            {
                Cashier = user,
                Product = product,
                ProductId = product.Id,
                Quantity = model.Quantity,
                Status = Status.Active,
                UserId = user.Id
            };
            this.Db.Orders.Add(order);
            this.Db.SaveChanges();
            return this.Redirect("/Home/Index");
        }
    }
}

