using GameModel.Entities;

namespace View.Utils
{
    public interface IEntityView<T> where T : IEntity
    {
        T EntityModel { get; set; }
    }
}