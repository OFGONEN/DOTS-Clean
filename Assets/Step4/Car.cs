using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Profiling;
using UnityEngine;

namespace Jobs_Demo.Step4
{
    public class Car : MonoBehaviour
    {
        public float Speed;
        public Vector3 SeekAreaSize;

        private NativeArray<int> targetIndices;

        private MaterialPropertyBlock materialPropertyBlock;
        ProfilerMarker seekMarker = new ProfilerMarker("Car.Seek");

        private Vector2 localWidth;
        private Vector2 localHeight;
        private Vector2 localLength;


        private void Awake() 
        {
            targetIndices = new NativeArray<int>(Spawner.TargetTransforms.Length, Allocator.Persistent);

            materialPropertyBlock = new MaterialPropertyBlock();

            localWidth = new Vector2(-SeekAreaSize.x / 2, SeekAreaSize.x / 2);
            localHeight = new Vector2(-SeekAreaSize.y / 2, SeekAreaSize.y / 2);
            localLength = new Vector2(-SeekAreaSize.z / 2, SeekAreaSize.z / 2);
        }

        private void OnDestroy() 
        {
            targetIndices.Dispose();
        }

        public void Update()
        {
            Move();
            ClearTargetRenderers();
            seekMarker.Begin();
            Seek();
            seekMarker.End();
            SetTargetRenderers();
        }

        void Move()
        {
            transform.parent.Rotate(Vector3.up * Speed * Time.deltaTime);
        }

        void Seek()
        {
            var seekJob = new SeekJob
            {
                SeekAreaLocalWidth = localWidth,
                SeekAreaLocalHeight = localHeight,
                SeekAreaLocalLength = localLength,
                Transform = transform.localToWorldMatrix,
                TargetPositions = Spawner.TargetPositions,
                TargetIndices = targetIndices
            };

            var coreCount = System.Environment.ProcessorCount;

            var seekJobHandle = seekJob.Schedule(Spawner.TargetPositions.Length, coreCount);
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

            //Front
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(SeekAreaSize.x / -2f, SeekAreaSize.y / -2f, SeekAreaSize.z / 2f)), transform.TransformPoint(new Vector3(SeekAreaSize.x / 2f, SeekAreaSize.y / -2f, SeekAreaSize.z / 2f)));
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(SeekAreaSize.x / -2f, SeekAreaSize.y / 2f, SeekAreaSize.z / 2f)), transform.TransformPoint(new Vector3(SeekAreaSize.x / 2f, SeekAreaSize.y / 2f, SeekAreaSize.z / 2f)));
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(SeekAreaSize.x / -2f, SeekAreaSize.y / -2f, SeekAreaSize.z / 2f)), transform.TransformPoint(new Vector3(SeekAreaSize.x / -2f, SeekAreaSize.y / 2f, SeekAreaSize.z / 2f)));
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(SeekAreaSize.x / 2f, SeekAreaSize.y / -2f, SeekAreaSize.z / 2f)), transform.TransformPoint(new Vector3(SeekAreaSize.x / 2f, SeekAreaSize.y / 2f, SeekAreaSize.z / 2f)));

            //Back
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(SeekAreaSize.x / -2f, SeekAreaSize.y / -2f, SeekAreaSize.z / -2f)), transform.TransformPoint(new Vector3(SeekAreaSize.x / 2f, SeekAreaSize.y / -2f, SeekAreaSize.z / -2f)));
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(SeekAreaSize.x / -2f, SeekAreaSize.y / 2f, SeekAreaSize.z / -2f)), transform.TransformPoint(new Vector3(SeekAreaSize.x / 2f, SeekAreaSize.y / 2f, SeekAreaSize.z / -2f)));
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(SeekAreaSize.x / -2f, SeekAreaSize.y / -2f, SeekAreaSize.z / -2f)), transform.TransformPoint(new Vector3(SeekAreaSize.x / -2f, SeekAreaSize.y / 2f, SeekAreaSize.z / -2f)));
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(SeekAreaSize.x / 2f, SeekAreaSize.y / -2f, SeekAreaSize.z / -2f)), transform.TransformPoint(new Vector3(SeekAreaSize.x / 2f, SeekAreaSize.y / 2f, SeekAreaSize.z / -2f)));

            //Left
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(SeekAreaSize.x / -2f, SeekAreaSize.y / -2f, SeekAreaSize.z / -2f)), transform.TransformPoint(new Vector3(SeekAreaSize.x / -2f, SeekAreaSize.y / -2f, SeekAreaSize.z / 2f)));
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(SeekAreaSize.x / -2f, SeekAreaSize.y / 2f, SeekAreaSize.z / -2f)), transform.TransformPoint(new Vector3(SeekAreaSize.x / -2f, SeekAreaSize.y / 2f, SeekAreaSize.z / 2f)));

            //Right
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(SeekAreaSize.x / 2f, SeekAreaSize.y / -2f, SeekAreaSize.z / -2f)), transform.TransformPoint(new Vector3(SeekAreaSize.x / 2f, SeekAreaSize.y / -2f, SeekAreaSize.z / 2f)));
            Gizmos.DrawLine(transform.TransformPoint(new Vector3(SeekAreaSize.x / 2f, SeekAreaSize.y / 2f, SeekAreaSize.z / -2f)), transform.TransformPoint(new Vector3(SeekAreaSize.x / 2f, SeekAreaSize.y / 2f, SeekAreaSize.z / 2f)));

        }
    }
}