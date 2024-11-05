using UnityEngine;

namespace Jobs_Demo.Step1
{
    public class FindNearest : MonoBehaviour
    {
        public void Update()
        {
            Vector3 nearestTargetPosition = default;
            float nearestTargetSquare = float.MaxValue;

            var currentPosition = transform.position;

            foreach (var target in Spawner.TargetTransforms)
            {
                var targetPosition = target.position;
                Vector3 offset = targetPosition - currentPosition;
                float distanceSquare = offset.sqrMagnitude;

                if(distanceSquare < nearestTargetSquare)
                {
                    nearestTargetSquare = distanceSquare;
                    nearestTargetPosition = targetPosition;
                }
            }

            Debug.DrawLine(currentPosition, nearestTargetPosition);
        }
    }
}