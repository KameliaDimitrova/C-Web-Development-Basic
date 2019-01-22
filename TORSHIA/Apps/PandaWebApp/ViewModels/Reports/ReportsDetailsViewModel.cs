using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Reports
{
   public class ReportsDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Level { get; set; }

        public string Status { get; set; }

        public string ReportedOn { get; set; }

        public string ReporterName { get; set; }

        public string Participants { get; set; }

        public string AffectedSectors { get; set; }

        public string Description { get; set; }

        public string DueDate { get; set; }

    }
}
