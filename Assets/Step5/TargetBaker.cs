using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace Jobs_Demo.Step5
{
    class TargetBaker : MonoBehaviour
    {
        public Color Color;
    }

    class TargetBakerBaker : Baker<TargetBaker>
    {
        public override void Bake(TargetBaker authoring)
        {
            var bakedEntity = GetEntity(TransformUsageFlags.None);

            AddComponent<URPMaterialPropertyBaseColor>(bakedEntity, new URPMaterialPropertyBaseColor { Value = new float4(authoring.Color.r, authoring.Color.g, authoring.Color.b, authoring.Color.a) });
            AddComponent<TargetTag>(bakedEntity);
            AddComponent<SelectedTag>(bakedEntity);
        }
    }
}