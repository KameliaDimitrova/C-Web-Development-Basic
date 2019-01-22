using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PandaWebApp.Models
{
   public class UserChanel
    {
       
        [Key]
        public int UserId { get; set; }

        public User User { get; set; }

        [Key]
        public int ChannelId { get; set; }

        public Channel Channel { get; set; }
    }
}
