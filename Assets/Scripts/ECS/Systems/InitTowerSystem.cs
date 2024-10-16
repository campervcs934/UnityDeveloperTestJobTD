using ECS.Components;
using ECS.Configs;
using ECS.MonoBehaviours;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using UnityEngine;

namespace ECS.Systems
{
    public class InitTowerSystem : IEcsInitSystem
    {
        EcsPool<TowerComponent> _towerComponents;
        EcsPool<ShootableComponent> _shootableComponents;
        
        public void Init(IEcsSystems systems)
        {
            var config = systems.GetShared<GameConfig>();
            var world = systems.GetWorld();
            _towerComponents = world.GetPool<TowerComponent>();
            _shootableComponents = world.GetPool<ShootableComponent>();
            
            foreach (var towerConfig in config.Towers)
            {
              var tower = SpawnTower(towerConfig, out var towerPosition);
              var weapon = SpawnWeapon(towerPosition, towerConfig, tower);
              InitEcsEntity(world, towerConfig, tower, weapon);
            }
        }

        private void InitEcsEntity(EcsWorld world, TowerConfig towerConfig, GameObject tower, GameObject weapon)
        {
            var towerId = world.NewEntity();
            _towerComponents.Add(towerId);
            _shootableComponents.Add(towerId);
            tower.AddComponent<Entity>().entityId = towerId;
            InitTowerComponent(towerId, weapon, towerConfig);
        }

        private void InitTowerComponent(int towerId, GameObject weapon, TowerConfig towerConfig)
        {
            ref var towerComponent = ref _towerComponents.Get(towerId);
            towerComponent.Damage = towerConfig.tower.damage;
            towerComponent.ShootInterval = towerConfig.tower.shootInterval;
            towerComponent.Range = towerConfig.tower.range;
            towerComponent.Weapon = weapon;
            
            ref var shootableComponent = ref _shootableComponents.Get(towerId);
            shootableComponent.bulletPrefab = towerConfig.tower.bulletPrefab;
        }

        private static GameObject SpawnWeapon(Vector3 towerPosition, TowerConfig towerConfig, GameObject tower)
        {
            var weaponPosition = new Vector3(towerPosition.x, towerPosition.y + 3, towerPosition.z);
            return Object.Instantiate(towerConfig.tower.weaponPrefab, weaponPosition, Quaternion.identity, tower.transform);
        }

        private static GameObject SpawnTower(TowerConfig towerConfig, out Vector3 towerPosition)
        {
            var tower
                = Object.Instantiate(towerConfig.tower.towerPrefab, towerConfig.spawnPoint.position, Quaternion.identity);
            towerPosition = tower.transform.position;
            return tower;
        }
    }
}