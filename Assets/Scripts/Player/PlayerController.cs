using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    public float speed;
    public GameObject boundary;

    public GameObject iceBullet;
    public GameObject flamethrower;
    public Transform shotSpawn;
    public float iceBulletFireRate;
    public float flamethrowerFireRate;

    private float iceBulletNextFire;
    private float flamethrowerNextFire;

    public delegate void PowerUp();
    public static event PowerUp OnPowerUp;

    public enum State
    {
        NORMAL = 0,
        POWEREDUP,
        COUNT
    }

    [HideInInspector] public State state;
    [HideInInspector] public bool powerUpActivated = false;
    [HideInInspector] public Vector3 startingPos;

    private void Awake()
    {
        state = State.NORMAL;
        powerUpActivated = false;
        startingPos = GetComponent<Transform>().position;
    }

    void Update()
    {
        if (!GameControl.instance.gameOver)
        {
            transform.Translate(gameObject.transform.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime);
            transform.Translate(gameObject.transform.up * Input.GetAxis("Vertical") * speed * Time.deltaTime);
            RepositionUponBoundaryExit();

            //shoot ice bullets
            if (Input.GetButton("Jump") && Time.time > iceBulletNextFire && !powerUpActivated)
            {
                iceBulletNextFire = Time.time + iceBulletFireRate;
                Instantiate(iceBullet, shotSpawn.position, shotSpawn.rotation);
            }
            //shoot flamethrower
            if(Input.GetButton("Jump") && Time.time > flamethrowerNextFire && powerUpActivated)
            {
                flamethrowerNextFire = Time.time + flamethrowerFireRate;
                Instantiate(flamethrower, shotSpawn.position, shotSpawn.rotation);
            }
            //activate powerup
            if (state == State.POWEREDUP && !powerUpActivated)
            {
                if (Input.GetButton("Fire1"))
                {
                    if (OnPowerUp != null)
                    {
                        powerUpActivated = true;
                        OnPowerUp();
                    }
                }
            }
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
        if (transform.position.x + hx > B.x/2.0f)
        {
            transform.position = new Vector3(B.x/2.0f - hx, transform.position.y, transform.position.z);
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
