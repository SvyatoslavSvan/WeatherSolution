namespace DataCoreModule.Core.Models.Entities.Base;

public abstract class Entity
{
    protected Entity()
    {
        Id = Guid.Empty;
    }

    protected Entity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; protected set; }
}