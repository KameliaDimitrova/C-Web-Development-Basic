using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExamWeb.Models;
using ExamWeb.Models.Enums;
using ExamWeb.ViewModels.Receipts;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace ExamWeb.Controllers
{
    public class ReceiptsController : BaseController
    {
        [Authorize("Admin")]
        [HttpGet("/Receipts/All")]
        public IHttpResponse All()
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (!User.IsLoggedIn || user.Role != Role.Admin)
            {
                return this.View("/Users/Login");
            }
            var receipts = this.Db.Receipts
                .Select(x => new OneReceiptsViewModel
                {
                    Cashier = x.Cashier.Username,
                    Id = x.Id.ToString(),
                    IssuedOn = x.IssuedOn.ToString("dd/MM/yyyy"),
                    Total = x.Orders.Select(y => y.Product.Price * y.Quantity).Sum(),

                }).ToList();
            var model = new AllReceiptsViewModel
            {
                Receipts = receipts
            };
            return this.View("/Receipts/All", model);
        }

        [Authorize]
        [HttpGet("/Receipts/Details")]
        public IHttpResponse Details(string id)
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (!User.IsLoggedIn)
            {
                return this.View("/Users/Login");
            }
            var guidId = new Guid(id);
            var receipt = this.Db.Receipts
                .FirstOrDefault(x => x.Id == guidId);
            if (receipt == null)
            {
                return this.BadRequestError("Id is not correct!");
            }
            var orders = this.Db.Receipts
                .Where(x => x.Id == guidId)
                .Select(x => x.Orders.Select(y=>new OrderInReceiptViewModel
                {
                    Name = y.Product.Name,
                    Price = y.Product.Price,
                    Quantity = y.Quantity,
                   
                }).ToList())
                .FirstOrDefault();
            if (orders == null)
            {
                return this.BadRequestError("Id is not correct!");
            }
            var model = new ReceiptDetailsViewModel();

            model.Orders = orders;

            model.Cashier = receipt.Cashier.Username;
              
            if (User.Role == "User" && model.Cashier != User.Username)
            {
                return this.BadRequestError("You can not check other users receipt`s details!");
            }
            model.IssuedOn = receipt.IssuedOn.ToString("dd/MM/yyyy");
            model.Receipt = receipt.Id;
            model.Total = orders.Select(y => y.Price * y.Quantity).Sum();
            
            return this.View("/Receipts/Details", model);
        }


        [HttpGet("/Receipts/Cashout")]
        public IHttpResponse Chasout()
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (!User.IsLoggedIn)
            {
                return this.View("/Users/Login");
            }

            var orders = this.Db.Orders
                .Where(x => x.Status == Status.Active && User.Username == x.Cashier.Username)
                .ToList();

            if (orders.Count < 1)
            {
                return this.BadRequestError("There is no orders, please add product first!");
            }
            foreach (var order in orders)
            {
                order.Status = Status.Completed;
            }

            var receipt = new Receipt
            {
                Cashier = user,
                IssuedOn = DateTime.Now,
                Orders = orders,
                UserId = user.Id,

            };

            this.Db.Receipts.Add(receipt);
            this.Db.SaveChanges();
            return Redirect("/Receipts/All");
        }
    }
}
