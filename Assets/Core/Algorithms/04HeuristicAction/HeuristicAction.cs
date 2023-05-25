using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

namespace MuscleSystemV01
{
    public static class HeuristicActions
    {
        public static void SetActionsInPlace(ActionSegment<float> _Actions)
        {
            _Actions[0] = Input.GetAxisRaw("Horizontal"); // then modify array by link
            _Actions[1] = Input.GetAxisRaw("Vertical"); // FIX: Edit - project settings - player - configuration - input = both
        }
    }
}
