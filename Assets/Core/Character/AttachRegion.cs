using System.Linq;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MuscleSystemV01
{
    public class AttachRegion : MonoBehaviour
    {
        [Serializable] 
        public class RegionPoints { public BoxCollider Collider; public List<Vector3> Points; }

        [SerializeField] List<RegionPoints> m_Cache;
        [SerializeField] Vector3Int m_Resolution = new Vector3Int(3, 3, 3);
        [Bttn("Generate")][SerializeField] bool m_Generate;
        [Bttn("Clear")][SerializeField] bool m_Clear;
        [SerializeField] bool m_Draw;


        void Generate()
        {
            m_Cache = new List<RegionPoints>();
            foreach (var region in GetComponentsInChildren<BoxCollider>().Where(x=> x.transform.parent.name == "a" || x.transform.parent.parent.name == "a"))
                m_Cache.Add(new RegionPoints(){ Collider = region, Points = FindPointsInRegion(region, m_Resolution)});
        }

        void Clear() => m_Cache = new List<RegionPoints>();

        void OnDrawGizmos()
        {
            if (m_Cache == null) 
                    return;
            if (m_Draw)
            {
                Gizmos.color = Color.cyan;
                foreach(var pair in m_Cache)
                    foreach(var point in pair.Points)
                        Gizmos.DrawSphere(point, 0.005f);
            }
        }

        static List<Vector3> FindPointsInRegion(BoxCollider _Region, Vector3Int _Res)
        {
            var root = new GameObject("template").transform;

            for (float x = -0.5f; x <= 0.5f; x += 1f/(float)_Res.x)
                for (float y = -0.5f; y <= 0.5f; y += 1f/(float)_Res.y)
                    for (float z = -0.5f; z <= 0.5f; z += 1f/(float)_Res.z)
                        CreateEntry(x, y, z, root);

            root.parent = _Region.transform;
            ResetTransform(root);

            root.parent = _Region.transform.parent;
            List<Transform> points = root.GetComponentsInChildren<Transform>().Where(x=>x!=root).ToList();
            points.ForEach(x => x.transform.parent = _Region.transform.parent);
            DestroyImmediate(root.gameObject);

            List<Vector3> positions = points.Select(x => x.position).ToList();
            foreach(var point in points)
                DestroyImmediate(point.gameObject);
            return positions;
        }

        static void CreateEntry(float _X, float _Y, float _Z, Transform _Root)
        {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.hideFlags = HideFlags.DontSave;
            cube.transform.position = new Vector3(_X, _Y, _Z);
            cube.transform.localScale = new Vector3(0.03f, 0.03f, 0.03f);
            cube.transform.parent = _Root;
        }

        static void ResetTransform(Transform _Root)
        {
            _Root.transform.localPosition = Vector3.zero;
            _Root.transform.localRotation = Quaternion.Euler(Vector3.zero);
            _Root.transform.localScale = _Root.parent.GetComponent<BoxCollider>().size;
        }
    }
}