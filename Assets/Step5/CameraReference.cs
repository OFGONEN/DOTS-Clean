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
    }
}