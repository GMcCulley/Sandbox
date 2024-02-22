using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using API.Entities.Identity;
using API.Entities;

namespace API.Database
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid, IdentityUserClaim<Guid>, IdentityUserRole<Guid>, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<Guid>>().HasNoKey();
            modelBuilder.Entity<IdentityUserRole<Guid>>().HasNoKey();
            modelBuilder.Entity<IdentityUserToken<Guid>>().HasNoKey();

			// Users
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasOne(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById);
                entity.HasOne(x => x.EditedBy).WithMany().HasForeignKey(x => x.EditedById);
                entity.HasOne(x => x.DeletedBy).WithMany().HasForeignKey(x => x.DeletedById);
            });

			// Roles
            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasOne(x => x.CreatedBy).WithMany().HasForeignKey(x => x.CreatedById);
                entity.HasOne(x => x.EditedBy).WithMany().HasForeignKey(x => x.EditedById);
                entity.HasOne(x => x.DeletedBy).WithMany().HasForeignKey(x => x.DeletedById);
            });
        }
    }
}