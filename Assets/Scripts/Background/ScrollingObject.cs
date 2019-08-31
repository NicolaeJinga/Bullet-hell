using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    private Rigidbody2D rb2d;
    bool optimize = false;

	void Start ()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
        optimize = false;
	}
	
	void FixedUpdate ()
    {
	    if(GameControl.instance.gameOver)
        {
            rb2d.velocity = Vector2.zero;
            optimize = true;
        }
        else if (!GameControl.instance.gameOver && optimize)
        {
            rb2d.velocity = new Vector2(GameControl.instance.scrollSpeed, 0);
            optimize = false;
        }
	}
}
