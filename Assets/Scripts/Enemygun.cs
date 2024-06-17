using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemygun : MonoBehaviour
{
    public GameObject EnemyBullet;
    void Start()
    {
        Invoke ("FireEnemyBullet", 1f) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FireEnemyBullet ()
    {
        GameObject playerShip = GameObject.Find("Player");

        if (playerShip != null )
        {
            GameObject bullet = (GameObject)Instantiate(EnemyBullet);

            bullet.transform.position = transform.position;

            Vector2 direction = playerShip.transform.position - bullet.transform.position;

            bullet.GetComponent<EnemyBullet>().SetDirection(direction);
        }
    }
}
