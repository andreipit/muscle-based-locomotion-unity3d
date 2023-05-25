using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuscleSystemV01
{
    public static class EpisodeEnd
    {
        public static bool IsEpisodeEnded(AgentConfig _Config, AgentCache _Cache, Transform _Env)
        {
            bool result = false;
            foreach (var bp in _Cache.Body.Items)
            {
                var collisions = bp.GetComponent<OnCollisionEnterScript>().Entered;
                if ((bp.name == "shin_r" || bp.name == "shin_l" || bp.name == "thigh_r" || bp.name == "thigh_l" || bp.name == "hip") 
                    && (collisions.Contains("Terrain") || collisions.Contains("Wall")))
                {
                    result = true;
                    break;
                }
            }
            if (_Cache.Root.position.y < -3)
                result = true;
            return result;
        }
    }
}
