using CommonLayer;
using CommonLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Services
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions<FundooContext> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasIndex(x => x.Email).IsUnique();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<NotesModel> Notes { get; set; }
        public DbSet<LabelModel> Labels { get; set; }
    }
}
