using UnityEngine;

namespace ECS.Components
{
    public struct MovableComponent
    {
        public Transform transform;
        public Transform targetPoint;
        public float moveSpeed;
        public bool isMoving;
    }
}