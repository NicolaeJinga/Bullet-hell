using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnContact : MonoBehaviour
{
    public delegate void PickupAction(string pickup, GameObject character);
    public static event PickupAction OnPickup;

    public delegate void HitByEnemyBullet();
    public static event HitByEnemyBullet OnHitByEnemyBullet;

    void Start ()
    {
		
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Steam"))
        {
            if(OnPickup != null)
            {
                OnPickup("steam", gameObject);
            }
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("Wildfire"))
        {
            if(OnPickup != null)
            {
                OnPickup("wildfire", gameObject);
            }
            Destroy(other.gameObject);
        }

        if(other.gameObject.CompareTag("EnemyBullet") && gameObject.name == "Player")
        {
            if(OnHitByEnemyBullet != null)
            {
                OnHitByEnemyBullet();
            }
            Destroy(other.gameObject);
        }
    }
}
