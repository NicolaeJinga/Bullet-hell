using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBulletTravel : MonoBehaviour
{
    public float speed;

    void Start()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.right * speed;
    }
}
