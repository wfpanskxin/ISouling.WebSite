using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ISoul.WebSite.Www.Data.Migrations
{
    [DbContext(typeof(UserDbContext))]
    partial class UserDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity<IdentityRole<int>>(b =>
            {
                b.Property<int>("Id")
                    .UseSqlServerIdentityColumn();

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken();

                b.Property<string>("Name")
                    .HasAnnotation("MaxLength", 256);

                b.Property<string>("NormalizedName")
                    .HasAnnotation("MaxLength", 256);

                b.HasKey("Id");

                b.HasIndex("NormalizedName")
                    .HasName("RoleNameIndex");

                b.ToTable("tbl_Roles");
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(b =>
           {
               b.Property<int>("Id")
                    .UseSqlServerIdentityColumn();

               b.Property<string>("ClaimType");

               b.Property<string>("ClaimValue");

               b.Property<int>("RoleId")
                   .IsRequired();

               b.HasKey("Id");

               b.HasIndex("RoleId");

               b.ToTable("tbl_RoleClaims");
           });

            modelBuilder.Entity<IdentityUserClaim<int>>(b =>
            {
                b.Property<int>("Id")
                    .UseSqlServerIdentityColumn();

                b.Property<string>("ClaimType");

                b.Property<string>("ClaimValue");

                b.Property<int>("UserId")
                    .IsRequired();

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("tbl_UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(b =>
           {
               b.Property<string>("LoginProvider");

               b.Property<string>("ProviderKey");

               b.Property<string>("ProviderDisplayName");

               b.Property<int>("UserId")
                   .IsRequired();

               b.HasKey("LoginProvider", "ProviderKey");

               b.HasIndex("UserId");

               b.ToTable("tbl_UserLogins");
           });

            modelBuilder.Entity<IdentityUserRole<int>>(b =>
           {
               b.Property<int>("UserId");

               b.Property<int>("RoleId");

               b.HasKey("UserId", "RoleId");

               b.HasIndex("RoleId");

               b.HasIndex("UserId");

               b.ToTable("tbl_UserRoles");
           });

            modelBuilder.Entity<IdentityUserToken<int>>(b =>
           {
               b.Property<int>("UserId");

               b.Property<string>("LoginProvider");

               b.Property<string>("Name");

               b.Property<string>("Value");

               b.HasKey("UserId", "LoginProvider", "Name");

               b.ToTable("tbl_UserTokens");
           });

            modelBuilder.Entity<IdentityUser<int>>(b =>
            {
                b.Property<int>("Id")
                    .UseSqlServerIdentityColumn();

                b.Property<int>("AccessFailedCount");

                b.Property<string>("ConcurrencyStamp")
                    .IsConcurrencyToken();

                b.Property<string>("Email")
                    .HasAnnotation("MaxLength", 256);

                b.Property<bool>("EmailConfirmed");

                b.Property<bool>("LockoutEnabled");

                b.Property<DateTimeOffset?>("LockoutEnd");

                b.Property<string>("NormalizedEmail")
                    .HasAnnotation("MaxLength", 256);

                b.Property<string>("NormalizedUserName")
                    .HasAnnotation("MaxLength", 256);

                b.Property<string>("PasswordHash");

                b.Property<string>("PhoneNumber");

                b.Property<bool>("PhoneNumberConfirmed");

                b.Property<string>("SecurityStamp");

                b.Property<bool>("TwoFactorEnabled");

                b.Property<string>("UserName")
                    .HasAnnotation("MaxLength", 256);

                b.HasKey("Id");

                b.HasIndex("NormalizedEmail")
                    .HasName("EmailIndex");

                b.HasIndex("NormalizedUserName")
                    .IsUnique()
                    .HasName("UserNameIndex");

                b.ToTable("tbl_Users");
            });

            modelBuilder.Entity<IdentityRoleClaim<int>>(b =>
           {
               b.HasOne(typeof(IdentityRole<int>))
                   .WithMany("Claims")
                   .HasForeignKey("RoleId")
                   .OnDelete(DeleteBehavior.Cascade);
           });

            modelBuilder.Entity<IdentityUserClaim<int>>(b =>
           {
               b.HasOne(typeof(IdentityUser<int>))
                   .WithMany("Claims")
                   .HasForeignKey("UserId")
                   .OnDelete(DeleteBehavior.Cascade);
           });

            modelBuilder.Entity<IdentityUserLogin<int>>(b =>
            {
                b.HasOne(typeof(IdentityUser<int>))
                    .WithMany("Logins")
                   .HasForeignKey("UserId")
                   .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<IdentityUserRole<int>>(b =>
            {
                b.HasOne(typeof(IdentityRole<int>))
                    .WithMany("Users")
                   .HasForeignKey("RoleId")
                   .OnDelete(DeleteBehavior.Cascade);

                b.HasOne(typeof(IdentityUser<int>))
                    .WithMany("Roles")
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
