using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using UnityEngine;

public class DelayedActivationSystem : JobComponentSystem
{
    public struct DelayedActivationReduction : IJobForEach<DelayedActivation>
    {
        public float deltaTime;

        public void Execute(ref DelayedActivation delayed)
        {
            if (delayed.Value > 0)
            {
                delayed.Value -= deltaTime;
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var delayActivationJob = new DelayedActivationReduction
        {
            deltaTime = Time.deltaTime
        }.Schedule(this, inputDeps);

        return delayActivationJob;
    }

}
