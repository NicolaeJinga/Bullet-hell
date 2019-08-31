using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameParticleTravel : MonoBehaviour
{
    public float speed;
    public float range;

    private GameObject player;

	void Start ()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.right * speed;
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update ()
    {
        if (transform.position.x > player.transform.position.x + range)
        {
            Destroy(gameObject);
        }
	}
}
