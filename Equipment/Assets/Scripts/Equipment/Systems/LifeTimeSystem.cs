using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class LifeTimeSystem : JobComponentSystem
{
    [BurstCompile]
    public struct ReduceLifeTimeJob : IJobForEach<LifeTime>
    {
        public float deltaTime;

        public void Execute(ref LifeTime life)
        {
            if(life.Value > 0)
            {
                life.Value -= deltaTime;
            }
        }
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return new ReduceLifeTimeJob
        {
            deltaTime = Time.deltaTime
        }.Schedule(this, inputDeps);
    }
}
