using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWeb.ViewModels.Receipts
{
    public class ReceiptDetailsViewModel
    {
        public IList<OrderInReceiptViewModel> Orders { get; set; }

        public decimal Total { get; set; }

        public string IssuedOn { get; set; }

        public string Cashier { get; set; }

        public Guid Receipt { get; set; }
    }
}
