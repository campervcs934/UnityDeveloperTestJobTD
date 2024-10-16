using System;
using System.Collections.Generic;
using ECS.ScriptableObjects.Enemies;
using ECS.ScriptableObjects.Towers;
using UnityEngine;

namespace ECS.Configs
{
    [Serializable]
    public class GameConfig
    {
        public List<TowerConfig> Towers;
        public List<BaseEnemy> Enemies;
        public Transform EnemySpawnPoint;
        public Transform EnemyTargetPoint;
    }

  
}