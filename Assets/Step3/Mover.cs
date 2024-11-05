using UnityEngine;

namespace Jobs_Demo.Step3
{
    public class Mover : MonoBehaviour
    {
        [HideInInspector]
        public Vector3 Direction;

        private void Update()
        {
            transform.position += Direction * Time.deltaTime;
        }
    }
}