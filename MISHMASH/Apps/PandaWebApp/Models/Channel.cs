using System;
using System.Collections.Generic;
using PandaWebApp.Models.Enums;

namespace PandaWebApp.Models
{

    public class Channel
    {
        public Channel()
        {
            this.Followers=new HashSet<UserChanel>();
            this.Tags=new HashSet<ChannelTag>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public ChanelType Type { get; set; }

        public virtual ICollection<ChannelTag> Tags { get; set; }

        public virtual ICollection<UserChanel> Followers { get; set; }
    }
}