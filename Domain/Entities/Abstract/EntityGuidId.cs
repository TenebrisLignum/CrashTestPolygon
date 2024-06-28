namespace Domain.Entities.Abstract
{
    public abstract class EntityGuidId: Entity
    {
        public virtual Guid Id { get; set; }

        protected EntityGuidId()
        {
        }

        protected EntityGuidId(Guid id)
        {
            Id = id;
        }
    }
}
