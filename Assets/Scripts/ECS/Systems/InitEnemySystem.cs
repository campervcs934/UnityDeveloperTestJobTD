using ECS.Configs;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Systems
{
    public class InitEnemySystem : IEcsInitSystem
    {
        EcsPool<EnemyComponent> _enemyComponents;
        public void Init(IEcsSystems systems)
        {
            var config = systems.GetShared<GameConfig>();
            var world = systems.GetWorld();
            _enemyComponents = world.GetPool<EnemyComponent>();
            
            foreach (var enemy in config.Enemies)
            {
                var enemyGameObject = Object
                    .Instantiate(enemy.prefab, config.EnemySpawnPoint.position, Quaternion.identity);
                enemyGameObject.AddComponent<OnTriggerEnterChecker>();
                
                var enemyId = world.NewEntity();
                _enemyComponents.Add(enemyId);

                ref var enemyComponent = ref _enemyComponents.Get(enemyId);
                enemyComponent.EnemyPrefab = enemyGameObject;
                enemyComponent.Health = enemy.health;
                enemyComponent.MoveSpeed = enemy.speed;
                enemyComponent.SpawnPoint = config.EnemySpawnPoint;
            }
        }
    }
}