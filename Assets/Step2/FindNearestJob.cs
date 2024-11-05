using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Jobs_Demo.Step2
{
    [BurstCompile]
    public struct FindNearestJob : IJob
    {
        [ReadOnly] public NativeArray<float3> TargetPositions;
        [ReadOnly] public NativeArray<float3> SeekerPositions;
        public NativeArray<float3> NearestTargetPositions;

        [BurstCompile]
        public void Execute()
        {
            for (int i = 0; i < SeekerPositions.Length; i++)
            {
                float3 seekerPos = SeekerPositions[i];
                float nearestTargetSquare = float.MaxValue;

                for (int j = 0; j < TargetPositions.Length; j++)
                {
                    float3 targetPosition = TargetPositions[j];
                    float distanceSquare = math.distancesq(seekerPos, targetPosition);

                    if(distanceSquare < nearestTargetSquare)
                    {
                        nearestTargetSquare = distanceSquare;
                        NearestTargetPositions[i] = targetPosition;
                    }
                }
            }
        }
    }
}