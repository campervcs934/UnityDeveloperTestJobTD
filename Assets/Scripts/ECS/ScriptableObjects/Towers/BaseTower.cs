using System;
using UnityEngine;

namespace ECS.ScriptableObjects.Towers
{
    [Serializable]
    public class BaseTower : ScriptableObject
    {
        public GameObject towerPrefab;
        public GameObject weaponPrefab;
        public GameObject bulletPrefab;
        public float range;
        public float shootInterval;
        public float damage;
    }
}