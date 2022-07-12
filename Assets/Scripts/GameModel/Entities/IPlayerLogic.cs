namespace GameModel.Entities
{
    public interface IPlayerLogic : IEntity, IUpdatable
    {
        void SetMovingState(bool isMoving);
        void SetRotationState(RotationState rotationState);
    }
}