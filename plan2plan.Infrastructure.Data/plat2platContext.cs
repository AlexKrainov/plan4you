using plan2plan.Domain.Core;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Data
{
    public partial class plat2platContext : DbContext
    {
        public plat2platContext()
            //: base("Data Source=u479185.mssql.masterhost.ru;initial catalog=u479185_bujodb;user id=u479185;password=sagemicerea7;multipleactiveresultsets=True;application name=EntityFramework;")
            : base("Server=DELL\\SQLEXPRESS;Database=BujoDB;Trusted_Connection=True;")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Feedback>().ToTable("Feedback");
            modelBuilder.Entity<File>().ToTable("File");
            modelBuilder.Entity<Statistics>().ToTable("Statistics");
            modelBuilder.Entity<Domain.Core.Action>().ToTable("Action");
            modelBuilder.Entity<UserType>().ToTable("UserType");
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<UserSession>().ToTable("UserSession");
            modelBuilder.Entity<Email>().ToTable("Email");
        }

        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<File> Files { get; set; }
        public virtual DbSet<Statistics> Statistics { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<UserSession> UserSessions { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }
        public virtual DbSet<Domain.Core.Action> Actions { get; set; }
    }
}
