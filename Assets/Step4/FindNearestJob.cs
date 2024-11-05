using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;

namespace Jobs_Demo.Step4
{
    [BurstCompile]
    public struct FindNearestJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<float3> TargetPositions;
        [ReadOnly] public NativeArray<float3> SeekerPositions;
        public NativeArray<float3> NearestTargetPositions;

        [BurstCompile]
        public void Execute(int index)
        {
            float3 seekerPos = SeekerPositions[index];

            // If a Precise Match is Not found, the Bitwise Inverse of the Index value is returned.
            // Since the Index Value is always positive, the Bitwise Inverse will always be negative.
            int searchedIndex = TargetPositions.BinarySearch(seekerPos, new AxisXComparer{ });

            // Exact Match is not Found, so we Inverse the Value again to get the Last-Searched offset.
            if (searchedIndex < 0) searchedIndex = ~searchedIndex;

            searchedIndex = math.min(searchedIndex, TargetPositions.Length - 1);

            float3 nearestTargetPosition = TargetPositions[searchedIndex];
            float nearestTargetDistanceSq = math.distancesq(seekerPos, nearestTargetPosition);

            // Since we only sorted the X Axis, we need to search the array for the nearest target that might be in the Y Axis.
            // We need to search for it in both directions, upwards and downwards in the Array.

            // Searching upwards through the array for a closer target.
            Search(seekerPos, searchedIndex + 1, TargetPositions.Length, +1, ref nearestTargetPosition, ref nearestTargetDistanceSq);

            // Search downwards through the array for a closer target.
            Search(seekerPos, searchedIndex - 1, -1, -1, ref nearestTargetPosition, ref nearestTargetDistanceSq);

            NearestTargetPositions[index] = nearestTargetPosition;
        }

        void Search(float3 seekerPos, int startIdx, int endIdx, int step,
                    ref float3 nearestTargetPos, ref float nearestDistSq)
        {
            for (int i = startIdx; i != endIdx; i += step)
            {
                float3 targetPos = TargetPositions[i];
                float xdiff = seekerPos.x - targetPos.x;

                // If the square of the x distance is greater than the current nearest, we can stop searching.
                if ((xdiff * xdiff) > nearestDistSq) break;

                float distSq = math.distancesq(targetPos, seekerPos);

                if (distSq < nearestDistSq)
                {
                    nearestDistSq = distSq;
                    nearestTargetPos = targetPos;
                }
            }
        }
    }
}