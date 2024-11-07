using Unity.Entities;

namespace Jobs_Demo.Step5
{
    public struct SpawnData : IComponentData
    {
        public Entity TargetPrefab;
        public int TargetCount;
        public float SpawnRadius;
    }
}