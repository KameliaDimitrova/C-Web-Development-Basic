using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWeb.ViewModels.Home
{
    public class LoggedInIndexViewModel
    {
        public LoggedInIndexViewModel()
        {
            this.Orders=new List<OrdersIndexViewModel>();
        }
        public string UserRole { get; set; }
        public decimal Total { get; set; }
        public List<OrdersIndexViewModel> Orders { get; set; }
    }
}
