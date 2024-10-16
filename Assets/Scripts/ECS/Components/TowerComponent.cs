using UnityEngine;

namespace ECS.Components
{
    public struct TowerComponent
    {
        public float Range;
        public float ShootInterval;
        public float Damage;
        public GameObject Weapon;
    }
}
