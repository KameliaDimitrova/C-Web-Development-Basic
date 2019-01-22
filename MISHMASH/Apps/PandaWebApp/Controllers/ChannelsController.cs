using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using PandaWebApp.Models;
using PandaWebApp.Models.Enums;
using PandaWebApp.ViewModels.Channels;
using PandaWebApp.ViewModels.Users;
using SIS.HTTP.Cookies;
using SIS.HTTP.Responses;
using SIS.MvcFramework;
using SIS.MvcFramework.Services;

namespace PandaWebApp.Controllers
{
    public class ChannelsController : BaseController
    {

        [HttpGet("/channels/details")]
        public IHttpResponse Details(int id)
        {
            if (this.User.IsLoggedIn)
            {
                var channelViewModel = this.Db.Channels.Where(x => x.Id == id)
                    .Select(x => new ChannelViewModel
                    {
                        Type = x.Type,
                        Name = x.Name,
                        Tags = x.Tags.Select(t => t.Tag.Name),
                        Description = x.Description,
                        FollowersCount = x.Followers.Count()

                    }).FirstOrDefault();
                return this.View("/Channels/Details", channelViewModel);
            }
            else
            {
                return this.Redirect("/Home/Login");
            }

        }

        [HttpGet("/channels/followed")]
        public IHttpResponse Followed()
        {
            if (!this.User.IsLoggedIn)
            {
                return this.Redirect("/Users/Login");
            }
            var followedChannels = this.Db.Channels
                .Where(x => x.Followers
                    .Any(f => f.User.Username == this.User.Username))
                .Select(x => new BaseChannelViewModel()
                {
                    Id = x.Id,
                    Type = x.Type,
                    Name = x.Name,
                    FollowersCount = x.Followers.Count()

                }).ToList();
            var viewModel = new FollowedChannelsViewModel
            {
                FollowedChannels = followedChannels
            };
            return this.View("channels/followed", viewModel);

        }

        [HttpGet("Channels/Follow")]
        public IHttpResponse Follow(int id)
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (!this.User.IsLoggedIn)
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.Db.UserChanel.Any(x => x.UserId == user.Id && x.ChannelId == id))
            {
                this.Db.UserChanel.Add(new UserChanel
                {
                    ChannelId = id,
                    UserId = user.Id
                });
                this.Db.SaveChanges();
            }
            return this.Redirect("/Channels/Followed");
        }

        [HttpGet("Channels/Unfollow")]
        public IHttpResponse Unfollow(int id)
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (!this.User.IsLoggedIn)
            {
                return this.Redirect("/Users/Login");
            }
            var userInChannel = this.Db.UserChanel.FirstOrDefault(
                x => x.UserId == user.Id && x.ChannelId == id);
            if (userInChannel != null)
            {
                this.Db.UserChanel.Remove(userInChannel);
                this.Db.SaveChanges();
            }
            return this.Redirect("/Channels/Followed");
        }

        [HttpGet("channels/create")]
        public IHttpResponse Create()
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (!User.IsLoggedIn || user.Role != Role.Admin)
            {
                return this.View("/Users/Login");
            }
            return this.View("channels/create");
        }

        [HttpPost("channels/create")]
        public IHttpResponse Create(CreateChannelsInputModel model)
        {
            var user = this.Db.Users.FirstOrDefault(x => x.Username == this.User.Username);
            if (!User.IsLoggedIn || user.Role != Role.Admin)
            {
                return this.View("/Users/Login");
            }
            if (!Enum.TryParse(model.Type, true, out ChanelType type))
            {
                return this.BadRequestError("Invelid channel type");
            }
            var tags = model.Tags
                .Split(',', ';',StringSplitOptions.RemoveEmptyEntries);
            var channel = new Channel
            {
                Name = model.Name,
                Description = model.Description,
                Type = type,
            };
            foreach (var tag in tags)
            {
                var dbTag = this.Db.Tags.FirstOrDefault(x => x.Name == tag.Trim());
                if (dbTag == null)
                {
                    dbTag = new Tag
                    {
                        Name = tag
                    };
                    this.Db.Tags.Add(dbTag);
                    Db.SaveChanges();
                }
                channel.Tags.Add(new ChannelTag
                {
                    TagId = dbTag.Id
                });
            }

            this.Db.Channels.Add(channel);
            this.Db.SaveChanges(); 
            return this.Redirect("/Channels/Details?id="+channel.Id);
        }
    }
}

