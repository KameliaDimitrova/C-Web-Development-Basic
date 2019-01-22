using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PandaWebApp.Models.Enums;

namespace PandaWebApp.Models
{
   public class Task
    {
        public Task()
        {
            this.IsReported = false;
            this.AffectedSectors=new List<TaskSector>();
        }
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DueDate { get; set; }

        public bool IsReported { get; set; }

        public string Description { get; set; }

        public string Participants { get; set; }
   
        public ICollection<TaskSector> AffectedSectors { get; set; }
    }
}
