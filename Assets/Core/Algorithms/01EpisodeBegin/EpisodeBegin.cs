using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;

namespace MuscleSystemV01
{
    public static class EpisodeBegin
    {
        
        public static AgentCache Begin(GameObject _HeroPrefab, AgentConfig _Config, AgentCache _Cache, Transform _Env)
        {
            if (_Cache.Hero != null)
                UnityEngine.Object.Destroy(_Cache.Hero);

            _Cache.Hero = UnityEngine.Object.Instantiate<GameObject>(_HeroPrefab, parent: _Env);
            _Cache.Hero.transform.parent = _Env;
            CharacterRigidbodies.AddOnCollisionEnter(_Cache.Body.Items);
            _Cache.Goal = _Cache.Hero.transform.position + new Vector3(1000, 0, 0);
            AgentCache.UpdateOrientation(_Cache.OrientationCube, _Cache.Hero.transform, _Cache.Goal);
            _Cache.Stats = Academy.Instance.StatsRecorder;
            _Cache.CompleteSteps = 0;
            return _Cache;
        }
    }
}
