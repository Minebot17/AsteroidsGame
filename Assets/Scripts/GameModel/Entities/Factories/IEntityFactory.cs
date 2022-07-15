namespace GameModel.Entities.Factories
{
    public interface IEntityFactory<out T> where T : IEntity
    {
        T Create();
    }
}