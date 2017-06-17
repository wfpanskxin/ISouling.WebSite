using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ISouling.WebSite.Www.Data.Migrations
{
    [DbContext(typeof(UserDbContext))]
    internal class UserDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<IdentityRole<int>>(b =>
            {
                b.Property(e => e.Id)
                    .UseSqlServerIdentityColumn();

                b.Property(e => e.ConcurrencyStamp)
                    .IsConcurrencyToken();

                b.Property(e => e.Name)
                    .HasMaxLength(256);

                b.Property(e => e.NormalizedName)
                    .HasMaxLength(256);

                b.HasKey(e => e.Id);

                b.HasIndex(e => e.NormalizedName);

                b.ToTable("tbl_Roles");
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(b =>
            {
                b.Property(e => e.Id)
                    .UseSqlServerIdentityColumn();

                b.Property(e => e.ClaimType);

                b.Property(e => e.ClaimValue);

                b.Property(e => e.RoleId)
                    .IsRequired();

                b.HasKey(e => e.Id);

                b.HasIndex(e => e.RoleId);

                b.ToTable("tbl_RoleClaims");
            });

            modelBuilder.Entity<IdentityUserClaim<int>>(b =>
            {
                b.Property(e => e.Id)
                    .UseSqlServerIdentityColumn();

                b.Property(e => e.ClaimType);

                b.Property(e => e.ClaimValue);

                b.Property(e => e.UserId)
                    .IsRequired();

                b.HasKey(e => e.Id);

                b.HasIndex(e => e.UserId);

                b.ToTable("tbl_UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(b =>
           {
               b.Property(e => e.LoginProvider);

               b.Property(e => e.ProviderKey);

               b.Property(e => e.ProviderDisplayName);

               b.Property(e => e.UserId)
                   .IsRequired();

               b.HasKey(e => new { e.LoginProvider, e.ProviderKey });

               b.HasIndex(e => e.UserId);

               b.ToTable("tbl_UserLogins");
           });

            modelBuilder.Entity<IdentityUserRole<int>>(b =>
           {
               b.Property(e => e.UserId);

               b.Property(e => e.RoleId);

               b.HasKey(e => new { e.UserId, e.RoleId });

               b.HasIndex(e => e.RoleId);

               b.HasIndex(e => e.UserId);

               b.ToTable("tbl_UserRoles");
           });

            modelBuilder.Entity<IdentityUserToken<int>>(b =>
           {
               b.Property(e => e.UserId);

               b.Property(e => e.LoginProvider);

               b.Property(e => e.Name);

               b.Property(e => e.Value);

               b.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

               b.ToTable("tbl_UserTokens");
           });

            modelBuilder.Entity<IdentityUser<int>>(b =>
            {
                b.Property(e => e.Id)
                    .UseSqlServerIdentityColumn();

                b.Property(e => e.AccessFailedCount);

                b.Property(e => e.ConcurrencyStamp)
                    .IsConcurrencyToken();

                b.Property(e => e.Email)
                    .HasMaxLength(256);

                b.Property(e => e.EmailConfirmed);

                b.Property(e => e.LockoutEnabled);

                b.Property(e => e.LockoutEnd);

                b.Property(e => e.NormalizedEmail)
                    .HasMaxLength(256);

                b.Property(e => e.NormalizedUserName)
                    .HasMaxLength(256);

                b.Property(e => e.PasswordHash);

                b.Property(e => e.PhoneNumber);

                b.Property(e => e.PhoneNumberConfirmed);

                b.Property(e => e.SecurityStamp);

                b.Property(e => e.TwoFactorEnabled);

                b.Property(e => e.UserName)
                    .HasMaxLength(256);

                b.HasKey(e => e.Id);

                b.HasIndex(e => e.NormalizedEmail);

                b.HasIndex(e => e.NormalizedUserName)
                    .IsUnique();

                b.ToTable("tbl_Users");
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(b =>
            {
                b.HasOne<IdentityRole<int>>()
                    .WithMany("Claims")
                    .HasForeignKey("RoleId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<IdentityUserClaim<int>>(b =>
           {
               b.HasOne<IdentityUser<int>>()
                   .WithMany("Claims")
                   .HasForeignKey("UserId")
                   .OnDelete(DeleteBehavior.Cascade);
           });

            modelBuilder.Entity<IdentityUserLogin<int>>(b =>
            {
                b.HasOne<IdentityUser<int>>()
                    .WithMany("Logins")
                   .HasForeignKey("UserId")
                   .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<IdentityUserRole<int>>(b =>
            {
                b.HasOne<IdentityRole<int>>()
                    .WithMany("Users")
                   .HasForeignKey("RoleId")
                   .OnDelete(DeleteBehavior.Cascade);

                b.HasOne<IdentityUser<int>>()
                    .WithMany("Roles")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}