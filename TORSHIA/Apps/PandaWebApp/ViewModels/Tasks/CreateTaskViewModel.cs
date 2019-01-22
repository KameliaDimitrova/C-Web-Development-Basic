using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace PandaWebApp.ViewModels.Tasks
{
    public class CreateTaskViewModel
    {
      
        public string Title { get; set; }

        public string DueDate { get; set; }

        public string Description { get; set; }

        public string Participants { get; set; }
        
        public string Sector1 { get; set; }
        public string Sector2 { get; set; }
        public string Sector3 { get; set; }
        public string Sector4 { get; set; }
        public string Sector5 { get; set; }

    }
}
