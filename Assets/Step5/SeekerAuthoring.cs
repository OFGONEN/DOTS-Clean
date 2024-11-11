using Unity.Entities;
using UnityEngine;

namespace Jobs_Demo.Step5
{
    public class SeekerAuthoring : MonoBehaviour
    {
        public float Speed;
        public float SeekRadius;
    }

    class SeekerBaker : Baker<SeekerAuthoring>
    {
        public override void Bake(SeekerAuthoring authoring)
        {
            var bakedEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<MoveData>(bakedEntity, new MoveData { Speed = authoring.Speed });
            AddComponent<SeekData>(bakedEntity, new SeekData { SeekRadius = authoring.SeekRadius });
            AddComponent<SeekerTag>(bakedEntity);
        }
    }
}