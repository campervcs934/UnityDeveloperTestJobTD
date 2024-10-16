using System.Linq;
using DG.Tweening;
using ECS.Components;
using ECS.MonoBehaviours;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Systems
{
    public class MovementSystem : IEcsInitSystem, IEcsRunSystem
    {
        EcsPool<MovableComponent> _movableComponents;
        EcsPool<EnemyComponent> _enemyComponents;
        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();
            _movableComponents = world.GetPool<MovableComponent>();
            _enemyComponents = world.GetPool<EnemyComponent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            var enemies = systems.GetWorld().Filter<EnemyComponent>().Inc<MovableComponent>().End();
            
            foreach (var enemy in enemies)
            {
                ref var movableComponent = ref _movableComponents.Get(enemy);
                ref var enemyComponent = ref _enemyComponents.Get(enemy);
                var distance = Vector3.Distance(movableComponent.targetPoint.position, enemyComponent.SpawnPoint.position);
                var time = distance / movableComponent.moveSpeed;
                if (movableComponent.isMoving) return;
                movableComponent.transform.DOMoveX(movableComponent.targetPoint.position.x, time)
                    .SetEase(Ease.Linear)
                    .OnStart(() =>
                    {
                        ref var movableComponent = ref _movableComponents.Get(enemy);
                        movableComponent.isMoving = true;
                    })
                    .OnComplete(() =>
                    {
                        ref var movableComponent = ref _movableComponents.Get(enemy);
                        movableComponent.isMoving = false;
                        var enemyGo = Object.FindObjectsOfType<Entity>().Single(x=>x.entityId == enemy);
                        Object.Destroy(enemyGo);
                    });
            }
        }
    }
}