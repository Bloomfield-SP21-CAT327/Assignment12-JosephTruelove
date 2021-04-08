using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Enemy : NetworkBehaviour
{
    [SyncVar]
    public Color color;
    public float moveSpeed = 1.875f;
    public GameObject bulletPrefab;
    [SyncVar]
    public int score;
    public int health = 5;

    public float fireTime = 3;


    private void Update()
    {


        if (health == 0)
        {
            Destroy(this);
        }
        

        if (isServer)
        {
            if (Time.fixedTime > fireTime)
            {
                GameObject bullet = (GameObject)Instantiate(bulletPrefab, this.transform.position + this.transform.right, Quaternion.identity);
                bullet.GetComponent<Rigidbody>().velocity = Vector3.forward * -17.5f;
                bullet.GetComponent<Bullet>().color = color;
                bullet.GetComponent<Bullet>().parentNetID = this.netId;
                Destroy(bullet, 0.875f);
                NetworkServer.Spawn(bullet);
                fireTime = Time.time + 2;
            }
        }

    }



}
