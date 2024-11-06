using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;

namespace Jobs_Demo.Step1
{
    public class Car : MonoBehaviour
    {
        public float Speed;
        public float SeekRadius;

        private List<Renderer> targetRenderers;
        private MaterialPropertyBlock materialPropertyBlock;
        ProfilerMarker seekMarker = new ProfilerMarker("Car.Seek");

        private void Awake() 
        {
            targetRenderers = new List<Renderer>(128);

            materialPropertyBlock = new MaterialPropertyBlock();
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
            var currentPosition = transform.position;
            var seekRadiusSq = SeekRadius * SeekRadius;

            for (int i = 0; i < Spawner.TargetTransforms.Length; i++)
            {
                var targetPosition = Spawner.TargetTransforms[i].position;
                var distance = (targetPosition - currentPosition).sqrMagnitude;

                if (distance < seekRadiusSq)
                {
                    targetRenderers.Add(Spawner.TargetRenderers[i]);
                }
            }
        }

        void ClearTargetRenderers()
        {
            materialPropertyBlock.SetColor("_BaseColor", Color.blue);

            foreach (var renderer in targetRenderers)
                renderer.SetPropertyBlock(materialPropertyBlock);

            targetRenderers.Clear();
        }

        void SetTargetRenderers()
        {
            materialPropertyBlock.SetColor("_BaseColor", Color.red);

            foreach (var renderer in targetRenderers)
                renderer.SetPropertyBlock(materialPropertyBlock);
        }


        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, SeekRadius);
        }
    }
}