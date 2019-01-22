using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using PandaWebApp.Models;
using PandaWebApp.Models.Enums;
using PandaWebApp.ViewModels.Packages;
using PandaWebApp.ViewModels.Rreceipts;
using PandaWebApp.ViewModels.Users;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace PandaWebApp.Controllers
{
    public class PackageController : BaseController
    {
        [HttpGet("Packages/Create")]
        public IHttpResponse Create()
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (!User.IsLoggedIn || user.Role != Role.Admin)
            {
                return this.View("/Users/Login");
            }
            var recipients = this.Db.Users
                .Select(x => new RecipientViewModel
                {
                    Recipient = x.Username
                }).ToList();

            var model = new AllRecipients
            {
                Recipients = recipients
            };

            return this.View("Packages/Create", model);
        }

        [HttpPost("Packages/Create")]
        public IHttpResponse Create(CreatePackageModelView model)
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (!User.IsLoggedIn || user.Role != Role.Admin)
            {
                return this.View("/Users/Login");
            }

            var package = new Package
            {
                Description = model.Description,
                Weight = model.Weight,
                ShippingAddress = model.ShippingAddress,
                Recipient = this.Db.Users.FirstOrDefault(x => x.Username == model.Recipient),
                Status = Status.Pending

            };
            if (package.Recipient == null)
            {
                return this.BadRequestError("Please, choose recipient!");
            }
            this.Db.Packages.Add(package);
            this.Db.SaveChanges();
            return this.Redirect("/Packages/Details?id=" + package.Id);
        }

        [Authorize]
        [HttpGet("Packages/Details")]
        public IHttpResponse Details(int id)
        {
            if (this.User.IsLoggedIn)
            {
                var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);


                var modelView = this.Db.Packages
                    .Where(x => id == x.Id)
                    .Select(x => new PackageDetailsViewModel
                    {
                        Id = x.Id,
                        Address = x.ShippingAddress,
                        Description = x.Description,
                        EstimatedDeliveryDate = x.EstimatedDeliveryDate.HasValue ? x.EstimatedDeliveryDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                        Recipient = x.Recipient.Username,
                        Status = x.Status.ToString(),
                        Weight = x.Weight
                    }).FirstOrDefault();

                if (modelView == null)
                {
                    return this.BadRequestError("Invalid package id.");
                }


                return this.View("/Packages/Details", modelView);
            }
            else
            {
                return this.Redirect("/Users/Login");
            }
        }

        [Authorize("Admin")]
        [HttpGet("Packages/Pending")]
        public IHttpResponse Pending()
        {
            var pendingPackages = this.Db.Packages
                .Where(x => (x.Status == Status.Pending))
                .Select(x => new PendingAndDeliveredViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Recipient = x.Recipient.Username,
                    ShippingAddress = x.ShippingAddress,
                    Weight = x.Weight
                }).ToList();
            var allPendingPackages = new AllPendingAndDeliveredViewModel
            {

                PendingAndDelivered = pendingPackages
            };
            return this.View("/Packages/Pending", allPendingPackages);
        }


        [Authorize("Admin")]
        [HttpGet("/Packages/Ship")]
        public IHttpResponse Ship(int id)
        {
            var package = this.Db.Packages.FirstOrDefault(x => x.Id == id);
            if (package == null)
            {
                return BadRequestError("This Id doesn`t exist!");
            }
            if (package.Status != Status.Pending)
            {
                return BadRequestError("Please, contact your admin to create package first!");
            }
            package.Status = Status.Shipped;
            package.EstimatedDeliveryDate = DateTime.UtcNow.AddDays(new Random().Next(20, 40));
            this.Db.Packages.Update(package);
            this.Db.SaveChanges();
            return this.Redirect("/Packages/Shipped");
        }

        [Authorize("Admin")]
        [HttpGet("/Packages/Deliver")]
        public IHttpResponse Deliver(int id)
        {
            var package = this.Db.Packages.FirstOrDefault(x => x.Id == id);
            if (package == null)
            {
                return BadRequestError("This Id doesn`t exist!");
            }
            if (package.Status != Status.Shipped)
            {
                return BadRequestError("Please, contact your admin to ship package first!");
            }
            package.Status = Status.Delivered;
            this.Db.Packages.Update(package);
            this.Db.SaveChanges();
            return this.Redirect("/Packages/Delivered");
        }

        [Authorize("Admin")]
        [HttpGet("/Packages/Shipped")]
        public IHttpResponse Shipped()
        {
            var shipedPackages = this.Db.Packages
                .Where(x => (x.Status == Status.Shipped))
                .Select(x => new ShippedViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Recipient = x.Recipient.Username,
                    EstimatedDeliveryDate = x.EstimatedDeliveryDate.HasValue ? x.EstimatedDeliveryDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                    Weight = x.Weight
                }).ToList();
            var allshippedPackages = new AllShipedViewModel
            {

                AllShipedPackages = shipedPackages
            };
            return this.View("/Packages/Shipped", allshippedPackages);
        }

        [Authorize("Admin")]
        [HttpGet("/Packages/Delivered")]
        public IHttpResponse Delivered()
        {
            var deliveredPackages = this.Db.Packages
                .Where(x => (x.Status == Status.Delivered))
                .Select(x => new PendingAndDeliveredViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Recipient = x.Recipient.Username,
                    ShippingAddress = x.ShippingAddress,
                    Weight = x.Weight
                }).ToList();
            var allDeliveredPackages = new AllPendingAndDeliveredViewModel
            {

                PendingAndDelivered = deliveredPackages
            };
            return this.View("/Packages/Delivered", allDeliveredPackages);
        }

        [Authorize]
        [HttpGet("/Packages/Acquire")]
        public IHttpResponse Acquire(int id)
        {
            var package = this.Db.Packages.FirstOrDefault(x => x.Id == id);
            if (package == null)
            {
                return this.BadRequestError("Invalid package Id!");
            }

            if (package.Status != Status.Delivered)
            {
                return this.BadRequestError("The package is not delivered yet, you can`t aquired it!");
            }

            package.Status = Status.Acquired;
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            var receipt = new Receipt
            {
                IssuedOn = DateTime.Now,
                PackageId = package.Id,
                Package = package,
                Recipient = user,
                UserId = user.Id,
                Fee = (decimal) package.Weight * (decimal) 2.67
            };
            this.Db.Receipts.Add(receipt);
            this.Db.SaveChanges();
            return this.Redirect("/Receipts/Index");
        }
    }
}
