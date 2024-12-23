using System.Collections.Generic;
using BagControllerAPI.Model;
using Microsoft.EntityFrameworkCore;

namespace BagControllerAPI.Data
{
    public class BagDbContext : DbContext
    {
        public BagDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Users>? Users { get; set; }
        public DbSet<UserProfiles>? UserProfiles { get; set; }
        public DbSet<Bags>? Bags { get; set; }
    }

}
