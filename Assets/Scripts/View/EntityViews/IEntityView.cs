using GameModel.Entities;

namespace View.EntityViews
{
    public interface IEntityView
    {
        IEntity EntityModel { get; set; }
    }
}