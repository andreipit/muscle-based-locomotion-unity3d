using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuscleSystemV01
{
    public class OnCollisionEnterScript : MonoBehaviour
    {
        public List<string> Entered = new List<string>();


        void OnCollisionEnter(Collision _Col) => Entered.Add(_Col.transform.name);
    }
}
