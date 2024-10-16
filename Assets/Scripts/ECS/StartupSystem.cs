using ECS.Configs;
using ECS.Extentions;
using ECS.Systems;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS
{
    public class StartupSystem : MonoBehaviour
    {
        [SerializeField] private GameConfig _config;
        private EcsSystems _systems;
        private EcsWorld _world;

        private void Awake()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world, _config);

            SharedData.world = _world;
            EcsPhysicsEvents.ecsWorld = _world;

            _systems.Add(new InitTowerSystem())
                .Add(new InitEnemySystem())
                .Add(new MovementSystem())
                .Add(new ShootingTowerSystem())
                .Init();
        }

        public void Update() => _systems.Run();

        public void Dispose()
        {
            EcsPhysicsEvents.ecsWorld = null;
            _systems.Destroy();
            _world.Destroy();
        }
    }
}