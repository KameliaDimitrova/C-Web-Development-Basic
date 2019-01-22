using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PandaWebApp.Models;

namespace PandaWebApp.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Channel> Channels { get; set; }

        public DbSet<Tag> Tags { get; set; }

        public DbSet<UserChanel> UserChanel { get; set; }

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
                    @"Server=DESKTOP-2LIJCRE\SQLEXPRESS;Database=MishMash;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserChanel>().HasKey(us => new {us.UserId, us.ChannelId});

            builder.Entity<UserChanel>()
                .HasOne(u => u.User)
                .WithMany(uc => uc.Channels)
                .HasForeignKey(u => u.UserId);

            builder.Entity<UserChanel>()
                .HasOne(c => c.Channel)
                .WithMany(u => u.Followers)
                .HasForeignKey(c => c.ChannelId);

            builder.Entity<ChannelTag>().HasKey(ct => new { ct.ChannelId, ct.TagId });

            builder.Entity<ChannelTag>()
                .HasOne(c => c.Channel)
                .WithMany(ct => ct.Tags)
                .HasForeignKey(c => c.ChannelId);

            builder.Entity<ChannelTag>()
                .HasOne(t => t.Tag)
                .WithMany(ct => ct.Channels)
                .HasForeignKey(t => t.TagId);
        }
    }
}
