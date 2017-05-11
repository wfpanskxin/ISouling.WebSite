using ISoul.Component.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ISoul.WebSite.Www.Data
{
    public class UserDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<IdentityUser<int>>().ToTable("tbl_Users");

            //https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/inheritance
            //https://docs.microsoft.com/en-us/ef/core/modeling/inheritance
            //https://docs.microsoft.com/en-us/ef/core/modeling/relational/inheritance
            builder.Entity<MemberUser>();
            builder.Entity<CompanyUser>();
        }
    }
}
