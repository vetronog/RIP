using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Blog.Models
{
    public class DB : DbContext
    {
        public DB() : base("DB")
        {
            Database.SetInitializer<DB>(new CreateDatabaseIfNotExists<DB>());
        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}