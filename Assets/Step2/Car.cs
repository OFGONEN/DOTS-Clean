using Unity.Collections;
using Unity.Jobs;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.Jobs;

namespace Jobs_Demo.Step2
{
    public class Car : MonoBehaviour
    {
        public float Speed;
        public float SeekRadius;

        private NativeArray<int> targetIndices;

        private MaterialPropertyBlock materialPropertyBlock;
        ProfilerMarker seekMarker = new ProfilerMarker("Car.Seek");

        private void Awake() 
        {
            targetIndices = new NativeArray<int>(Spawner.TargetTransforms.Length, Allocator.Persistent);

            materialPropertyBlock = new MaterialPropertyBlock();
        }

        private void OnDestroy() 
        {
            targetIndices.Dispose();
        }

        public void Update()
        {
            MoveForward();
            ClearTargetRenderers();
            seekMarker.Begin();
            Seek();
            seekMarker.End();
            SetTargetRenderers();
        }

        void MoveForward()
        {
            transform.position += transform.forward * Speed * Time.deltaTime;
        }

        void Seek()
        {

            var seekJob = new SeekJob
            {
                SeekRadiusSq = SeekRadius * SeekRadius,
                CurrentPosition = transform.position,
                TargetIndices = targetIndices
            };

            // var coreCount = System.Environment.ProcessorCount;

            var seekJobHandle = seekJob.Schedule(Spawner.TargetTransformAccessArray);
            seekJobHandle.Complete();
        }

        void ClearTargetRenderers()
        {
            materialPropertyBlock.SetColor("_BaseColor", Color.blue);

            for (int i = 0; i < targetIndices.Length; i++)
            {
                if(targetIndices[i] == 1)
                {
                    targetIndices[i] = 0;
                    Spawner.TargetRenderers[i].SetPropertyBlock(materialPropertyBlock);
                }
            }
        }

        void SetTargetRenderers()
        {
            materialPropertyBlock.SetColor("_BaseColor", Color.red);

            for (int i = 0; i < targetIndices.Length; i++)
            {
                if(targetIndices[i] == 1)
                {
                    Spawner.TargetRenderers[i].SetPropertyBlock(materialPropertyBlock);
                }
            }
        }

        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, SeekRadius);
        }
    }
}