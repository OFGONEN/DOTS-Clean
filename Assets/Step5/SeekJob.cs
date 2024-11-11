using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

namespace Jobs_Demo.Step5
{
    [BurstCompile]
    public partial struct SeekJob : IJobEntity
    {
        [ReadOnly] public float3 SeekerPosition;
        [ReadOnly] public float SeekRadius;

        public EntityCommandBuffer.ParallelWriter ECB_Parallel;

        [BurstCompile]
        public void Execute(in Entity entity, in LocalTransform localTransform)
        {
            var distance = math.distancesq(SeekerPosition, localTransform.Position);

            if(distance < SeekRadius)
            {
                ECB_Parallel.SetComponent<URPMaterialPropertyBaseColor>(0, entity, new URPMaterialPropertyBaseColor { Value = new float4(1, 0, 0, 1) });
                ECB_Parallel.SetComponentEnabled<SelectedTag>(0, entity, true);
            }
        }
    }
}