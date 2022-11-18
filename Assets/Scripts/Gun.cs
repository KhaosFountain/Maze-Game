using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    public Transform bulletSpawn;
    public GameObject bulletPrefab;

    public float bulletSpeed = 10f;

    public float fireRate = 10;
    public float lastFired;
    

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Input.GetKeyDown(KeyCode.Mouse0)){
            var bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bulletSpawn.forward * bulletSpeed; 
        }


    }
}
