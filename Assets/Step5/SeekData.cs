using Unity.Entities;
using Unity.Mathematics;

namespace Jobs_Demo.Step5
{
    public struct SeekData : IComponentData
    {
        public float2 LocalWidth;
        public float2 LocalHeight;
        public float2 LocalLength;
    }
}