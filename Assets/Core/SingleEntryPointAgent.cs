using System;
using System.Collections.Generic;
using System.Linq;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEditor;
using UnityEngine;

namespace MuscleSystemV01
{
    public class SingleEntryPointAgent : Agent
    {
        [SerializeField] GameObject m_HeroPrefab;
        [SerializeField] AgentCache m_Cache;
        [SerializeField] AgentConfig m_Config;


        public override void OnEpisodeBegin() => m_Cache = EpisodeBegin.Begin(m_HeroPrefab, m_Config, m_Cache, transform);

        public override void CollectObservations(VectorSensor sensor) => Observation.Collect(m_Config, m_Cache, transform, ref sensor);

        public override void OnActionReceived(ActionBuffers actions) => MuscleContractor.ContractAll(m_Config, m_Cache, transform, actions);

        public override void Heuristic(in ActionBuffers actionsOut) => HeuristicActions.SetActionsInPlace(actionsOut.ContinuousActions);
        
        void OnDrawGizmos() => Visualization.DrawMuscles(m_Config, m_Cache, transform);

        void FixedUpdate()
        {
            RewardSystem.SetReward(m_Config, m_Cache, transform, AddReward);
            if (EpisodeEnd.IsEpisodeEnded(m_Config, m_Cache, transform)) 
            {
                RewardSystem.HandleEndEpisode(m_Config, m_Cache, transform, AddReward);
                EndEpisode();
            }
        }

    }
}
