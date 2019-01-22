using System;
using System.Collections.Generic;
using System.Text;

namespace PandaWebApp.ViewModels.Channels
{
   public class FollowedChannelsViewModel
    {
        public IEnumerable<BaseChannelViewModel> FollowedChannels { get; set; }
    }
}
