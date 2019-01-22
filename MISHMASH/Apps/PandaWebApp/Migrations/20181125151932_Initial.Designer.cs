﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PandaWebApp.Data;

namespace PandaWebApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20181125151932_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PandaWebApp.Models.Channel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.ToTable("Channels");
                });

            modelBuilder.Entity("PandaWebApp.Models.ChannelTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChannelId");

                    b.Property<int>("TagId");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.HasIndex("TagId");

                    b.ToTable("ChannelTag");
                });

            modelBuilder.Entity("PandaWebApp.Models.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ChannelId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("PandaWebApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<int>("Role");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PandaWebApp.Models.UserChanel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ChanelId");

                    b.Property<int?>("ChannelId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ChannelId");

                    b.HasIndex("UserId");

                    b.ToTable("UserChanel");
                });

            modelBuilder.Entity("PandaWebApp.Models.ChannelTag", b =>
                {
                    b.HasOne("PandaWebApp.Models.Channel", "Chanel")
                        .WithMany()
                        .HasForeignKey("ChannelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PandaWebApp.Models.Tag", "Tag")
                        .WithMany("Channels")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PandaWebApp.Models.Tag", b =>
                {
                    b.HasOne("PandaWebApp.Models.Channel")
                        .WithMany("Tags")
                        .HasForeignKey("ChannelId");
                });

            modelBuilder.Entity("PandaWebApp.Models.UserChanel", b =>
                {
                    b.HasOne("PandaWebApp.Models.Channel", "Channel")
                        .WithMany("Followers")
                        .HasForeignKey("ChannelId");

                    b.HasOne("PandaWebApp.Models.User", "User")
                        .WithMany("Chanels")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
