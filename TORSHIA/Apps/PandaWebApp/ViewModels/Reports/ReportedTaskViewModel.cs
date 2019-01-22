using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Reports
{
    public class ReportedTaskViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int Level { get; set; }

        public string Status { get; set; }
    }
}
