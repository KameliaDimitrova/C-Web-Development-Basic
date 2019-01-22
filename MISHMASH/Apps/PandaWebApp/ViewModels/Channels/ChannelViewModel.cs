using System;
using System.Collections.Generic;
using System.Text;
using PandaWebApp.Models;
using PandaWebApp.Models.Enums;

namespace PandaWebApp.ViewModels.Channels
{
   public class ChannelViewModel
    {
        public string Name { get; set; }

        public ChanelType Type { get; set; }

        public string Description { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public string TagsAsString => string.Join(", ", this.Tags);

        public int FollowersCount { get; set; }
       
    }
}
