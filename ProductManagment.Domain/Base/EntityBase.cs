namespace ProductManagment.Domain.Base
{
    public abstract class EntityBase<TId>
    {
        public TId Id { get; protected set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}