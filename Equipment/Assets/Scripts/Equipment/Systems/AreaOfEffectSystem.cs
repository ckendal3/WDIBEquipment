using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Jobs;
using Unity.Transforms;

// TODO: Implement HitLayers
// TODO: Re-evaluate Switch statement and component handling
// TODO: Implement a cylinder solution
public class AreaOfEffectSystem : ComponentSystem
{
    private int maxHits = 24;
    private int hitCount = 0;

    private Collider[] colliders;
    private LayerMask hitLayers;    

    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref Equipment equipment, ref AreaOfEffect AOE, ref Translation position, ref Rotation rotation)=> 
        {
            switch (AOE.Shape)
            {
                case ShapeType.Sphere:
                    hitCount = Physics.OverlapSphereNonAlloc(position.Value, AOE.Size.x, colliders, hitLayers);
                    break;
                case ShapeType.Box:
                    hitCount = Physics.OverlapBoxNonAlloc(position.Value, AOE.Size, colliders, rotation.Value, hitLayers);
                    break;
                case ShapeType.Capsule:
                    hitCount = Physics.OverlapCapsuleNonAlloc(position.Value, position.Value + AOE.Size, AOE.Size.x, colliders, hitLayers);
                    break;
                case ShapeType.Cylinder:
                    Debug.LogWarning("Cylinder shape is not yet implemented");
                    hitCount = Physics.OverlapSphereNonAlloc(position.Value, AOE.Size.x, colliders, hitLayers);
                    break;
                default:
                    return;
            }

            for (int i = 0; i < hitCount; i++)
            {
                //Debug.Log($"Overlapping: {colliders[i].name}");
                // check for interfaces

                // if(damageable)
                // { 
                    if(EntityManager.HasComponent<Damage>(entity))
                    {

                    }
                    else if(EntityManager.HasComponent<Heal>(entity))
                    {

                    }
                // }

                //if(physicsable)
                //{
                    if (EntityManager.HasComponent<ForceApplier>(entity))
                    {
                        ForceApplier forceApplier = EntityManager.GetComponentData<ForceApplier>(entity);
                        // Apply force to physicsable person
                    }
                //}

            }
        });
    }
    protected override void OnCreate()
    {
        colliders = new Collider[maxHits];
        hitLayers = LayerMask.NameToLayer("Everything");
    }
}
