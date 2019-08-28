using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ERPBase
{
    public class ERPBaseContext : DbContext
    {
        public ERPBaseContext()
        : base("name=ERPBase")
        {
            this.Configuration.LazyLoadingEnabled = true;
        }

        public DbSet<SYS_FILE> SYS_FILE { get; set; }

        public DbSet<SYS_FOLDER> SYS_FOLDER { get; set; }

        public DbSet<SP_WORK_FOLLOW> SP_WORK_FOLLOW { get; set; }

        public DbSet<SP_SMS> SP_SMS { get; set; }

    }
}