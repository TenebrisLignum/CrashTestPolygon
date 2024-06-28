namespace Domain.Entities.Abstract
{
    public abstract class EntityIntId : Entity
    {
        public virtual int Id { get; set; }

        protected EntityIntId()
        {
        }

        protected EntityIntId(int id)
        {
            Id = id;
        }
    }
}
