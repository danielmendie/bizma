
using BizMap.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Skybrinns.Server.Data
{
    public class BizDbContext : DbContext
    {
        public BizDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<BusinessUser> BizUsers { get; set; }
        public virtual DbSet<BusinessStore> BizStores { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //StaffToWorkspace binding

        }
    }
}
