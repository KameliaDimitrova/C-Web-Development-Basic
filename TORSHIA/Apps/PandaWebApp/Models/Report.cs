using System;
using System.Collections.Generic;
using System.Text;
using PandaWebApp.Models.Enums;

namespace PandaWebApp.Models
{
   public class Report
    {
        public int Id { get; set; }

        public Status Status { get; set; }

        public DateTime ReportedOn { get; set; }

        public int TaskId { get; set; }
        public Task Task { get; set; }

        public int UserId { get; set; }
        public User Reporter { get; set; }
    }
}
