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
        [ReadOnly] public float2 SeekAreaLocalWidth;
        [ReadOnly] public float2 SeekAreaLocalHeight;
        [ReadOnly] public float2 SeekAreaLocalLength;
        [ReadOnly] public float4x4 Transform;

        public EntityCommandBuffer.ParallelWriter ECB_Parallel;

        [BurstCompile]
        public void Execute(in Entity entity, in LocalTransform localTransform)
        {
            var targetPositionInversed = Transform.InverseTransformPoint(localTransform.Position);

            var isOutside = targetPositionInversed.x < SeekAreaLocalWidth.x || targetPositionInversed.x > SeekAreaLocalWidth.y ||
                            targetPositionInversed.y < SeekAreaLocalHeight.x || targetPositionInversed.y > SeekAreaLocalHeight.y ||
                            targetPositionInversed.z < SeekAreaLocalLength.x || targetPositionInversed.z > SeekAreaLocalLength.y;

            if (!isOutside) // Inside Seek Area
            {
                ECB_Parallel.SetComponent<URPMaterialPropertyBaseColor>(0, entity, new URPMaterialPropertyBaseColor { Value = new float4(1, 0, 0, 1) });
                ECB_Parallel.SetComponentEnabled<SelectedTag>(0, entity, true);
            }
        }
    }
}