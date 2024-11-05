using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

namespace Jobs_Demo.Step3
{
    public class FindNearest : MonoBehaviour
    {
        NativeArray<float3> targetPositions;
        NativeArray<float3> seekerPositions;
        NativeArray<float3> nearestTargetPositions;

        public void Start()
        {
            targetPositions = new NativeArray<float3>(Spawner.Instance.NumberOfTargets, Allocator.Persistent);
            seekerPositions = new NativeArray<float3>(Spawner.Instance.NumberOfSeekers, Allocator.Persistent);
            nearestTargetPositions = new NativeArray<float3>(Spawner.Instance.NumberOfSeekers, Allocator.Persistent);
        }

        private void OnDestroy() 
        {
            targetPositions.Dispose();
            seekerPositions.Dispose();
            nearestTargetPositions.Dispose();
        }

        public void Update()
        {
            for (int i = 0; i < seekerPositions.Length; i++)
            {
                seekerPositions[i] = Spawner.SeekerTransforms[i].position;
            }

            for (int i = 0; i < targetPositions.Length; i++)
            {
                targetPositions[i] = Spawner.TargetTransforms[i].position;
            }

            FindNearestJob findNearestJob = new FindNearestJob 
            {
                TargetPositions = targetPositions,
                SeekerPositions = seekerPositions,
                NearestTargetPositions = nearestTargetPositions
            };

            JobHandle findNearestJobHandle = findNearestJob.Schedule(seekerPositions.Length, seekerPositions.Length / 8);
            findNearestJobHandle.Complete();

            for (int i = 0; i < seekerPositions.Length; i++)
            {
                Debug.DrawLine(seekerPositions[i], nearestTargetPositions[i]);
            }
        }
    }
}