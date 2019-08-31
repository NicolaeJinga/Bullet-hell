using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    public float speed;
    public GameObject waypointParent;

    private Transform[] waypoints;
    [HideInInspector]
    public static bool firstTimeJoining = true;
    private bool proceeding = false;
    private int next;
    private float offset;

    private Vector3 offTheGrid;
    public static bool bossReset = false;

    //must be different because of dictionary, else dictionary.Add will throw exception
    public float level1, level2, level3;
    //float represents time until level activation
    //bool represents if the level has been completed
    private Dictionary<float, bool> levels; 
    private float elapsedTimeInLevel;
    
    public GameObject oe1, oe2, be1, be2, oe3, oe4;

	void Start ()
    {
        waypoints = waypointParent.GetComponentsInChildren<Transform>();
        firstTimeJoining = true;
        offset = 0.1f;
        offTheGrid = transform.position;
        bossReset = false;
        levels = new Dictionary<float, bool>(3);
        levels.Add(level1, false);
        levels.Add(level2, false);
        levels.Add(level3, false);
        elapsedTimeInLevel = level1;
    }
	
	void Update ()
    {
        if (!GameControl.instance.gameOver)
        {
            speed = 5;
            if (proceeding)
            {
                next = Random.Range(1, waypoints.Length - 1); //1-8
                proceeding = false;
            }
            if (firstTimeJoining)
            {
                Vector3 direction = (waypoints[1].transform.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
                if ((waypoints[1].transform.position - transform.position).magnitude < offset)
                {
                    firstTimeJoining = false;
                    proceeding = true;
                }
            }
            else
            {
                ProgressLevel();

                bossReset = false;
                Vector3 direction = (waypoints[next].transform.position - transform.position).normalized;
                transform.position += direction * Random.Range(speed - 1.5f, speed + 1.5f) * Time.deltaTime;
                if ((waypoints[next].transform.position - transform.position).magnitude < offset)
                {
                    proceeding = true;
                }
            }
        }
        if(GameControl.instance.gameOver)
        {
            proceeding = false;
            Vector3 direction = (offTheGrid - transform.position).normalized;
            Debug.DrawLine(offTheGrid, transform.position, Color.green);
            transform.position += direction * speed * Time.deltaTime;
            if((offTheGrid - transform.position).magnitude < offset)
            {
                bossReset = true;
                speed = 0;
                firstTimeJoining = true;
                DeactivateWeapons();
                elapsedTimeInLevel = level1;
            }
        }
	}

    void ProgressLevel()
    {
        elapsedTimeInLevel -= Time.deltaTime;
        if (elapsedTimeInLevel <= 0 && !levels[level1])
        {
            levels[level1] = true;
            oe1.SetActive(true);
            oe2.SetActive(true);
            elapsedTimeInLevel = level2;
        }

        if (elapsedTimeInLevel <= 0 && !levels[level2])
        {
            levels[level2] = true;
            be1.SetActive(true);
            be2.SetActive(true);
            elapsedTimeInLevel = level3;
        }

        if (elapsedTimeInLevel <= 0 && !levels[level3])
        {
            levels[level3] = true;
            oe3.SetActive(true);
            oe4.SetActive(true);
        }
    }

    void DeactivateWeapons()
    {
        levels[level1] = false;
        levels[level2] = false;
        levels[level3] = false;
        oe1.SetActive(false);
        oe2.SetActive(false);
        be1.SetActive(false);
        be2.SetActive(false);
        oe3.SetActive(false);
        oe4.SetActive(false);
    }
}
