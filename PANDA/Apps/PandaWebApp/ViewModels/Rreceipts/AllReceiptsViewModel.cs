using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Rreceipts
{
   public class AllReceiptsViewModel
    {
        public AllReceiptsViewModel()
        {
            this.Receipts=new List<ReceiptViewModel>();
        }
        public IList<ReceiptViewModel> Receipts { get; set; }
    }
}
