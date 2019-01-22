using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWeb.ViewModels.Receipts
{
    public class OrderInReceiptViewModel
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
