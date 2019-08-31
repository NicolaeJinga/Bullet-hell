using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Blink : MonoBehaviour
{
    float speed = 2.0f;

    private Color color;

    void Start()
    {
        color = gameObject.GetComponent<Text>().color;
    }

    void Update()
    {
        color.a = Mathf.Round(Mathf.PingPong(Time.time * speed, 1.0f));
        gameObject.GetComponent<Text>().color = color;
    }
}
