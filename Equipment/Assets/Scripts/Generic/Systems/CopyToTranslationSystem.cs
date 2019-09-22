using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class CopyToTranslationSystem : JobComponentSystem
{
    [BurstCompile]
    public struct CopyWorldToTranslation : IJobForEach<LocalToWorld, Translation, CopyTransformFromGameObject>
    {
        public void Execute([ReadOnly] ref LocalToWorld lWorld, [WriteOnly] ref Translation translation, [ReadOnly] ref CopyTransformFromGameObject transfromObject)
        {
            float3 newPos = new float3
            {
                x = lWorld.Value.c3.x,
                y = lWorld.Value.c3.y,
                z = lWorld.Value.c3.z
            };

            translation.Value = newPos;
        }
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var copyToTranslation = new CopyWorldToTranslation
        { }.Schedule(this, inputDeps);

        return copyToTranslation;
    }
}
