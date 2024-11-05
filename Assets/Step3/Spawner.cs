using UnityEngine;

namespace Jobs_Demo.Step3
{
    public class Spawner : MonoBehaviour
    {
        [HideInInspector]
        public static Transform[] TargetTransforms;
        [HideInInspector]
        public static Transform[] SeekerTransforms;

        public static Spawner Instance;

        public GameObject SeekerPrefab;
        public GameObject TargetPrefab;

        public int NumberOfSeekers;
        public int NumberOfTargets;

        public Vector2 Bound;

        public void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            Random.InitState(42);

            SeekerTransforms = new Transform[NumberOfSeekers];
            for (int i = 0; i < NumberOfSeekers; i++)
            {
                GameObject go = GameObject.Instantiate(SeekerPrefab);

                var mover = go.GetComponent<Mover>();
                Vector2 direction = Random.insideUnitCircle;
                mover.Direction = new Vector3(direction.x, 0, direction.y);

                go.transform.position = new Vector3(Random.Range(-Bound.x, Bound.x), 0, Random.Range(-Bound.y, Bound.y));

                SeekerTransforms[i] = go.transform;
            }
            
            TargetTransforms = new Transform[NumberOfTargets];
            for (int i = 0; i < NumberOfTargets; i++)
            {
                GameObject go = GameObject.Instantiate(TargetPrefab);

                var mover = go.GetComponent<Mover>();
                Vector2 direction = Random.insideUnitCircle;
                mover.Direction = new Vector3(direction.x, 0, direction.y);

                go.transform.position = new Vector3(Random.Range(-Bound.x, Bound.x), 0, Random.Range(-Bound.y, Bound.y));

                TargetTransforms[i] = go.transform;
            }
        }

        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLineList(new Vector3[] {
                new Vector3(-Bound.x, 0, -Bound.y),
                new Vector3(-Bound.x, 0, Bound.y),
                new Vector3(-Bound.x, 0, Bound.y),
                new Vector3(Bound.x, 0, Bound.y),
                new Vector3(Bound.x, 0, Bound.y),
                new Vector3(Bound.x, 0, -Bound.y),
                new Vector3(Bound.x, 0, -Bound.y),
                new Vector3(-Bound.x, 0, -Bound.y),
            });
        }
    }
}