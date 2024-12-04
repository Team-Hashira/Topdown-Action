public interface IEntityInitComponent
{
    public void Initialize(Entity unit);
}

public interface IEntityAfterInitCompo
{
    public void AfterInit();
}

public interface IEntityDisposeComponent
{
    public void Dispose();
}
