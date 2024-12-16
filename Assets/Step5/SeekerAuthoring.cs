using Unity.Entities;
using UnityEngine;

namespace Jobs_Demo.Step5
{
    public class SeekerAuthoring : MonoBehaviour
    {
        public float Speed;
        public Vector3 SeekAreaSize;
    }

    class SeekerBaker : Baker<SeekerAuthoring>
    {
        public override void Bake(SeekerAuthoring authoring)
        {
            var bakedEntity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<SeekerTag>(bakedEntity);
            AddComponent<MoveData>(bakedEntity, new MoveData { Speed = authoring.Speed });
            AddComponent<SeekData>(bakedEntity, new SeekData { 
                LocalWidth = new Vector2(-authoring.SeekAreaSize.x / 2, authoring.SeekAreaSize.x / 2),
                LocalHeight = new Vector2(-authoring.SeekAreaSize.y / 2, authoring.SeekAreaSize.y / 2),
                LocalLength = new Vector2(-authoring.SeekAreaSize.z / 2, authoring.SeekAreaSize.z / 2)
            });
        }
    }
}