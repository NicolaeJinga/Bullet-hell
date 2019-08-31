using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Travel : MonoBehaviour
{
    public float speed;
    [HideInInspector]
    public Vector3 direction;

    public GameObject smoke;
    public GameObject wildfire;

    //makes sure it doesn't trigger for the continous stream of flames,
    //thus Instantiating way more wildfire
    private bool hit;

	void Start ()
    {
        hit = false;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("IceBullet"))
        {
            Destroy(other.gameObject);
            Instantiate(smoke, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        if(other.gameObject.CompareTag("FlameParticle") && !hit)
        {
            hit = true;
            Destroy(other.gameObject);
            Instantiate(wildfire, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
    }

    void Update ()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
