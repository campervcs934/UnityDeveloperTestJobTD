using System;
using LeoEcsPhysics;
using Leopotam.EcsLite;

namespace ECS.Extentions
{
    public static class Extention
    {
        // public static void OnTriggerEnter(this EcsFilter filter, 
        //     EcsPool<EnemyComponent> enemyComponents, 
        //     OnTriggerEnterEvent onTriggerEnterEvent,
        //     Action action)
        // {
        //     foreach (var enemyId in filter)
        //     {
        //         ref var enemyComponent = ref enemyComponents.Get(enemyId);
        //
        //         if (enemyComponent.EnemyPrefab.name.Equals(onTriggerEnterEvent.senderGameObject.name))
        //         {
        //             action?.Invoke();  
        //         }
        //     }
        // }
    }
}