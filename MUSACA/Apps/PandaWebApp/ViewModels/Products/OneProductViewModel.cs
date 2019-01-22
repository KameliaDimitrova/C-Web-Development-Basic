using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWeb.ViewModels.Products
{
   public  class OneProductViewModel
    {
        public int Id { get; set; }

        public decimal Price { get; set; }

        public long Barcode { get; set; }

        public string Picture { get; set; }
        public string Name { get; set; }
    }
}
