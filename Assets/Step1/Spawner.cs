using UnityEngine;

namespace Jobs_Demo.Step1
{
    public class Spawner : MonoBehaviour
    {
        public static Transform[] TargetTransforms;
        public static Renderer[] TargetRenderers;

        public int TargetCount;
        public GameObject TargetPrefab;

        public float SpawnRadius;

        private void Awake()
        {
            Random.InitState(52);

            TargetTransforms = new Transform[TargetCount];
            TargetRenderers = new Renderer[TargetCount];
            for (int i = 0; i < TargetCount; i++)
            {
                var spawnPosition = Random.insideUnitSphere * SpawnRadius;
                var target = Instantiate(TargetPrefab, spawnPosition, Quaternion.identity);

                TargetTransforms[i] = target.transform;
                TargetRenderers[i] = target.GetComponent<Renderer>();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, SpawnRadius);
        }
    }
}