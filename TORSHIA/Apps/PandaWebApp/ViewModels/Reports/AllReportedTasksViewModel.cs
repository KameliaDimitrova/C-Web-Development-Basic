using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Reports
{
    public class AllReportedTasksViewModel
    {
        public AllReportedTasksViewModel()
        {
            this.AllReportedTasks=new HashSet<ReportedTaskViewModel>();
        }
        public HashSet<ReportedTaskViewModel> AllReportedTasks { get; set; }
    }
}
