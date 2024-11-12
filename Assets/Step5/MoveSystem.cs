using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

namespace Jobs_Demo.Step5
{
    [UpdateInGroup(typeof(SimulationSystemGroup), OrderFirst = true)]
    [BurstCompile]
    partial struct MoveSystem : ISystem
    {
        [BurstCompile]
        public void OnCreate(ref SystemState state)
        {
            state.RequireForUpdate<SeekerTag>();
            state.RequireForUpdate<MoveData>();
        }

        public void OnUpdate(ref SystemState state)
        {
            var seeker = SystemAPI.GetSingletonEntity<SeekerTag>();
            var seekerLocalTransform = SystemAPI.GetComponent<LocalTransform>(seeker);  
            var moveData = SystemAPI.GetComponent<MoveData>(seeker);

            var cameraTransform = CameraReference.Instance.transform;

            var rotation = quaternion.RotateY(math.TORADIANS * moveData.Speed * Time.deltaTime);
            seekerLocalTransform.Position = math.mul(rotation, seekerLocalTransform.Position);
            seekerLocalTransform.Rotation = math.mul(rotation, seekerLocalTransform.Rotation);

            cameraTransform.RotateAround(Vector3.zero, Vector3.up, moveData.Speed * Time.deltaTime);

            SystemAPI.SetComponent(seeker, seekerLocalTransform);
        }

        [BurstCompile]
        public void OnDestroy(ref SystemState state)
        {

        }
    }
}