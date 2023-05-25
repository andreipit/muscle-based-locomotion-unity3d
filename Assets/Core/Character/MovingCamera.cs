using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MuscleSystemV01
{
    public class MovingCamera : MonoBehaviour
    {
        void Update()
        {
            if (Camera.main == null)
                return;
            Vector3 pos = transform.position;
            Camera.main.transform.position = new Vector3(pos.x + 4, 2, 0);
            Camera.main.transform.eulerAngles = new Vector3(20, -90, 0);
        }
    }
}