using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Jobs_Demo.Step5
{
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderFirst = true)]
    partial struct SpawnSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SpawnData>();
        }

        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            state.Enabled = false;

            var spawnData = SystemAPI.GetSingleton<SpawnData>();

            var targetEntities = state.EntityManager.Instantiate(spawnData.TargetPrefab, spawnData.TargetCount, Allocator.Temp);

            for (int i = 0; i < targetEntities.Length; i++)
            {
                var spawnPosition = UnityEngine.Random.insideUnitSphere * spawnData.SpawnRadius;

                state.EntityManager.SetComponentData<LocalTransform>(targetEntities[i], new LocalTransform { Position = spawnPosition, Rotation = quaternion.identity, Scale = 0.1f });
            }
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}