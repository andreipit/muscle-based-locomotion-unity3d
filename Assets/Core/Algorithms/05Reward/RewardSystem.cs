using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

namespace MuscleSystemV01
{
    public static class RewardSystem
    {
        public static void SetReward(AgentConfig _Config, AgentCache _Cache, Transform _Env, Action<float> _AddReward)
        {
            if (_Cache.Hero == null) // wait instantiation
                return;

            AgentCache.UpdateOrientation(_Cache.OrientationCube, _Cache.Root, _Cache.Goal);
            float movingToTargetRew = MoveToTargetReward(_Cache.OrientationCube, _Cache.Root, _Config.TargetSpeed);

            float fwdLegReward = _Cache.LegTrack.UpdateFwdLeg(); // _Config.legChangeComplexity);
            if (fwdLegReward > 0)
            {
                float stepsReward = Mathf.Log10(Mathf.Clamp((1 + fwdLegReward * _Cache.CompleteSteps) * 2, 0, 100)) / 2;
                _AddReward(stepsReward * Mathf.Max(0, movingToTargetRew));
                _Cache.CompleteSteps++;
                _Cache.Stats.Add("rewards2/forwardLegState", fwdLegReward);
                _Cache.Stats.Add("rewards2/forwardLegState", stepsReward);
                _Cache.Stats.Add("rewards2/moveTowardsTargetReward", Mathf.Max(0, movingToTargetRew));
            }
        }

        public static void HandleEndEpisode(AgentConfig _Config, AgentCache _Cache, Transform _Env, Action<float> _AddReward)
        {
            _Cache.Stats.Add("rewards/steps_avg", _Cache.CompleteSteps, StatAggregationMethod.Average);
            _Cache.Stats.Add("rewards2/steps_avg", _Cache.CompleteSteps, StatAggregationMethod.Average); 
            // Debug.Log($"steps: {_Cache.CompleteSteps}");
            _AddReward(-2f * _Config.legChangeComplexity / (_Cache.CompleteSteps + 1));
        }

        static float MoveToTargetReward(Transform _OrientationCube, Transform _Hip, float _TargetSpeed)
        {
            var cubeForward = _OrientationCube.transform.forward;
            Vector3 vel = _Hip.gameObject.GetComponent<Rigidbody>().velocity;
            return Vector3.Dot(cubeForward, vel.normalized) * ForwardLegTracker.RemapClamped(vel.magnitude, 0, _TargetSpeed, 0, 1);
        }
    }
}
