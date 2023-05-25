using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuscleSystemV01
{
    public class CharacterVisualizer : MonoBehaviour
    {
            public bool DrawMuscles;

            void OnDrawGizmos()
            {
                if (DrawMuscles)
                {
                    float action = 0.6f;
                    foreach(var muscle in GetComponent<CharacterMuscles>().Items)
                    {
                        Transform[] path = CharacterMuscles.GetMusclePoints(muscle);
                        GizmoCylinder.DrawLines(path, _Radius: 0.01f, new Color(1, 1 - action * 10 / 10, 1 - action * 10 / 10, 1), _Selection: gameObject, _Volume: -1);
                    }
                }
            }
    }
}
