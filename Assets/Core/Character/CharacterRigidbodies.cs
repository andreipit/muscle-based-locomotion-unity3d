using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace MuscleSystemV01
{
    public class CharacterRigidbodies: MonoBehaviour
    {
        public List<Rigidbody> Items;
        [Bttn("Find")][SerializeField] bool m_Refresh;


        public static void AddOnCollisionEnter(List<Rigidbody> _Items)
            => _Items.ForEach(x => x.gameObject.AddComponent<OnCollisionEnterScript>());

        public static Vector3 GetMassCenter(List<Rigidbody> _RBodies)
        {
            Vector3 result = Vector3.zero;
            float totalWeight = 0f;
            foreach (Rigidbody body in _RBodies)
            {
                result += body.worldCenterOfMass * body.mass;
                totalWeight += body.mass;
            }
            return (result / totalWeight);
        }

        void Find() => Items = GetComponentsInChildren<Rigidbody>().ToList();
    }
}

/* ----- Uncomment this for debug mode ----
[Bttn("AttachOnCollisionEnter")][SerializeField] bool m_AttachOnCollisionEnterScript;
[Bttn("DetachOnCollisionEnter")][SerializeField] bool m_DetachOnCollisionEnterScript;
void AttachOnCollisionEnter() => AddScript(Items);
void DetachOnCollisionEnter() => Items.ForEach(x => DestroyImmediate(x.gameObject.GetComponent<OnCollisionEnterScript>()));
*/