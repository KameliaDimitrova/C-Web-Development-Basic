using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PandaWebApp.Models
{
   public class ChannelTag
    {
        [Key]
        public int ChannelId { get; set; }

        public Channel Channel { get; set; }

        [Key]
        public int TagId { get; set; }

        public Tag Tag { get; set; }
    }
}
