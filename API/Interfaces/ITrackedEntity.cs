namespace API.Interfaces
{
    public interface ITrackedEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedUtc { get; set; }
        public Guid? CreatedById { get; set; } 
        public DateTime? EditedUtc { get; set; }
        public Guid? EditedById { get; set; }
        public DateTime? DeletedUtc { get; set; }
        public Guid? DeletedById { get; set; }
    }
}