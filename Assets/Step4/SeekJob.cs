using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;
using Unity.Transforms;


namespace Jobs_Demo.Step4
{
    [BurstCompile]
    public struct SeekJob : IJobParallelFor
    {
        [ReadOnly] public float2 SeekAreaLocalWidth;
        [ReadOnly] public float2 SeekAreaLocalHeight;
        [ReadOnly] public float2 SeekAreaLocalLength;
        [ReadOnly] public float4x4 Transform;

        [ReadOnly] public NativeArray<float3> TargetPositions;

        [NativeDisableParallelForRestriction] public NativeArray<int> TargetIndices;


        [BurstCompile]
        public void Execute(int index)
        {
            var targetPositionInversed = Transform.InverseTransformPoint(TargetPositions[index]);

            var isOutside = targetPositionInversed.x < SeekAreaLocalWidth.x || targetPositionInversed.x > SeekAreaLocalWidth.y ||
                            targetPositionInversed.y < SeekAreaLocalHeight.x || targetPositionInversed.y > SeekAreaLocalHeight.y ||
                            targetPositionInversed.z < SeekAreaLocalLength.x || targetPositionInversed.z > SeekAreaLocalLength.y;

            if (!isOutside) // Inside Seek Area
            {
                TargetIndices[index] = 1;
            }
        }
    }
}