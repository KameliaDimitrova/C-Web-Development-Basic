using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PandaWebApp.ViewModels.Rreceipts;
using SIS.HTTP.Responses;
using SIS.MvcFramework;

namespace PandaWebApp.Controllers
{
   public class ReceiptsController:BaseController
    {
        [Authorize]
        [HttpGet("/Receipts/Index")]
        public IHttpResponse Receipts()
        {
            var receipts=new List<ReceiptViewModel>();
            if (User.Role == "User")
            {
                receipts = this.Db.Receipts
                    .Where(x => x.Recipient.Username == User.Username)
                    .Select(x => new ReceiptViewModel
                    {
                        Fee = x.Fee,
                        Id = x.Id,
                        IssuedOn = x.IssuedOn.ToString("dd/MM/yyyy"),
                        Recipient = x.Recipient.Username
                    }).ToList();
            }
            else
            {
                receipts = this.Db.Receipts
                    .Select(x => new ReceiptViewModel
                    {
                        Fee = x.Fee,
                        Id = x.Id,
                        IssuedOn = x.IssuedOn.ToString("dd/MM/yyyy"),
                        Recipient = x.Recipient.Username
                    }).ToList();
            }
           
            var allReceiptsViewModel = new AllReceiptsViewModel
            {
                Receipts = receipts
            };
            return this.View("/Receipts/Index",allReceiptsViewModel);
        }

        [Authorize]
        [HttpGet("/Receipts/Details")]
        public IHttpResponse Details(int id)
        {
            var model = this.Db.Receipts
                .Where(x => x.Id == id)
                .Select(x => new ReceiptDetailsViewModel
                {
                    DeliveryAddress = x.Package.ShippingAddress,
                    Id = x.Id,
                    IssuedOn = x.IssuedOn.ToString("dd/MM/yyyy"),
                    PackageDescription = x.Package.Description,
                    PackageWeight = x.Package.Weight,
                    Recipient = x.Recipient.Username,
                    Total = x.Fee
                }).FirstOrDefault();
            return this.View("/Receipts/Details", model);
        }

    }
}
