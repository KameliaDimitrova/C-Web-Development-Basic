using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PandaWebApp.Models.Enums;
using PandaWebApp.ViewModels.Tasks;

namespace PandaWebApp.ViewModels.Home
{
   public class LoggedInIndexViewModel
    {
        public LoggedInIndexViewModel()
        {
           this.Tasks=new List<IndexListOfTasks>();
        }
        public string Role { get; set; }
        public IList<IndexListOfTasks> Tasks { get; set; }
    }
}
