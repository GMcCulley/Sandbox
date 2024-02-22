using API.Entities.Identity;
using API.Interfaces;

namespace API.Entities
{
    public class TrackedEntity : ITrackedEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedUtc { get; set; } = DateTime.UtcNow;
        public Guid? CreatedById { get; set; }
        public virtual AppUser? CreatedBy { get; set; }
        public DateTime? EditedUtc { get; set; }
        public Guid? EditedById { get; set; }
        public virtual AppUser? EditedBy { get; set; }
        public DateTime? DeletedUtc { get; set; }
        public Guid? DeletedById { get; set; }
        public virtual AppUser? DeletedBy { get; set; }
    }
}