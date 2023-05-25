using System.Collections.Generic;
using UnityEngine;


namespace MuscleSystemV01
{
    public class CopyEnv : MonoBehaviour
    {
        [SerializeField] GameObject m_Env;
        [Bttn("Copy")][SerializeField] bool m_Copy;
        [Bttn("DeleteAll")][SerializeField] bool m_DeleteAll;
        [SerializeField] int m_CopiesCount = 1;
        [SerializeField] List<GameObject> m_Copies;


        void Copy()
        {
            DeleteAll(ref m_Copies);
            m_Copies = Dublicate(m_Env, m_CopiesCount);
        }

        void DeleteAll()
        {
            DeleteAll(ref m_Copies);
        }

        static void DeleteAll(ref List<GameObject> _Copies)
        {
            for (int i = 0; i < _Copies.Count; i++)
                DestroyImmediate(_Copies[i]);
            _Copies = new List<GameObject>();
        }

        static List<GameObject> Dublicate(GameObject _Obj, int _Count)
        {
            var result = new List<GameObject>();
            for (int i = 0; i < _Count; i++)
            {
                GameObject newObj = UnityEngine.Object.Instantiate<GameObject>(_Obj, new Vector3(0, 0, 0 + (i+1) * 10.0f), Quaternion.identity);
                result.Add(newObj);
            }
            return result;

        }
    }
}
