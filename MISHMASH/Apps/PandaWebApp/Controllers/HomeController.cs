﻿using System.Linq;
using PandaWebApp.ViewModels.Channels;
using PandaWebApp.ViewModels.Home;

namespace PandaWebApp.Controllers
{
    using SIS.HTTP.Responses;
    using SIS.MvcFramework;

    public class HomeController : BaseController
    {

        //It could not be writed because if we are at HOMEController and Index it is automatically Home/Index
        
        public IHttpResponse Index()
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (user != null)
            {
               
                var viewModel = new LoggedInIndexViewModel();
                viewModel.UserRole = user.Role.ToString();
                viewModel.YourChannels = this.Db.Channels.Where(
                        x => x.Followers.Any(f => f.User.Username == this.User.Username))
                    .Select(x => new BaseChannelViewModel
                    {
                        Id = x.Id,
                        Type = x.Type,
                        Name = x.Name,
                        FollowersCount = x.Followers.Count()
                    }).ToList();

                var followedChannelsTags = this.Db.Channels.Where(
                        x => x.Followers.Any(f => f.User.Username == this.User.Username))
                    .SelectMany(x => x.Tags.Select(t => t.TagId)).ToList();

                viewModel.SuggestedChannels = this.Db.Channels.Where(
                        x => !x.Followers.Any(f => f.User.Username == this.User.Username) &&
                             x.Tags.Any(t => followedChannelsTags.Contains(t.TagId)))
                    .Select(x => new BaseChannelViewModel
                    {
                        Id = x.Id,
                        Type = x.Type,
                        Name = x.Name,
                        FollowersCount = x.Followers.Count(),
                    }).ToList();

                var ids = viewModel.YourChannels.Select(x => x.Id).ToList();
                ids = ids.Concat(viewModel.SuggestedChannels.Select(x => x.Id)).ToList();
                ids = ids.Distinct().ToList();

                viewModel.SeeOtherChannels = this.Db.Channels.Where(x => !ids.Contains(x.Id))
                    .Select(x => new BaseChannelViewModel
                    {
                        Id = x.Id,
                        Type = x.Type,
                        Name = x.Name,
                        FollowersCount = x.Followers.Count(),
                    }).ToList();

                return this.View("/Home/LoggedInIndex",viewModel);
            }
            
            return this.View();
        }
    }
}
