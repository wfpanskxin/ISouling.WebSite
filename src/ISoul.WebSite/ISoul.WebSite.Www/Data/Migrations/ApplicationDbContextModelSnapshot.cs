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

            modelBuilder.Entity("IdentityRole", b =>
                {
                    b.Property("Id");

                    b.Property("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("IdentityRoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property("ClaimType");

                    b.Property("ClaimValue");

                    b.Property("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("IdentityUserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property("ClaimType");

                    b.Property("ClaimValue");

                    b.Property("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("IdentityUserLogin", b =>
                {
                    b.Property("LoginProvider");

                    b.Property("ProviderKey");

                    b.Property("ProviderDisplayName");

                    b.Property("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("IdentityUserRole", b =>
                {
                    b.Property("UserId");

                    b.Property("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("IdentityUserToken", b =>
                {
                    b.Property("UserId");

                    b.Property("LoginProvider");

                    b.Property("Name");

                    b.Property("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("IdentityUser", b =>
                {
                    b.Property("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property("PasswordHash");

                    b.Property("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("IdentityRoleClaim", b =>
                {
                    b.HasOne("IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IdentityUserClaim", b =>
                {
                    b.HasOne("IdentityUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IdentityUserLogin", b =>
                {
                    b.HasOne("IdentityUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("IdentityUserRole", b =>
                {
                    b.HasOne("IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("IdentityUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
