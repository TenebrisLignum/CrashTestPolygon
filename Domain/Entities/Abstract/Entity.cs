namespace Domain.Entities.Abstract
{
    public abstract class Entity
    {
        public virtual int Id { get; protected set; }

        protected Entity()
        {
        }

        protected Entity(int id)
        {
            Id = id;
        }
    }
}
