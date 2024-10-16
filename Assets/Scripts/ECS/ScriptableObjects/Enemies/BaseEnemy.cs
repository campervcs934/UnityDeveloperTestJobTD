using System;
using UnityEngine;

namespace ECS.ScriptableObjects.Enemies
{
    [Serializable]
    public class BaseEnemy : ScriptableObject
    {
        public GameObject prefab;
        public float health;
        public float speed;
    }
}