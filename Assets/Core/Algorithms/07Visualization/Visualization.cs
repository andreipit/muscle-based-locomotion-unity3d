using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuscleSystemV01
{
    public static class Visualization
    {
        public static void DrawMuscles(AgentConfig _Config, AgentCache _Cache, Transform _Env)
        {
            if (_Cache.LastFrameActions == null || _Cache.Hero == null)
                return;

            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(CharacterRigidbodies.GetMassCenter(_Cache.Body.Items), 0.1f);

            for (int i = 0; i <= _Cache.Muscles.Items.Count - 2; i++)
            {
                var muscle = _Cache.Muscles.Items[i];
                GizmoCylinder.DrawLines(
                    CharacterMuscles.GetMusclePoints(muscle), 
                    _Radius: 0.01f, 
                    new Color(1, Mathf.Clamp01(1 - _Cache.LastFrameActions[i]), Mathf.Clamp01(1 - _Cache.LastFrameActions[i]), 1)
                    , _Volume: -1);
            }
        }

        
    }
}
