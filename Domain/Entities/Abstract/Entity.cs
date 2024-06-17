namespace Domain.Entities.Abstract
{
    public abstract class Entity
    {
        public virtual int Id { get; set; }

        protected Entity()
        {
        }

        protected Entity(int id)
        {
            Id = id;
        }
    }
}
