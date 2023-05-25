using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

namespace MuscleSystemV01
{
    public static class Observation
    {
        public static void Collect(AgentConfig _Config, AgentCache _Cache, Transform _Env, ref VectorSensor _Sensor)
        {
            if (_Cache.Hero == null)
                return;

            foreach(var body in _Cache.Body.Items)
            {
                // distance from current bodypart to root (in local space)
                _Sensor.AddObservation(_Cache.OrientationCube.InverseTransformDirection(body.transform.position - _Cache.Root.position));
                _Sensor.AddObservation(body.transform.localRotation);
                // speed and angularSpeed of current bodypart
                _Sensor.AddObservation(_Cache.OrientationCube.InverseTransformDirection(body.velocity));
                _Sensor.AddObservation(_Cache.OrientationCube.InverseTransformDirection(body.angularVelocity));
            }

            _Sensor.AddObservation(Quaternion.FromToRotation(_Cache.Root.transform.forward, _Cache.OrientationCube.transform.forward));
            _Sensor.AddObservation(_Cache.OrientationCube.transform.InverseTransformPoint(_Cache.Goal));
            _Sensor.AddObservation(_Cache.CompleteSteps);
            
            Vector3 vel = _Cache.Root.GetComponent<Rigidbody>().velocity; // m_StatsRecorder.Add("rewards2/root_vel", vel.magnitude);
            var cubeForward = _Cache.OrientationCube.transform.forward;
            var velGoal = cubeForward * _Config.TargetSpeed;
            _Sensor.AddObservation(Vector3.Distance(velGoal, vel)); // _Sensor.AddObservation(_Cache.Hero.transform.localPosition);
        }
    }
}
