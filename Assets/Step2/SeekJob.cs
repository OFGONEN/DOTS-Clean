using Unity.Burst;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Jobs;

namespace Jobs_Demo.Step2
{
    [BurstCompile]
    public struct SeekJob : IJobParallelForTransform
    {
        [ReadOnly] public float2 SeekAreaLocalWidth;
        [ReadOnly] public float2 SeekAreaLocalHeight;
        [ReadOnly] public float2 SeekAreaLocalLength;
        [ReadOnly] public float4x4 Transform;

        [NativeDisableParallelForRestriction] public NativeArray<int> TargetIndices;


        [BurstCompile]
        public void Execute(int index, TransformAccess transform)
        {
            var targetPosition = transform.position;
            var targetPositionInversed = Transform.InverseTransformPoint(targetPosition);

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