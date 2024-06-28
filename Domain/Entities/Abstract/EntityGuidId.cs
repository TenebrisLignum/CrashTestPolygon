namespace Domain.Entities.Abstract
{
    public abstract class EntityGuidId: Entity
    {
        public virtual string Id { get; set; }

        protected EntityGuidId()
        {
        }

        protected EntityGuidId(string id)
        {
            Id = id;
        }
    }
}
