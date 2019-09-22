//using System.Collections;
//using System.Collections.Generic;
//using Unity.Burst;
//using Unity.Collections;
//using Unity.Entities;
//using Unity.Jobs;
//using UnityEngine;


//public class DestroySystem : JobComponentSystem
//{
//    private EntityQuery m_LifeGroup, m_HealthGroup;

//    [BurstCompile]
//    public struct LifeTimeCollection : IJobForEachWithEntity<LifeTime>
//    {
//        [WriteOnly] public NativeQueue<Entity>.ParallelWriter queue;

//        public void Execute(Entity entity, int index, [ReadOnly] ref LifeTime life)
//        {
//            if(life.Value < 0)
//            {
//                queue.Enqueue(entity);
//            }
//        }
//    }

//    [BurstCompile]
//    public struct HealthCollection : IJobForEachWithEntity<Health>
//    {
//        [WriteOnly] public NativeQueue<Entity>.ParallelWriter queue;

//        public void Execute(Entity entity, int index, [ReadOnly] ref Health health)
//        {
//            if (health.Value < 0)
//            {
//                queue.Enqueue(entity);
//            }
//        }
//    }

//    [BurstCompile]
//    public struct FillDestroyArrayJob : IJob
//    {
//        [WriteOnly] public NativeArray<Entity> entities;
//        public NativeQueue<Entity> entityQueue;

//        public void Execute()
//        {
//            for (int i = 0; i < entities.Length; i++)
//            {
//                entities[i] = entityQueue.Dequeue();
//            }
//        }
//    }


//    protected override JobHandle OnUpdate(JobHandle inputDeps)
//    {
//        var destroyQueue = new NativeQueue<Entity>(Allocator.Temp);

//        if(m_LifeGroup.CalculateEntityCount() > 0)
//        {
//            var lifeJob = new LifeTimeCollection
//            {
//                queue = destroyQueue.AsParallelWriter()
//            }.Schedule(this, inputDeps);
//            lifeJob.Complete();
//        }

//        if (m_HealthGroup.CalculateEntityCount() > 0)
//        {
//            var healthJob = new HealthCollection
//            {
//                queue = destroyQueue.AsParallelWriter()
//            }.Schedule(this, inputDeps);
//            healthJob.Complete();
//        }

//        // Dequeue and destroy all the entities
//        if (destroyQueue.Count > 0)
//        {
//            var destroyArray = new NativeArray<Entity>(destroyQueue.Count, Allocator.TempJob);
//            var fillJob = new FillDestroyArrayJob
//            {
//                entities = destroyArray,
//                entityQueue = destroyQueue
//            }.Schedule(inputDeps);
//            fillJob.Complete();

//            EntityManager.DestroyEntity(destroyArray);
//            destroyArray.Dispose();
//        }

//        destroyQueue.Dispose();

//        Debug.Log("Return");
//        return inputDeps;
//    }

//    protected override void OnCreate()
//    {
//        m_LifeGroup = GetEntityQuery(new EntityQueryDesc
//        {
//            All = new ComponentType[] { ComponentType.ReadWrite<LifeTime>() }
//        });

//        m_HealthGroup = GetEntityQuery(new EntityQueryDesc
//        {
//            All = new ComponentType[] { ComponentType.ReadWrite<Health>() }
//        });
//    }
//}
