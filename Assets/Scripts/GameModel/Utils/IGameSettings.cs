using UnityEngine;

namespace GameModel.Utils
{
    public interface IGameSettings
    {
        float PlayerMovingSpeed { get; }
        float PlayerRotationSpeed { get; }
        float PlayerDragModifier { get; }

        int BulletFireCooldown { get; }
        int BulletLifeDuration { get; }
        float BulletSpeed { get; }
        int LaserFireCooldown { get; }
        int LaserChargeCooldown { get; }
        int MaxLaserCharges { get; }

        float BigAsteroidsSpeed { get; }
        float BigAsteroidsTorque { get; }
        int BigAsteroidsFragmentsCount { get; }
        float UfoSpeed { get; }
        int MaxBigAsteroidsCount { get; }
        int SpawnBigAsteroidPeriod { get; }
        int MaxUfosCount { get; }
        int SpawnUfoPeriod { get; }
        float TeleporterBorderOffset { get; }

        int ScoreBigAsteroid { get; }
        int ScoreSmallAsteroid { get; }
        int ScoreUfo { get; }

        Vector2 MapSize { get; }
    }
}