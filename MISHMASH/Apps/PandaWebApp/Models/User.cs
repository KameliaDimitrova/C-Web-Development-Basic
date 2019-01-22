using System;
using System.Collections.Generic;
using System.Text;
using PandaWebApp.Models.Enums;

namespace PandaWebApp.Models
{
   public  class User
    {
        public User()
        {
            this.Channels=new HashSet<UserChanel>();
        }
        public int Id { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public virtual ICollection<UserChanel> Channels { get; set; }

        public string Email { get; set; }

        public Role Role { get; set; }


    }
}
