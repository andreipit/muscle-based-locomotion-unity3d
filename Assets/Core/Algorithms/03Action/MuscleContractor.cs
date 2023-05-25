using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents.Actuators;

namespace MuscleSystemV01
{
    public static class MuscleContractor
    {
        public static void ContractAll(AgentConfig _Config, AgentCache _Cache, Transform _Env, ActionBuffers _Actions)
        {
            if (_Cache.Hero == null)
                return;

            for (int i = 0; i < _Cache.Muscles.Items.Count; i++) // 20
            {
                var muscle = _Cache.Muscles.Items[i];
                ContractOne(CharacterMuscles.GetMusclePoints(muscle), _Actions.ContinuousActions[i] * 150); // *100f
            }
            _Cache.LastFrameActions = _Actions.ContinuousActions.ToArray();
        }

        static void ContractOne(Transform[] _Path, float _Force)
        {
            Vector3 ResultForceVector = Vector3.zero;
            bool ForceConserve = true;
            float normalizedForce = _Force / (float)(_Path.Length - 1);

            for (int i = 0; i < _Path.Length - 1; i++)
            {
                Transform from = _Path[i];
                Transform to = _Path[i + 1];
                Vector3 dir = (from.position - to.position).normalized;
                Rigidbody rb = to.GetComponentInParent<Rigidbody>();
                rb.AddForceAtPosition(dir * normalizedForce, to.position, ForceMode.Force);
                ResultForceVector += dir * normalizedForce;
            }
            if (_Path.Length > 0 && ForceConserve == true)
            {
                Transform to0 = _Path[0];
                Rigidbody rb0 = to0.GetComponentInParent<Rigidbody>();
                rb0.AddForceAtPosition(-ResultForceVector, to0.position, ForceMode.Force);
            }
        }
    }
}
