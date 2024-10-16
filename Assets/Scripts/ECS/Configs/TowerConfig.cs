using System;
using ECS.ScriptableObjects.Towers;
using UnityEngine;

namespace ECS.Configs
{
    [Serializable]
    public class TowerConfig
    {
        public BaseTower tower;
        public Transform spawnPoint;
    }
}