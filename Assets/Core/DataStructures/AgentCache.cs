using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Animations;
using Unity.MLAgents;


namespace MuscleSystemV01
{
    public class AgentCache : MonoBehaviour 
    {
        public GameObject Hero;
        public Vector3 Goal;
        public int CompleteSteps; // how long is character already running
        public float[] LastFrameActions; // last
        public ForwardLegTracker LegTrack => Hero.GetComponent<ForwardLegTracker>();
        public StatsRecorder Stats;
        

        #region Properties

        public Transform Root => Hero.transform.Find("Root");
        public Rigidbody FootL => Body.Items.Find(x => x.name == "foot_l");
        public Rigidbody FootR => Body.Items.Find(x => x.name == "foot_r");
        public Transform OrientationCube => Hero.transform.Find("OrientationCube");
        public CharacterRigidbodies Body => Hero.GetComponent<CharacterRigidbodies>();
        public CharacterMuscles Muscles => Hero.GetComponent<CharacterMuscles>();

        #endregion


        public static void UpdateOrientation(Transform _Cube, Transform _RootBP, Vector3 _TargetPos)
        {
            
            var dirVector = _TargetPos - _Cube.position;
            dirVector.y = 0; // Flatten dir on the y. This will only work on level, uneven surfaces
            var lookRot =
                dirVector == Vector3.zero
                    ? Quaternion.identity
                    : Quaternion.LookRotation(dirVector); // Get our look rot to the target

            _Cube.SetPositionAndRotation(_RootBP.position, lookRot);
        }
    }
}
