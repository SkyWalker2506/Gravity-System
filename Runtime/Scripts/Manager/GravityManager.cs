using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GravitySystem
{
    public class GravityManager : MonoBehaviour
    {
        List<PullData> pullableDatas = new List<PullData>();
        List<PullData> pullerDatas = new List<PullData>();

        Coroutine gravityCoroutine;
        float minDistance = .1f;


        void OnDestroy()
        {
            RemoveAllObjects();
        }

        public void AddGravityObject(PullData pullData)
        {
            RemoveGravityObject(pullData);
            if (pullData.Type==PullType.Both)
            {
                AddPullableObject(pullData);
                pullerDatas.Add(pullData);
            }
            else if (pullData.Type == PullType.Pullable)
                AddPullableObject(pullData);
            else
                pullerDatas.Add(pullData);
        }

        public void RemoveGravityObject(PullData pullData)
        {
            if (pullableDatas.Contains(pullData))
                RemovePullableObject(pullData);
            if (pullerDatas.Contains(pullData))
                pullerDatas.Remove(pullData);
        }

        void AddPullableObject(PullData pullData)
        {
            if (!pullData.ObjectRigidbody)
                pullData.AddRigidbody();
            pullableDatas.Add(pullData);
        }

        void RemovePullableObject(PullData pullData)
        {
            if (!pullData.HadRigidbody)
                pullData.RemoveRigidbody();
            pullableDatas.Remove(pullData);
        }


        void RemoveAllObjects()
        {
            pullableDatas.ForEach(p => RemoveGravityObject(p));
            pullerDatas.ForEach(p => RemoveGravityObject(p));
        }


        public void StartGravity()
        {
            gravityCoroutine = StartCoroutine(IEStartGravity());
        }

        public void StopGravity()
        {
            StopCoroutine(gravityCoroutine);
        }

        
        IEnumerator IEStartGravity()
        {

            while (true)
            {
                if (pullableDatas.Count == 0) yield break;
                PullData pullableObject;
                for (int i = 0; i < pullableDatas.Count; i++)
                {
                    pullableObject = pullableDatas[i];
                    pullableDatas[i].ObjectRigidbody.AddForce(GetAffectingPullPower(pullableObject));
                }
                yield return null;

            }
            
        }

        Vector3 GetAffectingPullPower(PullData pullData)
        {
            Vector3 pullPower = Vector3.zero;
            if (pullData.Type!=PullType.Puller)
            {
                var count = pullerDatas.Count;
                for (int i = 0; i < count; i++)
                {
                    var pullerData = pullerDatas[i];
                    if (pullerData == pullData) continue;
                    Vector3 positionDifference = pullerData.ObjectTransform.position - pullData.ObjectTransform.position;
                    pullPower += positionDifference.normalized * pullerData.PullPower* pullData.PullPower / Mathf.Pow(Mathf.Max(positionDifference.magnitude, minDistance), 2);
                }
            }
            return pullPower;
        }

    }
}
