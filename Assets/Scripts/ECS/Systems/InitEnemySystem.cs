using ECS.Components;
using ECS.Configs;
using ECS.MonoBehaviours;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Systems
{
    public class InitEnemySystem : IEcsInitSystem
    {
        EcsPool<EnemyComponent> _enemyComponents;
        EcsPool<MovableComponent> _movableComponents;
        public void Init(IEcsSystems systems)
        {
            var config = systems.GetShared<GameConfig>();
            var world = systems.GetWorld();
            _enemyComponents = world.GetPool<EnemyComponent>();
            _movableComponents = world.GetPool<MovableComponent>();
            
            foreach (var enemy in config.Enemies)
            {
                var enemyId = world.NewEntity();
                _enemyComponents.Add(enemyId);
                _movableComponents.Add(enemyId);
                
                var enemyGameObject 
                    = Object.Instantiate(enemy.prefab, config.EnemySpawnPoint.position, Quaternion.identity);
                enemyGameObject.AddComponent<ECS.MonoBehaviours.OnTriggerEnterChecker>().entityId = enemyId;
                enemyGameObject.AddComponent<Entity>().entityId = enemyId;

                ref var enemyComponent = ref _enemyComponents.Get(enemyId);
                enemyComponent.Health = enemy.health;
                enemyComponent.SpawnPoint = config.EnemySpawnPoint;

                ref var movableComponent = ref _movableComponents.Get(enemyId);
                movableComponent.transform = enemyGameObject.transform;
                movableComponent.targetPoint = config.EnemyTargetPoint;
                movableComponent.moveSpeed = enemy.speed;
            }
        }
    }
}