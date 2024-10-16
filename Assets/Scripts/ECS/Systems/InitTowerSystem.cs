using ECS.Components;
using ECS.Configs;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Systems
{
    public class InitTowerSystem : IEcsInitSystem
    {
        EcsPool<TowerComponent> _towerComponents;
        public void Init(IEcsSystems systems)
        {
            var config = systems.GetShared<GameConfig>();
            var world = systems.GetWorld();
            _towerComponents = world.GetPool<TowerComponent>();
            
            foreach (var towerConfig in config.Towers)
            {
              var tower = Object.Instantiate(towerConfig.tower.towerPrefab, towerConfig.spawnPoint.position, Quaternion.identity);

              var towerPosition = tower.transform.position;
              var weaponPosition = new Vector3(towerPosition.x, towerPosition.y + 3, towerPosition.z);
              
              Object.Instantiate(towerConfig.tower.weaponPrefab, weaponPosition, Quaternion.identity, tower.transform);

              var towerId = world.NewEntity();
              _towerComponents.Add(towerId);
              
              ref var towerComponent = ref _towerComponents.Get(towerId);
              towerComponent.Prefab = tower;
              towerComponent.Damage = towerConfig.tower.damage;
              towerComponent.ShootInterval = towerConfig.tower.shootInterval;
              towerComponent.Range = towerConfig.tower.range;
            }
        }
    }
}