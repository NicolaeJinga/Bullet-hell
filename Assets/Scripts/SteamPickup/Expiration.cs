using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expiration : MonoBehaviour
{
    public float expirationInSeconds = 5;
    public bool stateRender;
    public float startBlink = 2;
    public float blinkRate = 0.15f;
    private float elapsedBlink;
    private int nrBlinks = 1;

	void Start ()
    {
        stateRender = GetComponent<Renderer>().enabled;
        elapsedBlink = startBlink;
    }
	
	void Update ()
    {
        expirationInSeconds -= Time.deltaTime;
        if (expirationInSeconds < startBlink)
        {
            Blink();
        }
	    if (expirationInSeconds <= 0.0f)
        {
            Destroy(gameObject);
        }
	}

    void Blink()
    {
        elapsedBlink -= Time.deltaTime;
        if (elapsedBlink < startBlink - (blinkRate * nrBlinks))
        {
            nrBlinks++;
            stateRender = !stateRender;
            GetComponent<Renderer>().enabled = stateRender;
        }
    }
}