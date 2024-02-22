using API.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace API.Entities.Identity
{
    public class AppUser : IdentityUser<Guid>, ITrackedEntity
    {
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public Guid? CreatedById { get; set; }
        public virtual AppUser CreatedBy { get; set; }
        public DateTime? EditedUtc { get; set; }
        public Guid? EditedById { get; set; }
        public virtual AppUser EditedBy { get; set; }
        public DateTime? DeletedUtc { get; set; }
        public Guid? DeletedById { get; set; }
        public virtual AppUser DeletedBy { get; set; }
    }
}