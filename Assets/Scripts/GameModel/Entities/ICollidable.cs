namespace GameModel.Entities
{
    public interface ICollidable
    {
        void OnCollision(ICollidable other);
    }
}