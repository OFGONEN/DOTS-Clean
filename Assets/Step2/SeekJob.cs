using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

namespace Jobs_Demo.Step2
{
    public struct SeekJob : IJobParallelForTransform
    {
        [ReadOnly] public float SeekRadiusSq;
        [ReadOnly] public float3 CurrentPosition;

        [NativeDisableParallelForRestriction] public NativeArray<int> TargetIndices;


        public void Execute(int index, TransformAccess transform)
        {
            var targetPosition = transform.position;
            var distance = math.distancesq(targetPosition, CurrentPosition);

            if (distance < SeekRadiusSq)
            {
                TargetIndices[index] = 1;
            }
        }
    }
}