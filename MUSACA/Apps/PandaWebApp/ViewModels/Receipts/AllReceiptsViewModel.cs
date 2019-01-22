using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExamWeb.ViewModels.Receipts
{
   public class AllReceiptsViewModel
    {
        public IList<OneReceiptsViewModel> Receipts { get; set; }
    }
}
