﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Tasks
{
    public class TaskDetails
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Level { get; set; }

        public string Participants { get; set; }

        public string AffectedSectors { get; set; }

        public string Description { get; set; }
        public string DueDate { get; set; }
    }
}
