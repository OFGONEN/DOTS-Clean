using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Jobs_Demo.Step3
{
    public class Spawner : MonoBehaviour
    {
        public static Transform[] TargetTransforms;
        public static Renderer[] TargetRenderers;

        public static NativeArray<float3> TargetPositions;

        public int TargetCount;
        public GameObject TargetPrefab;

        public float SpawnRadius;

        private void Awake()
        {
            UnityEngine.Random.InitState(52);

            TargetTransforms = new Transform[TargetCount];
            TargetRenderers = new Renderer[TargetCount];
            TargetPositions = new NativeArray<float3>(TargetCount, Allocator.Persistent);

            for (int i = 0; i < TargetCount; i++)
            {
                var spawnPosition = UnityEngine.Random.insideUnitSphere * SpawnRadius;
                var target = Instantiate(TargetPrefab, spawnPosition, Quaternion.identity);

                TargetTransforms[i] = target.transform;
                TargetRenderers[i] = target.GetComponent<Renderer>();
                TargetPositions[i] = spawnPosition;
            }
        }

        private void OnDestroy() 
        {
            TargetPositions.Dispose();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, SpawnRadius);
        }
    }
}