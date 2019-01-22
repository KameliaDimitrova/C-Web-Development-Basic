using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using PandaWebApp.Models.Enums;

namespace PandaWebApp.Models
{
 
   public class TaskSector
    {
   
        public int Id { get; set; }

        public int  TaskId { get; set; }
        public Task Task { get; set; }


        public Sector Sector { get; set; }
    }
}
