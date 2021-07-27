using CommonLayer;
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
        public DbSet<User> users { get; set; }
    }
}
