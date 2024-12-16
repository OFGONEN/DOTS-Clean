using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

namespace Jobs_Demo.Step5
{
    public class CameraReference : MonoBehaviour
    {
        public static Camera Instance;

        private void Awake()
        {
            Instance = GetComponent<Camera>();
        }

        private void OnDrawGizmosSelected()
        {
            var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            //CreateEntityQuery for Seeker Data, Seeker Tag, and Local Transform
            var query = entityManager.CreateEntityQuery(
                ComponentType.ReadOnly<SeekData>(),
                ComponentType.ReadOnly<SeekerTag>(),
                ComponentType.ReadOnly<LocalTransform>()
            );

            var seekData = query.ToComponentDataArray<SeekData>(Allocator.Temp);
            var localTransforms = query.ToComponentDataArray<LocalTransform>(Allocator.Temp);

            for (int i = 0; i < seekData.Length; i++)
            {
                var data = seekData[i];
                var localTransform = localTransforms[i];

                Gizmos.color = Color.green;

                //Front
                Gizmos.DrawLine(localTransform.TransformPoint(new Vector3(data.LocalWidth.x, data.LocalHeight.x, data.LocalLength.y)), localTransform.TransformPoint(new Vector3(data.LocalWidth.y, data.LocalHeight.x, data.LocalLength.y)));
                Gizmos.DrawLine(localTransform.TransformPoint(new Vector3(data.LocalWidth.x, data.LocalHeight.y, data.LocalLength.y)), localTransform.TransformPoint(new Vector3(data.LocalWidth.y, data.LocalHeight.y, data.LocalLength.y)));
                Gizmos.DrawLine(localTransform.TransformPoint(new Vector3(data.LocalWidth.x, data.LocalHeight.x, data.LocalLength.y)), localTransform.TransformPoint(new Vector3(data.LocalWidth.x, data.LocalHeight.y, data.LocalLength.y)));
                Gizmos.DrawLine(localTransform.TransformPoint(new Vector3(data.LocalWidth.y, data.LocalHeight.x, data.LocalLength.y)), localTransform.TransformPoint(new Vector3(data.LocalWidth.y, data.LocalHeight.y, data.LocalLength.y)));

                //Back
                Gizmos.DrawLine(localTransform.TransformPoint(new Vector3(data.LocalWidth.x, data.LocalHeight.x, data.LocalLength.x)), localTransform.TransformPoint(new Vector3(data.LocalHeight.y, data.LocalHeight.x, data.LocalLength.x)));
                Gizmos.DrawLine(localTransform.TransformPoint(new Vector3(data.LocalWidth.x, data.LocalHeight.y, data.LocalLength.x)), localTransform.TransformPoint(new Vector3(data.LocalHeight.y, data.LocalHeight.y, data.LocalLength.x)));
                Gizmos.DrawLine(localTransform.TransformPoint(new Vector3(data.LocalWidth.x, data.LocalHeight.x, data.LocalLength.x)), localTransform.TransformPoint(new Vector3(data.LocalHeight.x, data.LocalHeight.y, data.LocalLength.x)));
                Gizmos.DrawLine(localTransform.TransformPoint(new Vector3(data.LocalWidth.y, data.LocalHeight.x, data.LocalLength.x)), localTransform.TransformPoint(new Vector3(data.LocalHeight.y, data.LocalHeight.y, data.LocalLength.x)));

                //Left
                Gizmos.DrawLine(localTransform.TransformPoint(new Vector3(data.LocalWidth.x, data.LocalHeight.x, data.LocalLength.x)), localTransform.TransformPoint(new Vector3(data.LocalWidth.x, data.LocalHeight.x, data.LocalLength.y)));
                Gizmos.DrawLine(localTransform.TransformPoint(new Vector3(data.LocalWidth.x, data.LocalHeight.y, data.LocalLength.x)), localTransform.TransformPoint(new Vector3(data.LocalWidth.x, data.LocalHeight.y, data.LocalLength.y)));

                //Right
                Gizmos.DrawLine(localTransform.TransformPoint(new Vector3(data.LocalWidth.y, data.LocalHeight.x, data.LocalLength.x)), localTransform.TransformPoint(new Vector3(data.LocalWidth.y, data.LocalHeight.x, data.LocalLength.y)));
                Gizmos.DrawLine(localTransform.TransformPoint(new Vector3(data.LocalWidth.y, data.LocalHeight.y, data.LocalLength.x)), localTransform.TransformPoint(new Vector3(data.LocalWidth.y, data.LocalHeight.y, data.LocalLength.y)));
            }
        }
    }
}