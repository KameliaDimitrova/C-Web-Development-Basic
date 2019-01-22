using System;
using System.Collections.Generic;
using System.Text;
using ExamWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ExamWeb.Data
{
    public class ApplicationDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        public ApplicationDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                  optionsBuilder.UseSqlServer(
                    @"Server=DESKTOP-2LIJCRE\SQLEXPRESS;Database=Musaka;Integrated Security=True;");
            }
        }
    }
}
