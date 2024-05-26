namespace Domain.Entities.Abstract
{
    public abstract class Entity
    {
        public virtual long Id { get; protected set; }

        protected Entity()
        {
        }

        protected Entity(long id)
        {
            Id = id;
        }
    }
}
