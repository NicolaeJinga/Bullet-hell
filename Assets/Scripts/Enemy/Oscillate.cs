using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillate : MonoBehaviour
{
    public float angleSpan;
    public bool direction;
    
	void Update ()
    {
        if (!GameControl.instance.gameOver)
        {
            float angle = Mathf.Sin(Time.time) * angleSpan;
            if (direction)
            {
                angle = -angle;
            }
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
