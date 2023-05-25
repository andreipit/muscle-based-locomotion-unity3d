using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GizmoCylinder : MonoBehaviour
{

    public static void DrawLines(Transform[] _Path, float _Radius, Color _Col, GameObject _Selection = null, float _Volume = -1)
    {
        float pathLength = 0;
        for (int i = 0; i < _Path.Length - 1; i++)
            pathLength += (_Path[i].position - _Path[i + 1].position).magnitude;

        for (int i = 0; i < _Path.Length - 1; i++)
        {
            Transform from = _Path[i];
            Transform to = _Path[i + 1];
            Vector3 direction0 = (from.position - to.position).normalized;
            float segmentVolume = _Volume==-1 ? _Volume: _Volume * (from.position - to.position).magnitude / pathLength;
            DrawLine(from.position, to.position, _Radius:_Radius, _Col: _Col, _Selection, _Volume: segmentVolume);
        }
    }

    public static void DrawLine(Vector3 _Start, Vector3 _End, float _Radius, Color _Col, GameObject _Selection = null, float _Volume = -1)
    {
        Mesh _M = Resources.GetBuiltinResource<Mesh>("Cylinder.fbx");

        Vector3 pos = _Start + (_End - _Start) / 2;
        Vector3 fwd = Vector3.Cross(_End - _Start, new Vector3(0.1253f, 0.83f, 0.24f));
        Vector3 up = _End - _Start; //if (fwd == Vector3.zero || up == Vector3.zero) return;
        Quaternion rot = Quaternion.LookRotation(forward: fwd, upwards: up);
        float h = (_End - _Start).magnitude / 2;
        float r = _Volume == -1 ? _Radius : Mathf.Sqrt(_Volume / (Mathf.PI * h)); // use radius or get it from volume

        Vector3 scale = new Vector3(r * 2, h, r * 2); // scale == diameter == r*2

        Gizmos.color = (_Selection != null && UnityEditor.Selection.Contains(_Selection)) ? Color.blue : _Col;
        Gizmos.DrawMesh(_M, position: pos, rotation: rot, scale: scale); //Graphics.DrawMeshNow(_M, position: pos, rotation: rot);
        
        Gizmos.color = Color.white;
    }

    public static void DrawTubes(Transform[] _Path, float _Radius, Color _Col, GameObject _Selection = null, float _Volume = -1)
    {
        float pathLength = 0;
        for (int i = 0; i < _Path.Length - 1; i++)
            pathLength += (_Path[i].position - _Path[i + 1].position).magnitude;

        for (int i = 0; i < _Path.Length - 1; i++)
        {
            Transform from = _Path[i];
            Transform to = _Path[i + 1];
            Vector3 direction0 = (from.position - to.position).normalized;
            float segmentVolume = _Volume == -1 ? _Volume : _Volume * (from.position - to.position).magnitude / pathLength;
            DrawMesh(from.position, to.position, _Radius: _Radius, _Col: _Col, _Selection, _Volume: segmentVolume);
        }
    }

    public static void DrawMesh(Vector3 _Start, Vector3 _End, float _Radius, Color _Col, GameObject _Selection = null, float _Volume = -1)
    {
        //Material m_Mat = (Material)AssetDatabase.LoadAssetAtPath("Assets/.../musleMaterial.mat", typeof(Material));
        //m_Mat.color = _Col;
        Mesh _M = Resources.GetBuiltinResource<Mesh>("Cylinder.fbx");
        Vector3 pos = _Start + (_End - _Start) / 2;
        Vector3 fwd = Vector3.Cross(_End - _Start, new Vector3(0.1253f, 0.83f, 0.24f));
        Vector3 up = _End - _Start;
        //if (fwd == Vector3.zero || up == Vector3.zero)
        //    return;
        Quaternion rot = Quaternion.LookRotation(forward: fwd, upwards: up);
        float h = (_End - _Start).magnitude / 2;
        float r = _Volume == -1 ? _Radius : Mathf.Sqrt(_Volume / (Mathf.PI * h)); // use radius or get it from volume
        Vector3 scale = new Vector3(r * 2, h, r * 2); // scale == diameter == r*2
        //Gizmos.color = (_Selection != null && UnityEditor.Selection.Contains(_Selection)) ? Color.blue : _Col;
        //Gizmos.DrawMesh(_M, position: pos, rotation: rot, scale: scale);
        //Graphics.DrawMeshNow(_M, position: pos, rotation: rot);
        //Gizmos.color = Color.white;
        Matrix4x4 mt = Matrix4x4.identity;
        mt *= Matrix4x4.Translate(pos);
        mt *= Matrix4x4.Rotate(rot);
        mt *= Matrix4x4.Scale(scale);
        Graphics.DrawMeshNow(_M, matrix: mt);
    }
}
