using Unity.Entities;
using UnityEngine;

namespace Jobs_Demo.Step5
{
    public class PropertiesBaker : MonoBehaviour
    {
        public GameObject TargetPrefab;
        public int TargetCount;
        public int SpawnRadius;

        class Baker : Baker<PropertiesBaker>
        {
            public override void Bake(PropertiesBaker authoring)
            {
                var targetEntity = GetEntity(authoring.TargetPrefab, TransformUsageFlags.Renderable);

                var propertiesEntity = GetEntity(TransformUsageFlags.None);
                AddComponent<SpawnData>(propertiesEntity, new SpawnData
                {
                    TargetPrefab = targetEntity,
                    TargetCount = authoring.TargetCount,
                    SpawnRadius = authoring.SpawnRadius
                });
            }
        }
    }
}