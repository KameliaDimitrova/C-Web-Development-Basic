using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Rreceipts
{
  public  class ReceiptDetailsViewModel
    {
        public int Id { get; set; }

        public string IssuedOn { get; set; }

        public string DeliveryAddress { get; set; }

        public double PackageWeight { get; set; }

        public string PackageDescription { get; set; }

        public string Recipient { get; set; }

        public decimal Total { get; set; }
    }
}
