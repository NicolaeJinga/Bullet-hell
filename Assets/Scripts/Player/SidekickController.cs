using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SidekickController : MonoBehaviour
{
    public float speed;

    public GameObject boundary;
    public Material mat1;
    public Material mat2;
    public float wingsDelay = 0.5f;
    private float currentWingsDelay;
    private bool first = true;

    [HideInInspector] public Vector3 startingPos;

    void Awake ()
    {
        startingPos = GetComponent<Transform>().position;
        currentWingsDelay = wingsDelay;
        first = true;
    }
	
	void Update ()
    {
        if (!GameControl.instance.gameOver)
        {
            transform.Translate(gameObject.transform.right * Input.GetAxis("Horizontal2") * speed * Time.deltaTime);
            transform.Translate(gameObject.transform.up * Input.GetAxis("Vertical2") * speed * Time.deltaTime);
            RepositionUponBoundaryExit();
        }
        currentWingsDelay -= Time.deltaTime;
		if(currentWingsDelay <= 0.0f)
        {
            GetComponent<Renderer>().material = first ? mat2 : mat1;
            first = !first;
            currentWingsDelay = wingsDelay;
        }
	}

    void RepositionUponBoundaryExit()
    {
        float bhx = boundary.transform.localScale.x / 2.0f;
        float bhy = boundary.transform.localScale.y / 2.0f;
        float hx = transform.localScale.x / 2.0f;
        float hy = transform.localScale.y / 2.0f;

        Vector3 A;
        A.x = boundary.transform.position.x - bhx;
        A.y = boundary.transform.position.y - bhy;
        A.z = transform.position.z;
        Vector3 B;
        B.x = boundary.transform.position.x + bhx;
        B.y = boundary.transform.position.y + bhy;
        B.z = transform.position.z;

        if (transform.position.x - hx < A.x)
        {
            transform.position = new Vector3(A.x + hx, transform.position.y, transform.position.z);
        }
        if (transform.position.x + hx > B.x)
        {
            transform.position = new Vector3(B.x - hx, transform.position.y, transform.position.z);
        }
        if (transform.position.y - hy < A.y/1.3f)
        {
            transform.position = new Vector3(transform.position.x, A.y/1.3f + hy, transform.position.z);
        }
        if (transform.position.y + hy > B.y)
        {
            transform.position = new Vector3(transform.position.x, B.y - hy, transform.position.z);
        }
    }
}
