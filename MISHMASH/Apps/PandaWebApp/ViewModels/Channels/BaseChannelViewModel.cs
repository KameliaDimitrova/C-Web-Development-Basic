using System;
using System.Collections.Generic;
using System.Text;
using PandaWebApp.Models.Enums;

namespace PandaWebApp.ViewModels.Channels
{
    public class BaseChannelViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ChanelType Type { get; set; }

        public int FollowersCount { get; set; }
    }
}
