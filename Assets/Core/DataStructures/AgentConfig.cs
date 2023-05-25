using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuscleSystemV01
{
    public class AgentConfig : MonoBehaviour 
    {
        public float TargetSpeed = .25f; // {.25f, .75f, 1.5f, 3f, 5f};
        public readonly float legChangeComplexity = .25f; // {.25f, 0.5f, 1.0f, 1.5f, 2};

    }
}
