using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PandaWebApp.Models;

namespace PandaWebApp.Data
{
   public class ApplicationDbContext:DbContext
    {
        
            public DbSet<User> Users { get; set; }

            public DbSet<Package> Packages { get; set; }

            public DbSet<Receipt> Receipts { get; set; }

            public ApplicationDbContext()
            {

            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                if (!optionsBuilder.IsConfigured)
                {
                    optionsBuilder.UseSqlServer(
                        @"Server=DESKTOP-2LIJCRE\SQLEXPRESS;Database=Panda;Integrated Security=True;");
                }
            }

    }
    
}
