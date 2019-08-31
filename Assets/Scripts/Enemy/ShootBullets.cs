using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullets : MonoBehaviour
{
    public GameObject bullet;
    public float bulletInterval;

    private float nextFire;
    [HideInInspector]
    public Vector3 face;

    void Start ()
    { 
        face = -transform.right;
        nextFire = 0;
    }
	
	void Update ()
    {
        if (!GameControl.instance.gameOver && !BossMovement.firstTimeJoining)
        {
            nextFire += Time.deltaTime;
            if (nextFire > bulletInterval)
            {
                face = -transform.right;
                bullet.GetComponent<Travel>().direction = face;
                Instantiate(bullet, transform.position + face / 2, Quaternion.Euler(face));
                nextFire = 0;
            }
        }
    }
}
