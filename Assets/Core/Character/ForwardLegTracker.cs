using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuscleSystemV01
{
    public class ForwardLegTracker : MonoBehaviour
    {
        public Transform ForwardLeg; 
        [SerializeField] Transform m_FootR;
        [SerializeField] Transform m_FootL;

        const float COMPLEXITY = 0.25f; // COMPLEXITY_X={.25f, 0.5f, 1.0f, 1.5f, 2}; 0.25 gives the biggest reward
        const float THRESHOLD_X = 0.075f * COMPLEXITY;
        const float THRESHOLD_Y = 0.15f;
        [SerializeField] float m_DebugReward = 0;


        /// <summary>
        /// Check if leg has changed. If so, update saved fwd leg.
        /// </summary>
        /// <returns> Reward (step length normalized) </returns>
        public float UpdateFwdLeg()
        {
            Transform newLeg = GetFwdLegFiltered(ForwardLeg, m_FootL, m_FootR);
            if (newLeg != ForwardLeg)
            {
                ForwardLeg = newLeg;
                return RemapClamped(GapLength(m_FootL, m_FootR), 0, COMPLEXITY, 0, 1); // normalize step percent
            }
            return 0;
        }

        /// <summary>
        /// Like normalization.
        /// Converts value from interval1 to interval2 and clamp (ie stay inside inter.2 always).
        /// Ex: int1=[0, 10], int2=[0,1], value=6. Algo: 6 is 60%, 60% in int2=0.6. No clamp needed.
        /// https://docs.unity3d.com/Packages/com.unity.mathematics@1.2/api/Unity.Mathematics.math.remap.html
        /// </summary>
        /// <param name="_Value"> This value we want to convert </param>
        /// <param name="_In1"> Interval1 start </param>
        /// <param name="_In2"> Interval1 end </param>
        /// <param name="_Out1">nterval2 start</param>
        /// <param name="_Out2">nterval2 end</param>
        /// <returns></returns>
        public static float RemapClamped(float _Value, float _In1, float _In2, float _Out1, float _Out2)
        {
            float fractionInInterval1 = (_Value - _In1) / (_In2 - _In1); // percent of value inside int1
            float sameInInterval2 = _Out1 + (_Out2 - _Out1) * fractionInInterval1; // same value in int2
            float alwaysInInterval2 = Mathf.Clamp(sameInInterval2, Mathf.Min(_Out1, _Out2), Mathf.Max(_Out1, _Out2));
            return alwaysInInterval2;
        }

        static Transform GetFwdLegFiltered(Transform _Old, Transform _L, Transform _R)
        {
            Transform candidate = (_L.position.x > _R.position.x) ? _L: _R;
            if (candidate != _Old && GapLength(_L, _R) > THRESHOLD_X) // if leg has really changed
            {
                if (candidate.position.y < THRESHOLD_Y) // if new fwd leg has landed
                    return candidate;
            }
            return _Old;
        }
        
        static float GapLength(Transform _FootL, Transform _FootR) => Mathf.Abs(_FootL.position.x - _FootR.position.x);

        void OnDrawGizmos() { m_DebugReward += UpdateFwdLeg(); } // void Update()
    }
}
