using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;

namespace Jobs_Demo.Step5
{
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    [BurstCompile]
    partial struct UnselectTargetSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<TargetTag>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            var entities = SystemAPI.QueryBuilder().WithAll<TargetTag, SelectedTag>().Build().ToEntityArray(Allocator.Temp);

            foreach (var entity in entities)
            {
                SystemAPI.SetComponent<URPMaterialPropertyBaseColor>(entity, new URPMaterialPropertyBaseColor { Value = new float4(0, 0, 1, 1) });
                SystemAPI.SetComponentEnabled<SelectedTag>(entity, false);
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}