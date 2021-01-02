using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GravitySystem
{
    public class Test : MonoBehaviour
    {
        [SerializeField] Color pullerColor;
        [SerializeField] Color pullableColor;
        [SerializeField] Color pullablePullerColor;

        [SerializeField] int pullerCount=1;
        [SerializeField] int pullableCount=5;
        [SerializeField] int pullablePullerCount =3;
        [Range(1,100)]
        [SerializeField] float minPullPower = 3;
        [Range(1, 100)]
        [SerializeField] float maxPullPower = 23;

        GravityManager gravityManager;

        private void Awake()
        {
            gravityManager = new GameObject("Gravity Manager").AddComponent<GravityManager>();
            for (int i = 0; i < pullerCount; i++)
            {
                var pullerObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                pullerObject .name="Puller "+(i+1);
                pullerObject.GetComponent<MeshRenderer>().material.color = pullerColor;
                pullerObject.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(0, 10), Random.Range(-10, 10));
                var pullData = ScriptableObject.CreateInstance<PullData>();
                pullData.Init(pullerObject.transform, PullType.Puller, Random.Range(minPullPower, maxPullPower));
                gravityManager.AddGravityObject(pullData);

            }
            for (int i = 0; i < pullableCount; i++)
            {
                var pullableObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                pullableObject.name = "Pullable " + (i + 1);

                pullableObject.GetComponent<MeshRenderer>().material.color = pullableColor;
                pullableObject.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(0, 10), Random.Range(-10, 10));
                var pullData = ScriptableObject.CreateInstance<PullData>();
                pullData.Init(pullableObject.transform, PullType.Puller, Random.Range(minPullPower, maxPullPower));
                gravityManager.AddGravityObject(pullData);

            }
            for (int i = 0; i < pullablePullerCount ; i++)
            {
                var pullablePullerObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                pullablePullerObject.name= "PullablePuller " + (i+1);
                pullablePullerObject.GetComponent<MeshRenderer>().material.color = pullablePullerColor;
                pullablePullerObject.transform.position = new Vector3(Random.Range(-10, 10), Random.Range(0, 10), Random.Range(-10, 10));
                var pullData = ScriptableObject.CreateInstance<PullData>();
            //    Debug.Log.(pullablePullerObject.ObjectTransform);
            //    Debug.Log.(pullablePullerObject.ObjectTransform);

                pullData.Init(pullablePullerObject.transform, PullType.Both, Random.Range(minPullPower, maxPullPower));
                gravityManager.AddGravityObject(pullData);
            }
            gravityManager.StartGravity();
        }
    }
}
