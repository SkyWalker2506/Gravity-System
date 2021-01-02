using System;
using UnityEngine;

namespace GravitySystem
{
    [CreateAssetMenu(fileName = "PullData",menuName ="Gravity System/Pull data")]
    public class PullData : ScriptableObject
    {
        public Transform ObjectTransform;
        public PullType Type;
        public float PullPower=1;
        public Rigidbody ObjectRigidbody { get; private set; }
        public bool HadRigidbody { get; private set; }


        private void Awake()
        {
            Init(ObjectTransform, Type, PullPower);
        }

        public void Init(Transform objectTransform, PullType type, float pullPower = 1)
        {
            ObjectTransform = objectTransform;
            Debug.Log(ObjectTransform);
            Type = type;
            PullPower = Mathf.Max(pullPower, 1);
            if(ObjectTransform)
            ObjectRigidbody = ObjectTransform.GetComponent<Rigidbody>();
            HadRigidbody = ObjectRigidbody != null;
        }

        public void AddRigidbody()
        {
            if(ObjectRigidbody)
                throw new InvalidOperationException("Already have rigidbody");
            ObjectRigidbody = ObjectTransform.gameObject.AddComponent<Rigidbody>();
        }

        public void RemoveRigidbody()
        {
            if (HadRigidbody)
                throw new InvalidOperationException("It had rigidbody as default and can't remove");
            Destroy(ObjectRigidbody); 
        }
    }
}
