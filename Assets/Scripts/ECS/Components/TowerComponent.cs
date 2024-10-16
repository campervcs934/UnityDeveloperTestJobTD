using UnityEngine;

namespace ECS.Components
{
    public struct TowerComponent
    {
        public GameObject Prefab;
        public float Range;
        public float ShootInterval;
        public float Damage;
    }
}
