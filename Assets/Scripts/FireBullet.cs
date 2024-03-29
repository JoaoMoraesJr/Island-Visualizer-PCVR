using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : NetworkBehaviour
{
    public float speed = 50f;
    public GameObject bulletObj;
    public Transform frontOfGun;

    public void Fire()
    {
        GetComponent<AudioSource>().Play();
        GameObject spawnedBullet = Instantiate(bulletObj, frontOfGun.position, frontOfGun.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * frontOfGun.forward;
        Destroy(spawnedBullet, 5f);
        FireGunServer();
    }

    [Command]
    public void FireGunServer()
    {
        FireGunClient();
    }

    [ClientRpc]
    public void FireGunClient()
    {
        GetComponent<AudioSource>().Play();
        GameObject spawnedBullet = Instantiate(bulletObj, frontOfGun.position, frontOfGun.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * frontOfGun.forward;
        Destroy(spawnedBullet, 5f);
    }
}
