using System;
using System.Collections.Generic;
using System.Text;

namespace ExamWeb.Models
{
    public class Receipt
    {
        public Guid Id { get; set; }

        public DateTime IssuedOn { get; set; }

        public ICollection<Order> Orders { get; set; }

        public int UserId { get; set; }
        public User Cashier { get; set; }

    }
}
