using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

namespace MuscleSystemV01
{
    public class CharacterMuscles: MonoBehaviour
    {
        public List<PositionConstraint> Items;
        [Bttn("FindPC")][SerializeField] bool m_Refresh;
        

        public static Transform[] GetMusclePoints(PositionConstraint _Muscle)
        {
            var sources = new List<ConstraintSource>();
            _Muscle.GetSources(sources);
            return sources.Select(x => x.sourceTransform).ToArray();
        }

        void FindPC() => Items = GetComponentsInChildren<PositionConstraint>().ToList();
    }
}


/* ----- Uncomment this for debug mode ----
[Bttn("FindT")][SerializeField] bool m_FindTransforms;
[Bttn("AddPC")][SerializeField] bool m_AddPositionConstraints;
[Bttn("AddT")][SerializeField] bool m_AddTransforms;
[Bttn("RemovePC")][SerializeField] bool m_RemovePositionConstraints;
[Bttn("RemoveT")][SerializeField] bool m_RemoveTransforms;
[Bttn("CopyT2PC")][SerializeField] bool m_CopyT2PC;
[Bttn("CopyPC2T")][SerializeField] bool m_CopyPC2T;
public List<Transforms> ItemsT;

void FindT() => ItemsT = GetComponentsInChildren<Transforms>().ToList();
void AddPC() => ItemsT.ForEach(x => x.gameObject.AddComponent<PositionConstraint>());
void AddT() => ItemsPC.ForEach(x => x.gameObject.AddComponent<Transforms>());
void RemovePC() => ItemsT.ForEach(x => DestroyImmediate(x.gameObject.GetComponent<PositionConstraint>()));
void RemoveT() => ItemsPC.ForEach(x => DestroyImmediate(x.gameObject.GetComponent<Transforms>()));
void CopyT2PC()
{
    foreach(var t in ItemsT)
    {
        List<ConstraintSource> sources = t.Items.Select(x => new ConstraintSource(){sourceTransform = x}).ToList();
        t.GetComponent<PositionConstraint>().SetSources(sources);
    }
}
void CopyPC2T()
{
    foreach(var t in ItemsPC)
    {
        var sources = new List<ConstraintSource>();
        t.GetSources(sources);
        t.GetComponent<Transforms>().Items = sources.Select(x => x.sourceTransform).ToArray();
    }
}
*/