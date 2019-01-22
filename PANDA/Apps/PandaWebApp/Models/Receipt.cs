using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.Models
{
    public class Receipt
    {
        public int Id { get; set; }

        public decimal Fee { get; set; }

        public DateTime IssuedOn { get; set; }

        public User Recipient { get; set; }
        public int UserId { get; set; }

        public Package Package { get; set; }
        public int PackageId { get; set; }
    }

}
