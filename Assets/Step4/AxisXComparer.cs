using System.Collections.Generic;
using Unity.Mathematics;

namespace Jobs_Demo.Step4
{
    public struct AxisXComparer : IComparer<float3>
    {
        public int Compare(float3 a, float3 b) => a.x.CompareTo(b.x);
    }
}