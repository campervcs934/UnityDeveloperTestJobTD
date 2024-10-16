using DG.Tweening;
using ECS.Components;
using ECS.MonoBehaviours;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Systems
{
    public class ShootingTowerSystem : IEcsRunSystem, IEcsInitSystem
    {
        EcsPool<TowerComponent> _towerComponents;
        EcsPool<ShootableComponent> _shootableComponents;
        EcsPool<MovableComponent> _movableComponents;
        EcsPool<OnTriggerEnterEvent> _onTriggerEnterEvents;
        float lastShootTime;

        
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _towerComponents = world.GetPool<TowerComponent>();
            _shootableComponents = world.GetPool<ShootableComponent>();
            _movableComponents = world.GetPool<MovableComponent>();
            _onTriggerEnterEvents = world.GetPool<OnTriggerEnterEvent>();
        }
        
        public void Run(IEcsSystems systems)
        {
            lastShootTime += Time.deltaTime;
            
            var enemies = systems.GetWorld().Filter<OnTriggerEnterEvent>().Inc<EnemyComponent>().End();
            
            foreach (var entityId in enemies)
            {
                ref var enemy = ref _movableComponents.Get(entityId);
                ref var triggerEnterEvent = ref _onTriggerEnterEvents.Get(entityId);
                
                if (triggerEnterEvent.collider.gameObject.GetComponent<Entity>() is null) return;
                
                var towerId = triggerEnterEvent.collider.gameObject.GetComponent<Entity>().entityId;
                ref var tower = ref _towerComponents.Get(towerId);
                if(lastShootTime < tower.ShootInterval) return;
                lastShootTime = 0;
                ref var bullet = ref _shootableComponents.Get(towerId);
                
                var bulletGo = Object.Instantiate(bullet.bulletPrefab, tower.Weapon.transform);
                var position = triggerEnterEvent.senderGameObject.transform.position;

                var targetPosition = CalculateTargetPosition(position, enemy.moveSpeed);
                bulletGo.transform
                    .DOMove(targetPosition, 1f)
                    .OnComplete(() => Object.Destroy(bulletGo));
            }
        }

        private Vector3 CalculateTargetPosition(Vector3 targetPosition, float enemySpeed)
        {
            var targetMovementDirection = new Vector3(-1, 0, 0);
            var targetOffset = targetMovementDirection.normalized * enemySpeed;
            
            return targetPosition + targetOffset;
        }
    }
}