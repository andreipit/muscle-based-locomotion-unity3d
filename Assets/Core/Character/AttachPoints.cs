using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MuscleSystemV01
{
    public class AttachPoints : MonoBehaviour
    {
        public List<Renderer> Items;
        [Bttn("Find")][SerializeField] bool m_Find;
        [Bttn("AddColliders")][SerializeField] bool m_AddColliders;
        [Bttn("ScaleColliders")][SerializeField] bool m_ScaleColliders;
        [Bttn("RemoveColliders")][SerializeField] bool m_RemoveColliders;

        void Find() => Items = GetComponentsInChildren<Renderer>().Where(x=> x.transform.parent.name == "a" || x.transform.parent.parent.name == "a").ToList();

        void AddColliders() => Items.ForEach(x => x.gameObject.AddComponent<BoxCollider>());

        void ScaleColliders() => Items.ForEach(x => x.gameObject.GetComponent<BoxCollider>().size *= 10f);

        void RemoveColliders() => Items.ForEach(x => DestroyImmediate(x.gameObject.GetComponent<BoxCollider>()));
    }
}
