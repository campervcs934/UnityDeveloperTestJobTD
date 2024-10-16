using ECS.Extentions;
using LeoEcsPhysics;
using UnityEngine;

namespace ECS.MonoBehaviours
{
    public class OnTriggerEnterChecker : MonoBehaviour
    {
        public int entityId;

        private void OnTriggerEnter(Collider col)
        {
            var ecsWorld = SharedData.world;
            var pool = ecsWorld.GetPool<OnTriggerEnterEvent>();
            if (pool.Has(entityId)) return;
            pool.Add(entityId);
            
            ref var eventComponent = ref pool.Get(entityId);
            eventComponent.senderGameObject = gameObject;
            eventComponent.collider = col;
        }
    }
}