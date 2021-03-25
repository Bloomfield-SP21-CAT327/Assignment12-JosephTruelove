using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Bullet : NetworkBehaviour
{
    [SyncVar]
    public Color color;
    [SyncVar]
    public uint parentNetID;

    public override void OnStartClient()
    {
        gameObject.GetComponent<Renderer>().material.color = color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(isServer && other.tag == "Enemy")
        {
            Player player = NetworkIdentity.spawned[parentNetID].GetComponent<Player>();
            player.score += 100;
            Destroy(other.gameObject);
        }
    }
}
