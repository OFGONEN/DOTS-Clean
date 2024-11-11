using Unity.Burst;
using Unity.Entities;
using Unity.Profiling;
using Unity.Transforms;

namespace Jobs_Demo.Step5
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    [UpdateAfter(typeof(MoveSystem))]
    partial struct SeekSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TargetTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var seekerEntity = SystemAPI.GetSingletonEntity<SeekerTag>();
            var seekerLocalTransform = SystemAPI.GetComponent<LocalTransform>(seekerEntity);
            var seekData = SystemAPI.GetComponent<SeekData>(seekerEntity);

            var ecbParaller = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
            var targetQuery = SystemAPI.QueryBuilder().WithAll<TargetTag, LocalTransform>().Build();
            
            ProfilerMarker seekMarker = new ProfilerMarker("Car.Seek");

            var seekJob = new SeekJob
            {
                SeekerPosition = seekerLocalTransform.Position,
                SeekRadius = seekData.SeekRadius,
                ECB_Parallel = ecbParaller
            };

            seekMarker.Begin();
            var seekJobHandle = seekJob.ScheduleParallel(targetQuery, state.Dependency);
            seekJobHandle.Complete();
            seekMarker.End();
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}