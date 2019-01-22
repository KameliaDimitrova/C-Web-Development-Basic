using System;
using System.Collections.Generic;
using System.Text;
using ExamWeb.Models.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ExamWeb.Models
{
    public class Order
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Quantity { get; set; }

        public int UserId { get; set; }
        public User Cashier { get; set; }

        
     }
}
