using GameModel.Map;
using UnityEngine;

namespace GameModel.Entities.Weapons
{
    public interface IWeapon : IUpdatable
    {
        bool IsCanFire { get; }

        void TryFire(Vector2 position, Vector2 direction);
        void Fire(Vector2 position, Vector2 direction);
    }
}