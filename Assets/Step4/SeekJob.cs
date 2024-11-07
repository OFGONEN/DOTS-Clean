using Unity.Jobs;
using Unity.Collections;
using Unity.Mathematics;
using Unity.Burst;


namespace Jobs_Demo.Step4
{
    [BurstCompile]
    public struct SeekJob : IJobParallelFor
    {
        [ReadOnly] public float SeekRadiusSq;
        [ReadOnly] public float3 CurrentPosition;
        [ReadOnly] public NativeArray<float3> TargetPositions;

        [NativeDisableParallelForRestriction] public NativeArray<int> TargetIndices;


        [BurstCompile]
        public void Execute(int index)
        {
            var distance = math.distancesq(CurrentPosition, TargetPositions[index]);

            if (distance < SeekRadiusSq)
            {
                TargetIndices[index] = 1;
            }
        }
    }
}