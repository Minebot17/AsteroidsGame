using UnityEngine;

namespace GameModel.Entities
{
    public class BigAsteroidLogic : BaseAsteroidLogic
    {
        public override Vector2 Position { get; }
        public override float RotationAngle { get; }
        public override void FixedUpdate()
        {
            throw new System.NotImplementedException();
        }
    }
}