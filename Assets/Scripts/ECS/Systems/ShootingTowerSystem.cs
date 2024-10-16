using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using ECS.Components;
using ECS.Extentions;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Systems
{
    public class ShootingTowerSystem : IEcsRunSystem, IEcsInitSystem
    {
        EcsPool<TowerComponent> _towerComponents;
        EcsPool<EnemyComponent> _enemyComponents;
        EcsPool<OnTriggerEnterEvent> _onTriggerEnterEvents;
        public void Init(IEcsSystems systems)
        {
            var world = systems.GetWorld();

            _towerComponents = world.GetPool<TowerComponent>();
            _enemyComponents = world.GetPool<EnemyComponent>();
            _onTriggerEnterEvents = world.GetPool<OnTriggerEnterEvent>();
        }
        public void Run(IEcsSystems systems)
        {
            var filter = systems.GetWorld().Filter<OnTriggerEnterEvent>().End();
            var filterEnemy = systems.GetWorld().Filter<EnemyComponent>().End();
            
            foreach (var entityId in filter)
            {
                ref var triggerEnterEvent = ref _onTriggerEnterEvents.Get(entityId);

                filterEnemy.OnTriggerEnter(_enemyComponents, triggerEnterEvent, () =>
                {
                    ref var triggerEnterEvent = ref _onTriggerEnterEvents.Get(entityId);
                   
                    
                });
            }
            
            
        }
        
        public static TweenerCore<Vector3, Vector3, VectorOptions> DoMoveInTargetLocalSpace(Transform transform, Transform target, Vector3 targetLocalEndPosition, float duration)
        {
            var t = DOTween.To(
                () => transform.position - target.transform.position,
                x => transform.position = x + target.transform.position,
                targetLocalEndPosition, 
                duration);
            t.SetTarget(transform);
            return t;
        }

      
    }
}